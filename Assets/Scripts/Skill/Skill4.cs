using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4 : Skill
{
    public Skill4(int id, string name, string info, float damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {

    }

    public override void Use(int goalX, int goalY)
    {
        Buff(goalX, goalY);
        PlayerData.instance.player.GetComponent<Player>().playerStat.MoveMent = PlayerData.instance.player.GetComponent<Player>().playerStat.MaxMoveMent - 1;
    }

}