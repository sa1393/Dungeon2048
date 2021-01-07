using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Silence : Buff
{
    public Buff_Silence(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[7];
        EnemyScript.enemyStat.state = EnemyStat.State.silence;
    }

    public override void Use()
    {
        base.Use();
    }
}
