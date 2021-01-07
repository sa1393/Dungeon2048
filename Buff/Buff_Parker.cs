using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Parker : Buff
{
    public Buff_Parker(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[4];
        EnemyScript.enemyStat.state = EnemyStat.State.parker;
    }

    public override void Use()
    {
        base.Use();
    }
}
