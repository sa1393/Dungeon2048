using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulIconBtn : MonoBehaviour
{
    public int soulNum;
    public SoulLinkUpgradeUI upgradeUI;

    [SerializeField]
    GameObject soulLink;

    public void SoulLinkUpgrade()
    {
        upgradeUI.soulNum = soulNum;
        upgradeUI.Load();

        upgradeUI.gameObject.SetActive(true);
        soulLink.SetActive(false);
    }
}
