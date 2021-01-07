using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MovingObject
{
    protected int dirX = 0;
    protected int dirY = -1;

    protected float moveAnimeDelay = 0.2f;  //움직이기만 했을 때 키 딜레이
    protected float attackDelay = 0.2f; //이동하고 적 공격 딜레이

    int currentWeaponId = 0; //적용된 무기 아이디

    protected Animator anime;

    public PlayerStat playerStat;

    float bonusCritical = 0;
    int bonusDamage = 0;
    bool bonusMove = false;

    public int CurrentWeaponId { get => currentWeaponId; set => currentWeaponId = value; }
    public int BonusDamage { get => bonusDamage; set => bonusDamage = value; }
    public float BonusCritical { get => bonusCritical; set => bonusCritical = value; }
    public bool BonusMove { get => bonusMove; set => bonusMove = value; }

    protected virtual void Awake()
    {
        anime = GetComponent<Animator>();
        playerStat = new PlayerStat();
    }

    public virtual void MoveAttack(int dirX, int dirY)
    {
       
       
    }

    protected IEnumerator killMove(int desX, int desY, int dirX, int dirY)
    {
        Debug.Log("죽이고 이동");
        SetAnimationDelay(0.4f + moveAnimeDelay);
        SetSpiritAnimationDelay(0.4f + moveAnimeDelay);
        base.Move(LocX, LocY, desX, desY);
        LocX = desX;
        LocY = desY;
        desX += dirX;
        desY += dirY;
        yield return null;
    }

    protected IEnumerator AttackMotion()
    {
        anime.SetTrigger("attack");
        transform.GetChild(0).GetComponent<WeaponManager>().DeleteSprite();
        yield return new WaitForSeconds(0.4f);
        if (GameManager.instance.InGame == true)
            transform.GetChild(0).GetComponent<WeaponManager>().ApplySprite(CurrentWeaponId);
    }


    public virtual void Hurt(int hitDamage)
    {
        playerStat.Hp -= hitDamage;
    }
}
