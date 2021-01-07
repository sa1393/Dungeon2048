using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterStat : MonoBehaviour
{
	[SerializeField]
	int maxHp = 0;

	int hp = 0; //체력
	int tier = 1; //티어

	float defense = 0; //방어력

	[SerializeField]
	int damage = 0; //공격력

	float critical = 0; //크리티컬 확률
	float criticalDamage; //크리티컬 데미지 배율

	int maxMoveMent = 1; //이동 빈도
	int moveMent = 0; //현재 이동빈도

	public int Hp { get => hp; set => hp = value; }
	public int MaxHp { get => maxHp; set => maxHp = value; }
	public int Damage { get => damage; set => damage = value; }
	public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
	public int MoveMent { get => moveMent; set => moveMent = value; }
	public int MaxMoveMent { get => maxMoveMent; set => maxMoveMent = value; }
	public int Tier { get => tier; set => tier = value; }
	public float Critical { get => critical; set => critical = value; }
	public float Defense { get => defense; set => defense = value; }

	public void hit(int damage)
	{
		Hp -= damage;
	}
}
