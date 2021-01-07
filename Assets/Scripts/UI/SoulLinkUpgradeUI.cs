using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SoulLinkUpgradeUI : MonoBehaviour
{
    public int soulNum;
    int cost;
    int level;
    float value;
    float nextValue;

    [SerializeField]
    Image soulIcon = null;
    [SerializeField]
    Text costText = null;
    [SerializeField]
    Text levelText = null;
    [SerializeField]
    Text info;
    [SerializeField]
    Text title;

    public void Load()
    {
        soulIcon.sprite = GameManager.instance.soulLinkIcons[soulNum];
        switch (soulNum)
        {
            case 0:
                cost = SoulLinkManager.instance.AttackDamageCost;
                level = SoulLinkManager.instance.AttackDamageLevel;
                value = SoulLinkManager.instance.AttackDamage;
                title.text = "소울링크-공격력";
                if (level < 5)
                    info.text = "소울링크-공격력 " + (value * 100) + "% 증가 => " + ((SoulLinkManager.instance.AttackDamageLevel + 1) * 5) + "%";
                else
                    info.text = "소울링크-공격력 " + (value * 100) + "% 증가";

                break;
            case 1:
                cost = SoulLinkManager.instance.CriticalCost;
                level = SoulLinkManager.instance.CriticalLevel;
                value = SoulLinkManager.instance.Critical;
                title.text = "소울링크-크리티컬 확률";
                if (level < 5)
                    info.text = "크리티컬 확률 " + (value * 100) + "% 증가 => " + ((SoulLinkManager.instance.CriticalLevel + 1) * 2) + "%";
                else
                    info.text = "크리티컬 확률 " + (value * 100) + "% 증가";
                break;
            case 2:
                cost = SoulLinkManager.instance.CriticalDamageCost;
                level = SoulLinkManager.instance.CriticalDamageLevel;
                value = SoulLinkManager.instance.CriticalDamage;
                title.text = "소울링크-크리티컬 공격력";
                if (level < 5)
                    info.text = "크리티컬 공격력 " + (value * 100) + "% 증가 => " + ((SoulLinkManager.instance.CriticalDamageLevel + 1) * 10) + "%";
                else
                    info.text = "크리티컬 공격력 " + (value * 100) + "% 증가";
                break;
            case 3:
                cost = SoulLinkManager.instance.DefenseCost;
                level = SoulLinkManager.instance.DefenseLevel;
                value = SoulLinkManager.instance.Defense;
                title.text = "소울링크-방어력";
                if (level < 5)
                    info.text = "방어력 " + (value * 100) + "% 증가 => " + ((SoulLinkManager.instance.DefenseLevel + 1) * 2) + "%";
                else
                    info.text = "방어력 " + (value * 100) + "% 증가";
                break;
            case 4:
                cost = SoulLinkManager.instance.MaxHpCost;
                level = SoulLinkManager.instance.MaxHpLevel;
                value = SoulLinkManager.instance.MaxHp;
                title.text = "소울링크-최대 체력";
                if (level < 5)
                    info.text = "최대 체력 " + (value * 100) + "% 증가 => " + ((SoulLinkManager.instance.MaxHpLevel + 1) * 10) + "%";
                else
                    info.text = "최대 체력 " + (value * 100) + "% 증가";
                break;
            case 5:
                cost = SoulLinkManager.instance.MaxMpCost;
                level = SoulLinkManager.instance.MaxMpLevel;
                value = SoulLinkManager.instance.MaxMp;
                title.text = "소울링크-최대 마나";
                if (level < 5)
                    info.text = "최대 마나 " + value + "증가 => " + ((SoulLinkManager.instance.MaxMpLevel + 1) * 10);
                else
                    info.text = "최대 마나 " + value + "증가";
                break;
            case 6:
                cost = SoulLinkManager.instance.PlusExpCost;
                level = SoulLinkManager.instance.PlusExpLevel;
                value = SoulLinkManager.instance.PlusExp;
                title.text = "소울링크-추가 경험치";
                if (level < 5)
                    info.text = "경험치 " + (value*100) + "% 증가 => " + ((SoulLinkManager.instance.PlusExpLevel + 1) * 5) + "%";
                else
                    info.text = "경험치 " + (value * 100) + "% 증가";
                break;
        }

        levelText.text = level.ToString() + "단계";
        if (level < 5)
            costText.text = cost.ToString();
        else
            costText.text = "MAX";
    }

    public void Upgrade()
    {
        if(GameManager.instance.money >= cost && level < 5)
        {
            GameManager.instance.money -= cost;
            GameManager.instance.InGameScript.moneyText.text = GameManager.instance.money.ToString();
            switch (soulNum)
            {
                case 0:
                    SoulLinkManager.instance.AttackDamageLevel++;
                    break;
                case 1:
                    SoulLinkManager.instance.CriticalLevel++;
                    break;
                case 2:
                    SoulLinkManager.instance.CriticalDamageLevel++;
                    break;
                case 3:
                    SoulLinkManager.instance.DefenseLevel++;
                    break;
                case 4:
                    SoulLinkManager.instance.MaxHpLevel++;
                    break;
                case 5:
                    SoulLinkManager.instance.MaxMpLevel++;
                    break;
                case 6:
                    SoulLinkManager.instance.PlusExpLevel++;
                    break;
            }

            SoulLinkManager.instance.SetSoul();
            PlayerData.instance.player.GetComponent<Player>().UpdateStat();
            Load();
        }
    }
}
