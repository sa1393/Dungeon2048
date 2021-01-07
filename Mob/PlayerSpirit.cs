using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpirit : PlayerBase
{
    [SerializeField]
    public Image tierImage;
    public Image hpBar;

    private float hpCurrentFill = 1f;
    float lerpSpeed = 15f;
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        UpdateStat();
        anime.SetBool("dead", false);
    }

    public void UpdateTier()
    {
        int tier = 0;
        if (playerStat.Tier - 1 < 10)
            tier = playerStat.Tier - 1;
        else
            tier = 9;
        tierImage.sprite = GameManager.instance.tiers[tier];
        //effectAnime.SetTrigger(how);
    }

    public void MoveAndAttack(int dirX, int dirY)
    {
        this.dirX = dirX;
        this.dirY = dirY;

        anime.SetFloat("dirX", dirX);
        anime.SetFloat("dirY", dirY);

        int desX = LocX + dirX;
        int desY = LocY + dirY;

        if (CheckWall(desX, desY))
        {
            return; //벽
        }
        else if (CheckSpirit(desX, desY))
        {
            if (CheckTier(desX, desY))
            {
                Debug.Log("합성");
                StartCoroutine(Promotion(desX, desY, playerStat.Tier));
            }
        }
        else if (CheckPlayer(desX, desY))
        {
            if(CheckTier(desX, desY)){
                StartCoroutine(drain(desX, desY));
            }
        }
        else
        {
            MoveAttack(dirX, dirY);
        }
    }

    public override void MoveAttack(int dirX, int dirY)
    {
        int desX = dirX + LocX;
        int desY = dirY + LocY;

        if (CheckWall(desX, desY))
        {
            return; //벽
        }

        if (CheckMapNull(desX, desY))
        { // 이동 하고 공격
            base.Move(LocX, LocY, desX, desY);
            LocX = desX;
            LocY = desY;
            desX += dirX;
            desY += dirY;
            if (CheckWall(desX, desY))
            {
                return; //벽
            }
            if (CheckEnemy(desX, desY))
            {
                 StartCoroutine(Attack(desX, desY, false, attackDelay));
            }
            else
            {
                SetAnimationDelay(moveAnimeDelay);
            }
        }
        else if (CheckEnemy(desX, desY))// 적 바로 공격
        {
            StartCoroutine(Attack(desX, desY, false, 0f));
            if (CheckMapNull(desX, desY))
            {
                StartCoroutine(killMove(desX, desY, dirX, dirY));
            }
        }
    }

    public void Attack(int damage, int locX, int locY, int desX, int desY, float criticalDamage, float critical, bool skill) //공격 실행 후 이동 가능 반환
    {
        if (critical > 1) critical = 1;
        int ran = Random.Range(1, 101);
        int resultDamage;

        if (ran <= (critical * 100))
        {
            resultDamage = (int)(damage + (damage * criticalDamage));
        }
        else
        {
            resultDamage = damage;
        }

        GameManager.instance.StartCoroutine(GameManager.instance.map[desX, desY].GetComponent<Enemy>().Hurt(resultDamage, skill, locX - desX, locY - desY));
    }

    IEnumerator Promotion(int desX, int desY, int beforeTier) // 승급
    {
        SetAnimationDelay(0.2f);
        GameObject temp = GameManager.instance.map[desX, desY].gameObject;
        base.Move(LocX, LocY, desX, desY);

        yield return new WaitForSeconds(0.2f);
        GameManager.instance.InGameScript.playerSpiritList.Remove(temp.GetComponent<PlayerSpirit>());
        Destroy(temp);

        GameObject newSpirit = Instantiate(GameManager.instance.InGameScript.playerSpiritPrefeb, new Vector2(XBasicLoc + (XInterval * desX), YBasicLoc + (YInterval * desY)), Quaternion.identity);
        GameManager.instance.map[desX, desY] = newSpirit;

        newSpirit.GetComponent<PlayerSpirit>().playerStat.Tier = playerStat.Tier+1;

        newSpirit.GetComponent<PlayerSpirit>().LocX = desX;
        newSpirit.GetComponent<PlayerSpirit>().LocY = desY;
        newSpirit.GetComponent<PlayerSpirit>().UpdateStat();
        GameManager.instance.InGameScript.playerSpiritList.Add(newSpirit.GetComponent<PlayerSpirit>());

        GameManager.instance.InGameScript.playerSpiritList.Remove(gameObject.GetComponent<PlayerSpirit>());
        Destroy(gameObject);
    }

    IEnumerator drain(int desX, int desY)
    {
        SetSpiritAnimationDelay(0.2f);
        float goalX = XBasicLoc + (desX * XInterval);
        float goalY = YBasicLoc + (desY * YInterval);

        StartCoroutine(SmoothMovement(new Vector2(goalX, goalY)));
        GameManager.instance.InGameScript.playerSpiritList.Remove(gameObject.GetComponent<PlayerSpirit>());

        yield return new WaitForSeconds(0.2f);

        GameManager.instance.map[desX, desY].GetComponent<Player>().playerStat.Tier++;
        GameManager.instance.map[desX, desY].GetComponent<Player>().UpdateStat();
        GameManager.instance.map[desX, desY].GetComponent<Player>().playerStat.heal();

        Destroy(gameObject);
    }

    IEnumerator killMove(int desX, int desY, int dirX, int dirY)
    {
        Debug.Log("죽이고 이동");
        SetAnimationDelay(0.4f + moveAnimeDelay);
        SetSpiritAnimationDelay(0.4f + moveAnimeDelay);
        base.Move(LocX, LocY, desX, desY);
        LocX = desX;
        LocY = desY;
        desX += dirX;
        desY += dirY;
        yield return null;
    }

    IEnumerator Attack(int desX, int desY, bool skill, float time)
    {
        SetAnimationDelay(time + 0.4f);
        SetSpiritAnimationDelay(time + 0.4f);
        if (time != 0)
        {
            yield return new WaitForSeconds(time);
        }
        Attack(playerStat.Damage, LocX, LocY, desX, desY, playerStat.CriticalDamage, playerStat.Critical, skill);

        anime.SetTrigger("attack");

    }
    public void UpdateStat()
    {
        UpdateTier();
        playerStat.setStat();
    }

    void Update()
    {
        if (hpBar.fillAmount != hpCurrentFill)
        {
            hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hpCurrentFill, Time.deltaTime * lerpSpeed);
        }
    }

    public override void Hurt(int hitDamage)
    {
        Debug.Log("분신 피가 담");
        base.Hurt(hitDamage);
        hpCurrentFill = (float)playerStat.Hp / playerStat.MaxHp;
        anime.SetTrigger("hit");
        if (playerStat.Hp <= 0)
        {
            playerStat.Hp = 0;
            anime.SetBool("dead", true);
            StartCoroutine(dead());
        }
    }
    
    public IEnumerator dead()
    {
        //플레이어가 강화되는 스크립트

        GameManager.instance.InGameScript.playerSpiritList.Remove(gameObject.GetComponent<PlayerSpirit>());
        Destroy(gameObject);
        yield return null;
    }

}
