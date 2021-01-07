using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Fire : Buff
{
    public Buff_Fire(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[1];
        enemyScript.enemyStat.state = EnemyStat.State.fire;
    }

    public override void Use()
    {
        base.Use();
        EnemyScript.enemyStat.Hp -= (int)(EnemyScript.enemyStat.Hp * 0.1f);
    }
}
