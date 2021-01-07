using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    int turn; //남은 턴
    Sprite sprite;
    Enemy enemyScript;

    public int Turn { get => turn; set => turn = value; }
    public Enemy EnemyScript { get => enemyScript; set => enemyScript = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }

    public Buff(int turn, Enemy enemyScript)
    {
        this.turn = turn;
        this.EnemyScript = enemyScript;
    }

    public virtual void Use()
    {
        turn--;
        //대충
    }
}
