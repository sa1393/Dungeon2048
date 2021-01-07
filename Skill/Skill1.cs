using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill
{
    public Skill1(int id, string name, string info, float damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {

    }

    public override void Use(int goalX, int goalY)
    {
        Act(goalX, goalY);
        Enemy enemy = GameManager.instance.map[goalX, goalY].GetComponent<Enemy>();
        enemy.AddBuff(new Buff_Ice(2, enemy));
    }
}
