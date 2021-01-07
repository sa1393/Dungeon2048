using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill9 : Skill
{
    public Skill9(int id, string name, string info, int damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {

    }

    public override void Use(int goalX, int goalY)
    {
        Passive(goalX, goalY);
        int ran = Random.Range(1, 101);
        if (ran <= 10)
        {
            PlayerData.instance.player.GetComponent<Player>().playerStat.MoveMent--;
        }
    }
}
