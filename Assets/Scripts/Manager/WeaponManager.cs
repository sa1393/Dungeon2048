using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    SpriteRenderer sr;
    public List<Weapon> weaponList = new List<Weapon>();

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        weaponList.Add(new Weapon("낡은 검", 0, 30, Weapon.WeaponType.Sword, 1, ""));
        weaponList.Add(new Weapon("튼튼한 검", 1, 50, Weapon.WeaponType.Sword, 1, ""));
        weaponList.Add(new Weapon("튼튼한 대검", 2, 70, Weapon.WeaponType.GreatSword, 1, ""));
        weaponList.Add(new Weapon("튼튼한 단검", 3, 30, Weapon.WeaponType.Dagger, 1, ""));
        weaponList.Add(new Weapon("튼튼한 도끼", 4, 30, Weapon.WeaponType.Axe, 1, ""));
        weaponList.Add(new Weapon("튼튼한 망치", 5, 40, Weapon.WeaponType.Hammer, 1, ""));
        weaponList.Add(new Weapon("튼튼한 창", 6, 30, Weapon.WeaponType.Spear, 1, ""));
        weaponList.Add(new Weapon("강철 칼날", 7, 50, Weapon.WeaponType.Sword, 2, "특수 능력 – 공격 시 25% 확률로 검기를 날려 한 칸 뒤에 있는 적까지 공격합니다."));
        weaponList.Add(new Weapon("미쳐버린 검", 8, 10, Weapon.WeaponType.Sword, 2, "특수 능력 – 매 공격 시 공격력이 1~15까지 무작위로 결정됩니다."));
        weaponList.Add(new Weapon("무식하게 큰 대검", 9, 90, Weapon.WeaponType.GreatSword, 2, "무식하게 큰 대검 (대검)"));
        weaponList.Add(new Weapon("흉악한 대검", 10, 70, Weapon.WeaponType.GreatSword, 2, "특수 능력 – 치명타 데미지가 3배가 됩니다."));
        weaponList.Add(new Weapon("야비한 단검", 11, 50, Weapon.WeaponType.Dagger, 2, "특수 능력 – 추가 공격은 무조건 치명타가 발동합니다."));
        weaponList.Add(new Weapon("예리한 단검", 12, 50, Weapon.WeaponType.Dagger, 2, "특수 능력 – 치명타 확률이 20% 추가로 증가합니다."));
        weaponList.Add(new Weapon("잡초 파괴자", 13, 20, Weapon.WeaponType.Axe, 2, "특수 능력 – 무기 재사용 대기 시간이 2로 감소합니다."));
        weaponList.Add(new Weapon("검은 무쇠 도끼", 14, 40, Weapon.WeaponType.Axe, 2, ""));
        weaponList.Add(new Weapon("굳건한 망치", 15, 40, Weapon.WeaponType.Hammer, 3, "특수 능력 – 기절 성공 시 2배의 피해를 입힙니다."));
        weaponList.Add(new Weapon("육중한 망치", 16, 60, Weapon.WeaponType.Hammer, 3, ""));
        weaponList.Add(new Weapon("재빠른 창", 17, 30, Weapon.WeaponType.Spear, 3, "특수 능력 – 20% 확률로 1번 더 공격합니다."));
        //weaponList.Add(new Weapon("강철 할버드", 18, 30, Weapon.WeaponType.Spear, 5, ""));
    }

    public void DeleteSprite()
    {
        sr.sprite = null;
    }

    public void ApplySprite(int currentWeapon)
    {
        if(currentWeapon == weaponList.Count)
        {
            sr.sprite = null;
        }
        else
        {
            sr.sprite = weaponList[currentWeapon].sprite;
        }
    }
}
