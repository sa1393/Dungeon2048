using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Player : PlayerBase
{
    public List<Skill> skillList;

    int[] currentSkill = new int[4]; //적용된 스킬 아이디

    public UIManager uiManager;

    [SerializeField]
    public Image tierImage;


    public List<Weapon> weaponList;

    int weaponCool;
    int uiLevel = 1;

    public int[] CurrentSkill { get => currentSkill; set => currentSkill = value; }

    protected override void Awake()
    {
        base.Awake();
        weaponList = GetComponentInChildren<WeaponManager>().weaponList;
    }

    void Start()
    {
        skillList = GetComponentInChildren<SkillManager>().skillList;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        CurrentWeaponId = PlayerData.instance.player.GetComponent<Player>().CurrentWeaponId;
        transform.GetChild(0).GetComponent<WeaponManager>().ApplySprite(CurrentWeaponId);
        uiManager.weaponIcon.sprite = weaponList[CurrentWeaponId].sprite;

        playerStat.setStat();
        playerStat.LevelUp();

        for(int i = 0; i < 4; i++)
        {
            CurrentSkill[i] = skillList.Count - 1;
        }

        uiManager.Player = this;
        uiManager.setBar();
        uiManager.SetSkillSprite();
        playerStat.Tier = 1;
        UpdateTier();
        setWeapon();
        uiManager.SetWeaponUI();
    }

    public void UpdateTier()
    {
        int tier = 0;
        if (playerStat.Tier - 1 < 10)
            tier = playerStat.Tier - 1;
        else
            tier = 9;
        tierImage.sprite = GameManager.instance.tiers[tier];
    }

    public IEnumerator UseSkill(int skillNum, int goalX, int goalY)
    {
        if (skillList[CurrentSkill[skillNum]].UseMp > playerStat.Mp)
        {

        }
        else if (skillList[CurrentSkill[skillNum]].NowCoolTime == 0)
        {
            if(skillList[CurrentSkill[skillNum]].Active == 1)
            {
                Debug.Log("버프");
                skillList[CurrentSkill[skillNum]].Use(goalX, goalY);

                playerStat.Mp -= skillList[currentSkill[skillNum]].UseMp;
                uiManager.setBar();
                yield return null;
            }
            else if (GameManager.instance.map[goalX, goalY] != null)
            {
                if (GameManager.instance.map[goalX, goalY].GetComponent<Player>() == null)
                {
                    int disX = Mathf.Abs(PlayerData.instance.player.GetComponent<Player>().LocX - goalX);
                    int disY = Mathf.Abs(PlayerData.instance.player.GetComponent<Player>().LocY - goalY);
                    if(disX <= skillList[CurrentSkill[skillNum]].Cross && disY <= skillList[CurrentSkill[skillNum]].Cross)
                    {
                        skillList[CurrentSkill[skillNum]].Use(goalX, goalY);

                        playerStat.Mp -= skillList[currentSkill[skillNum]].UseMp;
                        uiManager.setBar();
                        yield return null;
                    }
                }
            }
        }
    }

    public void MoveAndAttack(int dirX, int dirY)
    {
        if (dirY == 1)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        if(dirX == -1)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }

        this.dirX = dirX;
        this.dirY = dirY;

        anime.SetFloat("dirX", dirX);
        anime.SetFloat("dirY", dirY);

        if (playerStat.Mp > playerStat.MaxMp) playerStat.Mp = playerStat.MaxMp;

        SetBar();

        int desX = LocX + dirX;
        int desY = LocY + dirY;

        for (int i = 0; i < 4; i++)
        {
            if (skillList[CurrentSkill[i]].Active == 2)
            {
                skillList[CurrentSkill[i]].Use(0, 0);
            }
        }

        if (CheckWall(desX, desY))
        {
            return; //벽
        }
        else if (CheckSpirit(desX, desY))
        {
            if (CheckTier(desX, desY))
            {
                
                StartCoroutine(Promotion(desX, desY));
                for (int i = 0; i < 4; i++)
                {
                    if (skillList[CurrentSkill[i]].NowCoolTime != 0)
                    {
                        skillList[CurrentSkill[i]].NowCoolTime--; //스킬쿨 감소
                    }
                }
            }
        }
        else
        {
            MoveAttack(dirX, dirY);
            for (int i = 0; i < 4; i++)
            {
                if (skillList[CurrentSkill[i]].NowCoolTime != 0)
                {
                    skillList[CurrentSkill[i]].NowCoolTime--; //스킬쿨 감소
                }
            }
            if (playerStat.Mp > playerStat.MaxMp) playerStat.Mp = playerStat.MaxMp;
        }

        SetBar();
    }

    public override void MoveAttack(int dirX, int dirY)
    {

        int desX = dirX + LocX;
        int desY = dirY + LocY;

        if (CheckWall(desX, desY))
        {
            return; //벽
        }

       
        if(playerStat.MoveMent == playerStat.MaxMoveMent)
        {
            uiManager.SetWeaponUI();
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
                else if (BonusMove)
                {
                    if (CheckMapNull(desX, desY))
                    {
                        base.Move(LocX, LocY, desX, desY);
                        LocX = desX;
                        LocY = desY;
                        desX += dirX;
                        desY += dirY;
                        BonusMove = false;
                    }
                }

                if (CheckEnemy(desX, desY)) //이동하고 공격
                {
                    playerStat.MoveMent = 0;
                    uiManager.SetWeaponUI();
                    playerStat.Mp += 5;
                    if (weaponList[CurrentWeaponId].weaponType == Weapon.WeaponType.GreatSword ||
                        weaponList[CurrentWeaponId].weaponType == Weapon.WeaponType.Spear) //창, 대검
                    {
                        StartCoroutine(Attack(desX, desY, false, attackDelay));
                        if (desX + dirX > 4 || desX + dirX < 0 || desY + dirY > 4 || desY + dirY < 0) { } // 벽
                        else
                        {
                            if (CheckEnemy(desX + dirX, desY + dirY))
                            {
                                StartCoroutine(Attack(desX + dirX, desY + dirY, true, attackDelay));
                            }
                        }

                    }
                    else if (weaponList[CurrentWeaponId].weaponType == Weapon.WeaponType.Axe) // 도끼 보류
                    {
                        StartCoroutine(AttackMotion());
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                if (i != 0 && j != 0) continue;
                                if (!CheckWall(LocX + i, LocY + j))
                                {
                                    if (CheckEnemy(LocX + i, LocY + j))
                                    {
                                        weaponList[0].Attack(playerStat.Damage, LocX, LocY, LocX + i, LocY + j, playerStat.CriticalDamage, playerStat.Critical, false);

                                    }
                                }
                            }
                        }
                    }
                    else // 나머지
                    {
                        StartCoroutine(Attack(desX, desY, false, attackDelay));
                    }
                }
                else
                {
                    SetAnimationDelay(moveAnimeDelay);
                }
            }
            else if (CheckEnemy(desX, desY))// 적 바로 공격
            {
                playerStat.Mp += 5;
                playerStat.MoveMent = 0;
                uiManager.SetWeaponUI();


                if (weaponList[CurrentWeaponId].weaponType == Weapon.WeaponType.GreatSword ||
                        weaponList[CurrentWeaponId].weaponType == Weapon.WeaponType.Spear) //창, 대검
                {
                    StartCoroutine(Attack(desX, desY, false, 0f));
                    if (CheckMapNull(desX, desY))
                    {
                        StartCoroutine(killMove(desX, desY, dirX, dirY));
                    }

                    if (desX + dirX > 4 || desX + dirX < 0 || desY + dirY > 4 || desY + dirY < 0) { } // 벽
                    else
                    {
                        if (CheckEnemy(desX + dirX, desY + dirY))
                        {
                            StartCoroutine(Attack(desX + dirX, desY + dirY, false, 0f));
                        }
                    }
                }
                else if (weaponList[CurrentWeaponId].weaponType == Weapon.WeaponType.Axe) //도끼 보류
                {
                    StartCoroutine(AttackMotion());
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i != 0 && j != 0) continue;
                            if (!CheckWall(LocX + i, LocY + j))
                            {
                                if (CheckEnemy(LocX + i, LocY + j))
                                {
                                    weaponList[0].Attack(playerStat.Damage, LocX, LocY, LocX + i, LocY + j, playerStat.CriticalDamage, playerStat.Critical, false);
                                }
                            }
                        }
                    }

                }
                else //나머지
                {
                    StartCoroutine(Attack(desX, desY, false, 0f));
                    if (CheckMapNull(desX, desY))
                    {
                        StartCoroutine(killMove(desX, desY, dirX, dirY));
                    }

                }
            }
        }
        else
        {
            playerStat.MoveMent++;
            uiManager.SetWeaponUI();

            if (CheckMapNull(desX, desY))
            {
                base.Move(LocX, LocY, desX, desY);
                LocX = desX;
                LocY = desY;
                desX += dirX;
                desY += dirY;
                if (BonusMove)
                {
                    if (CheckMapNull(desX, desY))
                    {
                        base.Move(LocX, LocY, desX, desY);
                        LocX = desX;
                        LocY = desY;
                        desX += dirX;
                        desY += dirY;
                        BonusMove = false;
                    }
                }
            }
            SetAnimationDelay(moveAnimeDelay);

            return;
        }
    }

    IEnumerator Promotion(int desX, int desY) // 승급
    {
        SetAnimationDelay(0.25f);
        GameObject temp = GameManager.instance.map[desX, desY].gameObject;

        base.Move(locX, locY, desX, desY);
        locX = desX;
        locY = desY;
        yield return new WaitForSeconds(0.2f);
        
        StartCoroutine(temp.GetComponent<PlayerSpirit>().dead());

        playerStat.Tier++;
        UpdateTier();
        playerStat.setStat();
        playerStat.heal();
    }
    IEnumerator Attack(int desX, int desY, bool skill, float time)
    {
        SetAnimationDelay(time + 0.4f);
        SetSpiritAnimationDelay(time + 0.4f);
        if (time != 0)
        {
            yield return new WaitForSeconds(time);
        }
        weaponList[0].Attack(playerStat.Damage + BonusDamage, LocX, LocY, desX, desY, playerStat.CriticalDamage, playerStat.Critical + BonusCritical, skill);
        BonusCritical = 0;
        BonusDamage = 0;

        StartCoroutine(AttackMotion());
    }

    public void UpdateStat()
    {
        UpdateTier();
        playerStat.setStat();
        uiManager.setBar();
    }

    public void setWeapon()
    {
        GetComponentInChildren<WeaponManager>().ApplySprite(CurrentWeaponId);
        playerStat.MaxMoveMent = weaponList[CurrentWeaponId].MoveMent;
        playerStat.MoveMent = playerStat.MaxMoveMent;
        uiManager.SetWeaponUI();
    }

    public void SetSkill()
    {
        uiManager.SetSkillSprite();
        SetBar();
    }
    public void SetBar()
    {
        uiManager.setBar();
    }
    public void getExp(int exp)
    {
        this.playerStat.Exp += exp;
        if(this.playerStat.Exp >= playerStat.MaxExp)
        {
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        playerStat.Level++;
        uiManager.Level.text = "Lv " + playerStat.Level;

        playerStat.Exp -= playerStat.MaxExp;
        playerStat.LevelUp();

        yield return null;
    }

    public void LevelUI()
    {
        if(uiLevel < playerStat.Level)
        {
            GameManager.instance.InUI = true;

            if ((playerStat.Level == 2 || playerStat.Level == 5 || playerStat.Level == 12 || playerStat.Level == 20) && playerStat.Level <= 21)
            {
                if (GameManager.instance.doTutorial == 0)
                    uiManager.SelectSkillStart();

            }
            else
            {
                uiManager.SelectWeaponStart();
            }

            Debug.Log("ui레벨업");
            if(GameManager.instance.doTutorial == 0)
                GameManager.instance.InGameScript.CreatePlayerSpirit();
            uiLevel++;
        }
    }


    
    public override void Hurt(int hitDamage)
    {
        base.Hurt(hitDamage);
        anime.SetTrigger("hit");
        uiManager.setBar();
        if (playerStat.Hp <= 0)
        {
            GetComponentInChildren<WeaponManager>().ApplySprite(weaponList.Count);
            playerStat.Hp = 0;
            uiManager.setBar();
            StartCoroutine(GameManager.instance.GameOver());
            Destroy(GetComponentInChildren<SkillManager>().gameObject);
            anime.SetBool("dead", true);
        }
    }
    
}
