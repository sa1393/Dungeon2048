using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Curse : Buff
{
    public Buff_Curse(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[6];
        EnemyScript.enemyStat.state = EnemyStat.State.curse;
    }

    public override void Use()
    {
        base.Use();
        //수치 모름
    }
}
