using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    int playerLevel;

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
        playerLevel = 1;
        //playerLevel = GameObject.Find("Player(Clone)").GetComponent<Player>().Level;
        this.name = "skeleton";
    }

    public override void SetStat(int evolution, int tier, string how)
    {
        base.SetStat(evolution, tier, how);
        anime.SetFloat("ev", (evolution - 1) * 0.5f);

        if (evolution >= 3) this.enemyStat.Evolution = 3;
        enemyStat.Id = 2;
        name = "skeleton";
        enemyStat.Evolution = evolution;
        enemyStat.Tier = tier;
        enemyStat.Rerity = 1;

        enemyStat.Damage = ((playerLevel * tier) + (evolution * tier) + (1* evolution)) * 10;
        enemyStat.Critical = evolution * 10;
        enemyStat.CriticalDamage = 2;
        // MaxHp = ((playerLevel * 4) + (evolution * tier * tier)) * 10;
        enemyStat.MaxHp = 50;
        enemyStat.Hp = enemyStat.MaxHp;
        enemyStat.Defense = evolution * 10;
        enemyStat.MaxMoveMent = 1;
        enemyStat.MoveMent = 0;
        exp = enemyStat.Rerity + tier + evolution;
    }

    public override void Move(int dirX, int dirY)
    {
        base.Move(dirX, dirY);
    }

    public override IEnumerator Hurt(int hitDamage, bool skill, int dirX, int dirY)
    {

        if (enemyStat.Hp <= 0)
        {
            int passive = Random.Range(1, 101);
            if(passive < enemyStat.Evolution * 10)
            {
                Debug.Log("스켈레톤이 공격을 막음");
            }
            else
            {
                StartCoroutine(base.Hurt(hitDamage, skill, dirX, dirY));
            }
        }
        else
        {
            StartCoroutine(base.Hurt(hitDamage, skill, dirX, dirY));
        }
        yield return null;
        
    }
}
