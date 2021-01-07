using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public InGame InGameScript;

    public GameObject[,] map;

    public int turn = 1;
    public int maxTurn;
    public int money;

    public bool InGame = false; // 게임중
    public bool InAnimation = false; //애니메이션 실행중
    public bool InUI = false; //유아이창
    public bool InSkill = false;
    public bool InMove = false; //이동중
    public bool InOption = false;

    public int doTutorial = 0; // 0 : 함 1: 안함

    public Sprite[] tiers;
    public Sprite[] skillIcons;
    public Sprite[] weaponIcons;
    public Sprite[] weaponSelect;
    public Sprite[] skillSelect;
    public Sprite[] buffIcons;
    public Sprite[] soulLinkIcons;
    public Sprite[] soulLinkAbilIcons;


    public Sprite[] aim;
    public Sprite skillRock;
   

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

        tiers = Resources.LoadAll<Sprite>("tier");
        skillIcons = Resources.LoadAll<Sprite>("skill/icon");
        weaponIcons = Resources.LoadAll<Sprite>("weapon");
        aim = Resources.LoadAll<Sprite>("ui/aim");
        skillRock = Resources.Load<Sprite>("ui/skill/skill_rock");
        weaponSelect = Resources.LoadAll<Sprite>("ui/environment/select_weapon");
        skillSelect = Resources.LoadAll<Sprite>("ui/environment/select_skill");
        buffIcons = Resources.LoadAll<Sprite>("ui/buff");
        soulLinkIcons = Resources.LoadAll<Sprite>("ui/SoulLink/Icon");
        soulLinkAbilIcons = Resources.LoadAll<Sprite>("ui/SoulLink/abilIcon");

        doTutorial = 1;

        maxTurn = PlayerPrefs.GetInt("maxTurn", 0);
        money = PlayerPrefs.GetInt("money", 0);
        
        doTutorial = PlayerPrefs.GetInt("tutorial", 1);
    }

    public void GameInitialize() 
    {
        InGame = true; 
        InAnimation = false; 
        InUI = false;
        InSkill = false;
    }

    public IEnumerator GameOver()
    {
        GameManager.instance.InGame = false;
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("SceneManager").GetComponent<SceneMove>().GameOver();
        if (turn > maxTurn) maxTurn = turn;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("maxTurn", maxTurn);
        PlayerPrefs.SetInt("money", money);

        PlayerPrefs.SetInt("attackDamageLevel", SoulLinkManager.instance.AttackDamageLevel);
        PlayerPrefs.SetInt("criticalLevel", SoulLinkManager.instance.CriticalLevel);
        PlayerPrefs.SetInt("criticalDamageLevel", SoulLinkManager.instance.CriticalDamageLevel);
        PlayerPrefs.SetInt("defense", SoulLinkManager.instance.DefenseLevel);
        PlayerPrefs.SetInt("maxHp", SoulLinkManager.instance.MaxHpLevel);
        PlayerPrefs.SetInt("maxHp", SoulLinkManager.instance.MaxMpLevel);
        PlayerPrefs.SetInt("plusExp", SoulLinkManager.instance.PlusExpLevel);

        PlayerPrefs.SetInt("tutorial", doTutorial);
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("maxTurn", maxTurn);
        PlayerPrefs.SetInt("money", money);

        PlayerPrefs.SetInt("attackDamageLevel", SoulLinkManager.instance.AttackDamageLevel);
        PlayerPrefs.SetInt("criticalLevel", SoulLinkManager.instance.CriticalLevel);
        PlayerPrefs.SetInt("criticalDamageLevel", SoulLinkManager.instance.CriticalDamageLevel);
        
        PlayerPrefs.SetInt("defense", SoulLinkManager.instance.DefenseLevel);
        PlayerPrefs.SetInt("maxHp", SoulLinkManager.instance.MaxHpLevel);
        PlayerPrefs.SetInt("maxHp", SoulLinkManager.instance.MaxMpLevel);
        PlayerPrefs.SetInt("plusExp", SoulLinkManager.instance.PlusExpLevel);
        
        PlayerPrefs.SetInt("tutorial", doTutorial);
    }
}


