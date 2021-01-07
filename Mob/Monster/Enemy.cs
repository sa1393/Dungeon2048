using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Enemy : MovingObject
{
    public Image tierImage;
    public SpriteRenderer buff_Img;
    public Text info;
    Buff buff;
    
    public string name = null; //이름
    protected int exp = 0; //주는 경험치

    protected SpriteRenderer sr;
    protected Animator anime;
    protected Animator effectAnime;
    public EnemyStat enemyStat;

    public Image hpBar;

    private float hpCurrentFill = 1f;

    float moveAnimeDelay = 0.3f;  //움직이기만 했을 때 키 딜레이

    public string Name { get => name; set => name = value; }
    public float HpCurrentFill { get => hpCurrentFill; set => hpCurrentFill = value; }

    float lerpSpeed = 15f;

    int dirX = 0;
    int dirY = 0;
    public virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anime = transform.GetChild(1).GetComponent<Animator>();
        effectAnime = transform.GetChild(0).GetComponent<Animator>();
        enemyStat = new EnemyStat();
    }

    public virtual void Move(int dirX, int dirY) //이동
    {
        BuffUse();
        if (enemyStat.state == EnemyStat.State.parker || enemyStat.state == EnemyStat.State.stun || enemyStat.state == EnemyStat.State.ice)
        {
            return;
        }
        enemyStat.MoveMent++;
        
        if (enemyStat.MoveMent != enemyStat.MaxMoveMent)
        {
        }
        else
        {
            int desX = LocX + dirX;
            int desY = LocY + dirY;
            enemyStat.MoveMent = 0;

            if (CheckWall(desX, desY))
            {
                return;
            }

            if (CheckMapNull(desX, desY))
            {
                SetAnimationDelay(moveAnimeDelay);
                base.Move(LocX, LocY, desX, desY);
                LocX = desX;
                LocY = desY;
            }
            else if (CheckEnemy(desX, desY))
            {
                if (CheckEqul(desX, desY)) //승급 조건
                {
                    StartCoroutine(Promotion(desX, desY, enemyStat.Evolution, enemyStat.Tier));
                }
                else if (CheckTier(desX, desY)) //합성 조건
                {
                    if (enemyStat.Evolution == GameManager.instance.map[desX, desY].GetComponent<Enemy>().enemyStat.Evolution)
                        StartCoroutine(Synthetic(desX, desY, enemyStat.Evolution, enemyStat.Tier+1));
                    else
                    {
                        int Evol = enemyStat.Evolution > GameManager.instance.map[desX, desY].GetComponent<Enemy>().enemyStat.Evolution ?
                            enemyStat.Evolution : GameManager.instance.map[desX, desY].GetComponent<Enemy>().enemyStat.Evolution;

                        StartCoroutine(Synthetic(desX, desY, Evol, enemyStat.Tier + 1));
                    }
                }
            }
            
        }
        
    }

    public virtual void SetStat(int evolution, int tier, string how)
    {
        enemyStat.Tier = tier;
        int tierNum;
        
        if (enemyStat.Tier - 1 < 10)
            tierNum = enemyStat.Tier - 1;
        else
            tierNum = 9;
        tierImage.sprite = GameManager.instance.tiers[tierNum];
        //effectAnime.SetTrigger(how);
    }

    public virtual IEnumerator Hurt(int hitDamage, bool skill, int dirX, int dirY) //데미지 받기
    {
        this.dirX = dirX;
        this.dirY = dirY;
        enemyStat.hit(hitDamage);
        enemyStat.MoveMent--;
        if (enemyStat.Hp < 0) enemyStat.Hp = 0;
        HpCurrentFill = (float)enemyStat.Hp / enemyStat.MaxHp;

        if (enemyStat.Hp <= 0)
        {
            SetCounterAnimeDelay(0f);
            GameManager.instance.map[LocX, LocY] = null;
            PlayerData.instance.player.GetComponent<Player>().getExp(exp);
            PlayerData.instance.player.GetComponent<Player>().SetBar();

            Die();
        }
        else
        {
            SetCounterAnimeDelay(0f);
            anime.SetTrigger("hit");
            yield return new WaitForSeconds(0.3f);
            if (!skill)
            {
                anime.SetTrigger("attack");
                if (CheckPlayer(LocX + dirX, LocY + dirY))
                {
                    GameManager.instance.map[LocX, LocY].GetComponent<Player>().Hurt(enemyStat.Damage);
                }
                else if(CheckSpirit(LocX + dirX, LocY + dirY))
                {
                    GameManager.instance.map[LocX, LocY].GetComponent<PlayerSpirit>().Hurt(enemyStat.Damage);
                }
                yield return new WaitForSeconds(0.3f);
                
            }
        }
    }

    public IEnumerator Synthetic(int desX, int desY, int beforeEvolution, int beforeTier) // 합성
    {
        SetAnimationDelay(0.3f);
        GameObject temp = GameManager.instance.map[desX, desY].gameObject;
        base.Move(LocX, LocY, desX, desY);

        yield return new WaitForSeconds(0.2f);

        temp.GetComponent<Enemy>().Remove();

        int ran = Random.Range(0, GameManager.instance.InGameScript.EnemyPrefebs.Count);
        GameObject newEnemy = Instantiate(GameManager.instance.InGameScript.EnemyPrefebs[ran], new Vector2(XBasicLoc + (XInterval * desX), YBasicLoc + (YInterval * desY)), Quaternion.identity);
        GameManager.instance.map[desX, desY] = newEnemy;
        newEnemy.GetComponent<Enemy>().SetStat(beforeEvolution, beforeTier, "synthe");
        newEnemy.GetComponent<Enemy>().LocX = desX;
        newEnemy.GetComponent<Enemy>().LocY = desY;
        GameManager.instance.InGameScript.enemyList.Add(newEnemy.GetComponent<Enemy>());

        Remove();
    }

    public IEnumerator Promotion(int desX, int desY, int beforeEvolution, int beforeTier) // 승급
    {
        SetAnimationDelay(0.3f);
        GameObject temp = GameManager.instance.map[desX, desY].gameObject;
        base.Move(LocX, LocY, desX, desY);

        yield return new WaitForSeconds(0.2f);
        temp.GetComponent<Enemy>().Remove();

        GameObject newEnemy = Instantiate(GameManager.instance.InGameScript.EnemyPrefebs[enemyStat.Id], new Vector2(XBasicLoc + (XInterval * desX), YBasicLoc + (YInterval * desY)), Quaternion.identity);
        GameManager.instance.map[desX, desY] = newEnemy;
        newEnemy.GetComponent<Enemy>().SetStat(beforeEvolution + 1, beforeTier + 1, "promotion");
        newEnemy.GetComponent<Enemy>().LocX = desX;
        newEnemy.GetComponent<Enemy>().LocY = desY;
        GameManager.instance.InGameScript.enemyList.Add(newEnemy.GetComponent<Enemy>());

        Remove();
    }

    public void BuffUse()
    {
        if (enemyStat.state == EnemyStat.State.none)
            return;

        buff.Use();
        if (buff.Turn == 0)
        {
            RemoveBuff(); 
        }
    }

    public void AddBuff(Buff buff)
    {
        if(buff != null)
        {
            RemoveBuff();
        }
        this.buff = buff;
        buff_Img.sprite = buff.Sprite;
        Debug.Log("버프 추가");
    }

    void RemoveBuff()
    {
        buff = null;
        enemyStat.state = EnemyStat.State.none;
        buff_Img.sprite = null;
    }

    public void Remove() //삭제
    {
        GameManager.instance.InGameScript.enemyList.Remove(gameObject.GetComponent<Enemy>());
        Destroy(gameObject);
    }

    public IEnumerator Die() //죽음
    {
        anime.SetTrigger("dead");
        yield return new WaitForSeconds(0.6f);
        GameManager.instance.InGameScript.enemyList.Remove(gameObject.GetComponent<Enemy>());
        Destroy(gameObject);
    }

    public virtual void Update()
    {
        if(hpBar.fillAmount != HpCurrentFill)
        {
            hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, HpCurrentFill, Time.deltaTime * lerpSpeed);
        }
    }

}
