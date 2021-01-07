using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    GameObject option;
    [SerializeField]
    GameObject optionBtn1;
    [SerializeField]
    GameObject optionBtn2;

    [SerializeField]
    GameObject soulLinkBtn1;
    [SerializeField]
    GameObject soulLinkBtn2;
    [SerializeField]
    GameObject soulLink;

    [SerializeField]
    GameObject soulLinkUpGrade;

    [SerializeField]
    GameObject soulChangeBtn1;
    [SerializeField]
    GameObject soulChangeBtn2;
    [SerializeField]
    GameObject soulChange;

    


    public void OptionOn()
    {
        Debug.Log("함수는 실행함");
        SoulChangeOff();
        SoulLinkOff();

        option.SetActive(true);
        optionBtn1.SetActive(false);
        optionBtn2.SetActive(true);
    }
    public void OptionOff()
    {
        option.SetActive(false);
        optionBtn1.SetActive(true);
        optionBtn2.SetActive(false);
    }

    public void SoulLinkOn()
    {
        OptionOff();
        SoulChangeOff();

        soulLink.SetActive(true);
        soulLinkBtn1.SetActive(false);
        soulLinkBtn2.SetActive(true);

    }
    public void SoulLinkOff()
    {
        soulLink.SetActive(false);
        soulLinkBtn1.SetActive(true);
        soulLinkBtn2.SetActive(false);
        soulLinkUpGrade.SetActive(false);
    }

    public void SoulChangeOn()
    {
        //OptionOff();
        //SoulLinkOff();

        //soulChange.SetActive(true);
        //soulChangeBtn1.SetActive(false);
        //soulChangeBtn2.SetActive(true);
    }
    public void SoulChangeOff()
    {
        //soulChange.SetActive(false);
        //soulChangeBtn1.SetActive(true);
        //soulChangeBtn2.SetActive(false);
    }

    public void SoulLinkBack()
    {
        soulLinkUpGrade.SetActive(false);
        SoulLinkOn();
    }

}
