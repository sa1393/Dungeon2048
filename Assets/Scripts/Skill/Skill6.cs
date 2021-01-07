using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill6 : Skill
{
    public Skill6(int id, string name, string info, int damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {
    }

    public override void Use(int goalX, int goalY)
    {
        Buff(goalX, goalY);
    }
}
