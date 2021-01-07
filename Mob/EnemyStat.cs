using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharcterStat
{
    int id = 0; // id

    int evolution = 1; //진화단계
    int rerity = 0; //희귀도
    int money = 0;
    public State state;

    public int Id { get => id; set => id = value; }

    public int Evolution { get => evolution; set => evolution = value; }
    public int Rerity { get => rerity; set => rerity = value; }
    public int Money { get => money; set => money = value; }

    public EnemyStat()
    {
        state = State.none;
    }

    public enum State
    {
        none, //기본
        ice,  //빙결
        fire, //화상
        shock, //감전
        stun, //기절
        parker, //속박
        blood, //출혈
        curse, //저주
        silence //침묵
    }
}
