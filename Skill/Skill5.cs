using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5 : Skill
{
    public Skill5(int id, string name, string info, float damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {

    }

    public override void Use(int goalX, int goalY)
    {
        Buff(goalX, goalY);
        PlayerData.instance.player.GetComponent<Player>().BonusDamage = (int)(PlayerData.instance.player.GetComponent<Player>().playerStat.Damage * 0.3f);

    }
}