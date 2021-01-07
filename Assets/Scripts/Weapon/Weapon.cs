using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Weapon{
    int id = 0;
    public string name = "";
    int attackDamage = 0;
    int rating = 0;
    float weaponCritical = 0;
    int moveMent = 0;
    string info;

    public WeaponType weaponType;

    public Sprite sprite;

    public int MoveMent { get => moveMent; set => moveMent = value; }
    public string Info { get => info; set => info = value; }
    public int Rating { get => rating; set => rating = value; }

    public Weapon(string name, int id, int attackDamage, WeaponType weaponType, int rating, string info)
    {
        this.name = name;
        this.id = id;
        this.attackDamage = attackDamage;
        this.weaponType = weaponType;
        this.Rating = rating;

        sprite = GameManager.instance.weaponIcons[id];

        switch (weaponType)
        {
            case WeaponType.Sword:
                weaponCritical = 0.2f;
                MoveMent = 0;
            break;

            case WeaponType.GreatSword:
                weaponCritical = 0.2f;
                MoveMent = 1;
            break;

            case WeaponType.Dagger:
                weaponCritical = 0.5f;
                MoveMent = 0;
            break;

            case WeaponType.Hammer:
                weaponCritical = 0.1f;
                MoveMent = 0;
            break;

            case WeaponType.Axe:
                weaponCritical = 0.1f;
                MoveMent = 2;
            break;

            case WeaponType.Spear:
                weaponCritical = 0.2f;
                MoveMent = 0;
                break;
        }
        this.info = "공격력 : " + attackDamage + "\n" + "무기 대기시간 : " + MoveMent + "턴";
    }

    public enum WeaponType
    {
        Sword,
        GreatSword,
        Dagger,
        Axe,
        Hammer,
        Spear
    }

    public virtual void Attack(int damage, int locX, int locY, int desX, int desY, float criticalDamage, float critical, bool skill) //공격 실행 후 이동 가능 반환
    {
        float resultCritical = critical + weaponCritical;
        if (resultCritical > 1) resultCritical = 1;
        int ran = Random.Range(1, 101);
        int resultDamage;

        if (GameManager.instance.doTutorial == 1)
        {
            resultCritical = 0;
        }

        if (ran <= (resultCritical*100))
        {
            resultDamage = (int)(((damage + attackDamage)) + ((damage + attackDamage) * criticalDamage));
        }
        else
        {
            resultDamage = (damage + attackDamage);
        }

        GameManager.instance.StartCoroutine(GameManager.instance.map[desX, desY].GetComponent<Enemy>().Hurt(resultDamage, skill, locX - desX, locY - desY));
    }
}