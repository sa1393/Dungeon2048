using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Slime : Enemy
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
    }

    private void Start()
    {
        this.name = "slime";
    }

    public override void SetStat(int evolution, int tier, string how)
    {
        base.SetStat(evolution, tier, how);
        anime.SetFloat("ev", (evolution - 1) * 0.5f);

        if(evolution >= 3) 
        enemyStat.Id = 0;
        name = "slime";
        this.enemyStat.Evolution = evolution;
        this.enemyStat.Tier = tier;
        this.enemyStat.Rerity = 1;

        enemyStat.MaxMoveMent = 1;
        enemyStat.MoveMent = 0;

        if(evolution == 1)
        {
            exp = 25;
            enemyStat.Damage = 20 + (int)(20 * 0.025f * tier);
            enemyStat.MaxHp = 50 + (int)(50 * 0.02f * tier);
            enemyStat.Critical = 0.1f;
            enemyStat.Defense = 0.1f;
            enemyStat.CriticalDamage = 1;
            enemyStat.Money = 200;

        }
        else if(evolution == 2)
        {
            exp = 40;
            enemyStat.Damage = 27 + (int)(27 * 0.03f * tier);
            enemyStat.MaxHp = 120 + (int)(120 * 0.03f * tier);
            enemyStat.Critical = 0.125f;
            enemyStat.Defense = 0.2f;
            enemyStat.CriticalDamage = 1.05f;
            enemyStat.Money = 300;
        }
        else
        {
            this.enemyStat.Evolution = 3;
            exp = 60;
            enemyStat.Damage = 35 + (int)(35 * 0.035f * tier);
            enemyStat.MaxHp = 250 + (int)(250 * 0.04f * tier);
            enemyStat.Critical = 0.15f;
            enemyStat.Defense = 0.3f;
            enemyStat.CriticalDamage = 1.1f;
            enemyStat.Money = 450;
        }
        enemyStat.Hp = enemyStat.MaxHp;

    }

    public override void Move(int dirX, int dirY)
    {
        base.Move(dirX, dirY);
    }

    public override IEnumerator Hurt(int hitDamage, bool skill, int dirX, int dirY)
    {
        //int resultHitDamage = hitDamage - (hitDamage / 100 * Defense);
        //Hp -= resultHitDamage;
        enemyStat.hit(hitDamage);
        if(!skill)
            enemyStat.MoveMent--;

        if (enemyStat.Hp < 0) enemyStat.Hp = 0;
        HpCurrentFill = (float)enemyStat.Hp / enemyStat.MaxHp;

        if (enemyStat.Hp <= 0) // 죽으면 true
        {
            SetCounterAnimeDelay(0f);
            GameManager.instance.map[LocX, LocY] = null;
            PlayerData.instance.player.GetComponent<Player>().getExp(exp + (int)(exp *SoulLinkManager.instance.PlusExp));
            PlayerData.instance.player.GetComponent<Player>().SetBar();
            GameManager.instance.InGameScript.GetMoney(enemyStat.Money);
            anime.SetTrigger("dead");

            yield return new WaitForSeconds(0.5f);
            GameManager.instance.InGameScript.enemyList.Remove(gameObject.GetComponent<Enemy>());
            Destroy(gameObject);
        }
        else
        {
            SetCounterAnimeDelay(0f);
            anime.SetTrigger("hit");
            yield return new WaitForSeconds(0.5f);
            if (!skill)
            {
                anime.SetTrigger("attack");
                if (CheckPlayer(LocX + dirX, LocY + dirY))
                {
                    GameManager.instance.map[LocX + dirX, LocY + dirY].GetComponent<Player>().Hurt(enemyStat.Damage);
                }
                else if (CheckSpirit(LocX + dirX, LocY + dirY))
                {
                    GameManager.instance.map[LocX + dirX, LocY + dirY].GetComponent<PlayerSpirit>().Hurt(enemyStat.Damage);
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
