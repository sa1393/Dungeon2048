using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    int playerLevel;
    int a;
    int ab;

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
        //playerLevel = GameManager.instance.player.
        playerLevel = 1;
        name = "goblin";
    }

    public override void SetStat(int evolution, int tier, string how)
    {
        base.SetStat(evolution, tier, how);
        anime.SetFloat("ev", (evolution - 1) * 0.5f);


        enemyStat.Id = 1;
        this.name = "goblin";
        this.enemyStat.Evolution = evolution;
        this.enemyStat.Tier = tier;

        enemyStat.Damage = ((playerLevel * tier) + (evolution * tier) + (2 * evolution)) * 10;
        enemyStat.Critical = evolution * 15;
        enemyStat.CriticalDamage = 2.5f;
        enemyStat.MaxHp = ((playerLevel * 3) + (evolution * tier * tier)) * 10;
        enemyStat.Hp = enemyStat.MaxHp;
        enemyStat.Defense = 0;
        enemyStat.MaxMoveMent = 1;
        enemyStat.MoveMent = 0;

        exp = enemyStat.Rerity + tier + evolution;
    }
}
