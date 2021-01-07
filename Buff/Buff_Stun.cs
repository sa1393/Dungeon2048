using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stun : Buff
{
    public Buff_Stun(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[3];
        EnemyScript.enemyStat.state = EnemyStat.State.stun;
    }

    public override void Use()
    {
        base.Use();
    }
}
