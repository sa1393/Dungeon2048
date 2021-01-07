using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Shock : Buff
{
    public Buff_Shock(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[2];
        EnemyScript.enemyStat.state = EnemyStat.State.shock;
    }

    public override void Use()
    {
        base.Use();
        EnemyScript.enemyStat.MoveMent--;
    }
}
