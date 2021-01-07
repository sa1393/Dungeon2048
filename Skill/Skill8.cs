using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill8 : Skill
{
    public Skill8(int id, string name, string info, int damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {
    }

    public override void Use(int goalX, int goalY)
    {
        Passive(goalX, goalY);
    }

}
