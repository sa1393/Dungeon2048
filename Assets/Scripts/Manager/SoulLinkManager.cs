using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLinkManager : MonoBehaviour
{
    public static SoulLinkManager instance = null;

    int attackDamageLevel = 0; //강화 레벨
    float attackDamage = 0; //공격력 %
    int attackDamageCost = 0; //비용

    int criticalLevel = 0;
    float critical = 0;
    int criticalCost = 0;

    int criticalDamageLevel = 0;
    float criticalDamage = 0;
    int criticalDamageCost = 0;

    int defenseLevel = 0;
    float defense = 0;
    int defenseCost = 0;

    int maxHpLevel = 0;
    float maxHp = 0; //체력 %
    int maxHpCost = 0;

    int maxMpLevel = 0;
    int maxMp = 0;
    int maxMpCost = 0;

    int plusExpLevel = 0;
    float plusExp = 0;
    int plusExpCost = 0;

    public int AttackDamageLevel { get => attackDamageLevel; set => attackDamageLevel = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int AttackDamageCost { get => attackDamageCost; set => attackDamageCost = value; }
    public int CriticalLevel { get => criticalLevel; set => criticalLevel = value; }
    public float Critical { get => critical; set => critical = value; }
    public int CriticalCost { get => criticalCost; set => criticalCost = value; }
    public int CriticalDamageLevel { get => criticalDamageLevel; set => criticalDamageLevel = value; }
    public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    public int CriticalDamageCost { get => criticalDamageCost; set => criticalDamageCost = value; }
    public int DefenseLevel { get => defenseLevel; set => defenseLevel = value; }
    public float Defense { get => defense; set => defense = value; }
    public int DefenseCost { get => defenseCost; set => defenseCost = value; }
    public int MaxHpLevel { get => maxHpLevel; set => maxHpLevel = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }
    public int MaxHpCost { get => maxHpCost; set => maxHpCost = value; }
    public int MaxMpLevel { get => maxMpLevel; set => maxMpLevel = value; }
    public int MaxMp { get => maxMp; set => maxMp = value; }
    public int MaxMpCost { get => maxMpCost; set => maxMpCost = value; }
    public int PlusExpLevel { get => plusExpLevel; set => plusExpLevel = value; }
    public float PlusExp { get => plusExp; set => plusExp = value; }
    public int PlusExpCost { get => plusExpCost; set => plusExpCost = value; }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //소울링크 레벨 로드

        attackDamageLevel = PlayerPrefs.GetInt("attackDamageLevel", 0);
        criticalLevel = PlayerPrefs.GetInt("criticalLevel", 0);
        criticalDamageLevel = PlayerPrefs.GetInt("criticalDamageLevel", 0);
        defenseLevel = PlayerPrefs.GetInt("defense", 0);
        maxHpLevel = PlayerPrefs.GetInt("maxHp", 0);
        maxMpLevel = PlayerPrefs.GetInt("maxHp", 0);
        plusExpLevel =  PlayerPrefs.GetInt("plusExp", 0);

        SetSoul();

    }

    public void SetSoul() //수치 업데이트
    {
        AttackDamage = AttackDamageLevel * 0.05f;
        Critical = CriticalLevel * 0.02f;
        CriticalDamage = CriticalDamageLevel * 0.1f;
        Defense = DefenseLevel * 0.02f;
        MaxHp = MaxHpLevel * 0.1f;
        MaxMp = MaxMpLevel * 10;
        PlusExp = PlusExpLevel * 0.05f;

        AttackDamageCost = (AttackDamageLevel + 1) * (5000 + 2500 * AttackDamageLevel);

        switch (CriticalLevel)
        {
            case 0:
                CriticalCost = 10000;
                break;
            case 1:
                CriticalCost = 40000;
                break;
            case 2:
                CriticalCost = 75000;
                break;
            case 3:
                CriticalCost = 115000;
                break;
            case 4:
                CriticalCost = 160000;
                break;
        }
        switch (CriticalDamageLevel)
        {
            case 0:
                CriticalDamageCost = 7500;
                break;
            case 1:
                CriticalDamageCost = 22500;
                break;
            case 2:
                CriticalDamageCost = 45000;
                break;
            case 3:
                CriticalDamageCost = 75000;
                break;
            case 4:
                CriticalCost = 112500;
                break;
        }
        switch (DefenseLevel)
        {
            case 0:
                DefenseCost = 7500;
                break;
            case 1:
                DefenseCost = 22500;
                break;
            case 2:
                DefenseCost = 45000;
                break;
            case 3:
                DefenseCost = 75000;
                break;
            case 4:
                DefenseCost = 112500;
                break;
        }
        switch (MaxHpLevel)
        {
            case 0:
                MaxHpCost = 10000;
                break;
            case 1:
                MaxHpCost = 40000;
                break;
            case 2:
                MaxHpCost = 75000;
                break;
            case 3:
                MaxHpCost = 1150000;
                break;
            case 4:
                MaxHpCost = 160000;
                break;
        }

        switch (MaxMpLevel)
        {
            case 0:
                MaxMpCost = 10000;
                break;
            case 1:
                MaxMpCost = 30000;
                break;
            case 2:
                MaxMpCost = 60000;
                break;
            case 3:
                MaxMpCost = 100000;
                break;
            case 4:
                MaxMpCost = 150000;
                break;
        }

        switch (PlusExpLevel)
        {
            case 0:
                PlusExpCost = 15000;
                break;
            case 1:
                PlusExpCost = 50000;
                break;
            case 2:
                PlusExpCost = 85000;
                break;
            case 3:
                PlusExpCost = 120000;
                break;
            case 4:
                PlusExpCost = 175000;
                break;
        }
    }


}
