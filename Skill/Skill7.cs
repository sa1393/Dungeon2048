using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill7 : Skill
{
    public Skill7(int id, string name, string info, int damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {
    }

    public override void Use(int goalX, int goalY)
    {
        Passive(goalX, goalY);
        Debug.Log("패시브 되나");
        int ran = Random.Range(1, 101);
        if (ran <= 100)
        {
            PlayerData.instance.player.GetComponent<Player>().BonusMove = true;
        }
    }

}
