using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Blood : Buff
{
    
    public Buff_Blood(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[5];
        EnemyScript.enemyStat.state = EnemyStat.State.blood;

    }

    public override void Use()
    {
        base.Use();
        //수치모름
    }
}
