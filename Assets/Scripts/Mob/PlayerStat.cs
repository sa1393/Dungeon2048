using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharcterStat
{
    [SerializeField]
    int maxMp;
    int mp; // 마나
    int level; //레벨
    int maxExp;
    int exp; //경험치

    public int MaxMp { get => maxMp; set => maxMp = value; }
    public int Mp { get => mp; set => mp = value; }
    public int Level { get => level; set => level = value; }
    public int MaxExp { get => maxExp; set => maxExp = value; }
    public int Exp { get => exp; set => exp = value; }

    public PlayerStat()
    {
        level = 1;
        setStat();
        heal();
        MoveMent = 0;
        Hp = MaxHp;
    }

    public void setStat() //스텟 업데이트
    {
        Damage = (10 * Tier) + (int)(10f * Tier * SoulLinkManager.instance.AttackDamage);
        Critical = 0.01f * Tier + SoulLinkManager.instance.Critical;
        CriticalDamage = 1 + (0.5f * Tier) + SoulLinkManager.instance.CriticalDamage;
        MaxHp = (100 * Tier) + (int)(100 * Tier * SoulLinkManager.instance.MaxHp);
        MaxHp += (int)(MaxHp * (0.032 + (Tier * 0.002))) * (level-1);
        Defense = 0.01f * Tier + SoulLinkManager.instance.Defense;
        maxMp = 100 + ((level / 10) * 5) + SoulLinkManager.instance.MaxMp;
        maxExp = 100 + (int)(100 * (level-1) * 0.04);
    }

    public void heal()
    {
        Hp = MaxHp;
        mp = maxMp;
    }

    public void LevelUp()
    {
        setStat();
        heal();
    }
}
