using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Ice : Buff
{
    public Buff_Ice(int turn, Enemy enemyScript) : base(turn, enemyScript)
    {
        Sprite = GameManager.instance.buffIcons[0];
        if (EnemyScript.enemyStat.state == EnemyStat.State.ice)
            EnemyScript.Die();
        else
        {
            enemyScript.enemyStat.state = EnemyStat.State.ice;
        }
        enemyScript.buff_Img.sprite = Sprite;
    }

    public override void Use()
    {
        base.Use();
    }

}
