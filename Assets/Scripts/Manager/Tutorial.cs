using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    int tutorialTurn = 1;
    List<String> info = new List<String>();
    [SerializeField]
    InGameUI inGameUI;

    [SerializeField]
    Text infoText;

    [SerializeField]
    Image infoBox;

    [SerializeField]
    Image[] panel = new Image[15];

    private void Awake()
    {
        info.Add("슬라이드를 하면 플레이어가 움직입니다.");
        info.Add("적도 플레이어를 따라 같은 방향으로 움직입니다.");
        info.Add("무기 사거리안에 적이 있다면 플레이어는 공격합니다.");
        info.Add("적을 한 번에 처치하지 못한다면 플레이어는 적에게 반격당합니다.");
        info.Add("바로 앞 적을 처치한다면 플레이어는 그 자리로 이동합니다.");
        info.Add("이 숫자는 티어입니다. 티어가 같은 적끼리는 합성이 가능하고 동일한 적이라면 진화합니다.");
        info.Add("2, 5, 12, 20레벨마다 스킬 칸이 해방되어 스킬을 사용 할 수 있습니다.");
        info.Add("레벨이 오를 때 마다 무기를 선택합니다.");
        info.Add("레벨이 오른다면 플레이어의 능력치를 계승받은 혼이 생성됩니다.");
        info.Add("혼은 티어가 같다면 플레이어와 합성할 수 있고 같은 혼과도 합성됩니다.");
        info.Add("무기에 따라 재사용시간과 공격범위가 달라집니다.");
        info.Add("터치로 스킬을 사용 할 수 있습니다.");
        info.Add("적을 죽여 얻은 화폐로 소울링크를 강화 할 수 있습니다.");
        info.Add("체력이 0이 되거나 모든칸이 가득차면 게임 오버됩니다.");
        info.Add("끊임없이 생성되는 적을 처치하고 레벨을 올려 최대한 오래 살아남으세요! 그럼 건투를 빕니다.");
    }

    void Start()
    {
        if(GameManager.instance.doTutorial == 1)
        {
            Debug.Log("튜토리얼 시작");
            infoBox.gameObject.SetActive(true);
            Scenario01();
        }
        else
        {
            for (int i = 0; i < 15; i++)
            {
                panel[i].gameObject.SetActive(false);
            }
        }
    }

    public void StartTutorial()
    {
        
    }

    public void ScenarioA()
    {

    }

    public void ScenarioRemove(int num)
    {
        for(int i = 0; i < 15; i++)
        {
            if (num == i)
                panel[i].gameObject.SetActive(true);
            else
                panel[i].gameObject.SetActive(false);
        }

        infoText.text = info[num];
    }

    public void Scenario01()
    {
        ScenarioRemove(0);
    }
    public void Scenario02()
    {
        ScenarioRemove(1);
        StartCoroutine(GameManager.instance.InGameScript.Move(2));
       
    }
    public void Scenario03()
    {
        ScenarioRemove(2);
    }
    public void Scenario04()
    {
        ScenarioRemove(3);
        StartCoroutine(GameManager.instance.InGameScript.Move(4));
    }
    public void Scenario05()
    {
        ScenarioRemove(4);
    }
    public void Scenario06()
    {
        ScenarioRemove(5);
        StartCoroutine(GameManager.instance.InGameScript.Move(4));
    }
    public void Scenario07()
    {
        ScenarioRemove(6);
        StartCoroutine(GameManager.instance.InGameScript.Move(1));
    }
    public void Scenario08()
    {
        ScenarioRemove(7);
        PlayerData.instance.player.GetComponent<Player>().uiManager.SelectSkillStart();
        PlayerData.instance.player.GetComponent<Player>().getExp(100);
    }
    public void Scenario09()
    {
        ScenarioRemove(8);
        PlayerData.instance.player.GetComponent<Player>().uiManager.SelectWeaponStart();
        GameManager.instance.InGameScript.CreatePlayerSpirit();
    }
    public void Scenario10()
    {
        ScenarioRemove(9);
    }
    public void Scenario11()
    {
        ScenarioRemove(10);
        StartCoroutine(GameManager.instance.InGameScript.Move(2));
    }
    public void Scenario12()
    {
        ScenarioRemove(11);
    }
    public void Scenario13()
    {
        ScenarioRemove(12);
        inGameUI.SoulLinkOn();
    }
    public void Scenario14()
    {
        ScenarioRemove(13);
        inGameUI.SoulLinkOff();
    }
    public void Scenario15()
    {
        ScenarioRemove(14);
    }
    public void ScenerioQuit()
    {
        GameManager.instance.doTutorial = 0;
        GameManager.instance.turn = 1;
        SceneManager.LoadScene("Game");
    }

}
