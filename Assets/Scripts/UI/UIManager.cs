using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Image hpBar;
    [SerializeField]
    Image mpBar;
    [SerializeField]
    Image expBar;

    [SerializeField]
    Image selectWeaponPanel;
    [SerializeField]
    Image selectSkillPanel;

    public Image weaponIcon;

    [SerializeField]
    Image skillImage1;
    [SerializeField]
    Image skillImage2;
    [SerializeField]
    Image skillImage3;
    [SerializeField]
    Image skillImage4;

    public Image weaponUI;

    public Text Level;
    public Text hpText;
    public Text mpText;
    public Text expText;
    public Text turnText;

    [SerializeField]
    Text[] skillCool;

    float lerpSpeed = 5;

    float hpCurrentFill = 0;
    float mpCurrentFill = 0;
    float expCurrentFill = 0;

    int[] weaponSelect = new int[3];
    int[] skillSelect = new int[3];

    Player player;

    public Player Player { get => player; set => player = value; }

    void Start()
    {
        player = PlayerData.instance.player.GetComponent<Player>();
    }

    public void SelectSkillStart()
    {
        GameManager.instance.InUI = true;
        selectSkillPanel.gameObject.SetActive(true);

        int check = -1;
        for(int i = 0; i < 3; i++)
        {
            while (true)
            {
                check = -1;
                skillSelect[i] = Random.Range(0, Player.skillList.Count);
                for (int j = 0; j < Player.CurrentSkill.Length; j++)
                {
                    if (Player.CurrentSkill[j] == skillSelect[i])
                    {
                        check = 0;
                    }
                }
                for(int k = 0; k < 3; k++)
                {
                    if (k == i) continue;
                    if (skillSelect[k] == skillSelect[i])
                    {
                        check = 0;
                    }
                }
                if (check == -1)
                {
                    Debug.Log(i + " : " + skillSelect[i]);
                    break;
                }

            }
        }

        if (GameManager.instance.doTutorial == 1)
        {
            skillSelect[0] = 0;
            skillSelect[1] = 1;
            skillSelect[2] = 2;
        }
        
        selectSkillPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.skillList[skillSelect[0]].Info;
        selectSkillPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = Player.skillList[skillSelect[0]].Name;
        selectSkillPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.instance.skillSelect[Player.skillList[skillSelect[0]].Rating - 1];
        selectSkillPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Player.skillList[skillSelect[0]].Sprite;

        selectSkillPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.skillList[skillSelect[1]].Info;
        selectSkillPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = Player.skillList[skillSelect[1]].Name;
        selectSkillPanel.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.instance.skillSelect[Player.skillList[skillSelect[1]].Rating - 1];
        selectSkillPanel.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = Player.skillList[skillSelect[1]].Sprite;

        selectSkillPanel.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.skillList[skillSelect[2]].Info;
        selectSkillPanel.transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = Player.skillList[skillSelect[2]].Name;
        selectSkillPanel.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.instance.skillSelect[Player.skillList[skillSelect[2]].Rating - 1];
        selectSkillPanel.transform.GetChild(2).transform.GetChild(1).GetComponent<Image>().sprite = Player.skillList[skillSelect[2]].Sprite;

    }
    public void SelectSkillClose(GameObject gameObject)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Player.CurrentSkill[i] == Player.skillList.Count - 1)
            {
                Player.CurrentSkill[i] = skillSelect[gameObject.transform.GetSiblingIndex()];
                Player.SetSkill();
                break;
            }
        }
        selectSkillPanel.gameObject.SetActive(false);
        StartCoroutine(UIOff());
        if(GameManager.instance.doTutorial == 0)
            SelectWeaponStart();
    }
    public void SelectWeaponStart()
    {
        GameManager.instance.InUI = true;
        selectWeaponPanel.gameObject.SetActive(true);
        weaponSelect[0] = Random.Range(0, Player.weaponList.Count);
        while (true)
        {
            weaponSelect[1] = Random.Range(0, Player.weaponList.Count);
            if (weaponSelect[1] != weaponSelect[0]) break;
        }
        while (true)
        {
            weaponSelect[2] = Random.Range(0, Player.weaponList.Count);
            if (weaponSelect[2] != weaponSelect[0] && weaponSelect[2] != weaponSelect[1]) break;
        }

        selectWeaponPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.weaponList[weaponSelect[0]].Info;
        selectWeaponPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = Player.weaponList[weaponSelect[0]].name;
        selectWeaponPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.instance.weaponSelect[Player.weaponList[weaponSelect[0]].Rating - 1];
        selectWeaponPanel.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Player.weaponList[weaponSelect[0]].sprite;

        selectWeaponPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.weaponList[weaponSelect[1]].Info;
        selectWeaponPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = Player.weaponList[weaponSelect[1]].name;
        selectWeaponPanel.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.instance.weaponSelect[Player.weaponList[weaponSelect[1]].Rating - 1];
        selectWeaponPanel.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Player.weaponList[weaponSelect[1]].sprite;

        selectWeaponPanel.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.weaponList[weaponSelect[2]].Info;
        selectWeaponPanel.transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = Player.weaponList[weaponSelect[2]].name;
        selectWeaponPanel.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.instance.weaponSelect[Player.weaponList[weaponSelect[2]].Rating - 1];
        selectWeaponPanel.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Player.weaponList[weaponSelect[2]].sprite;



    }

    public void SelectWeaponClose(GameObject gameObject)
    {
        Player.CurrentWeaponId = weaponSelect[gameObject.transform.GetSiblingIndex()];
        Player.setWeapon();
        selectWeaponPanel.gameObject.SetActive(false);
        weaponIcon.sprite = Player.weaponList[Player.CurrentWeaponId].sprite;
        StartCoroutine(UIOff());
    }

    public void setBar()
    {
        hpCurrentFill = (float)player.playerStat.Hp / (float)player.playerStat.MaxHp;
        mpCurrentFill = (float)player.playerStat.Mp / (float)player.playerStat.MaxMp;
        expCurrentFill = (float)player.playerStat.Exp / (float)player.playerStat.MaxExp;

        hpText.text = Player.playerStat.Hp + " / " + Player.playerStat.MaxHp;
        mpText.text = Player.playerStat.Mp + " / " + Player.playerStat.MaxMp;
        expText.text = Player.playerStat.Exp + " / " + Player.playerStat.MaxExp;

        if (Player.CurrentSkill[0] != Player.skillList.Count-1)
        {
            string currentCool;
            skillImage1.fillAmount = (Player.skillList[Player.CurrentSkill[0]].CoolTime - (float)Player.skillList[Player.CurrentSkill[0]].NowCoolTime) /
            (float)Player.skillList[Player.CurrentSkill[0]].CoolTime;

            if (Player.skillList[Player.CurrentSkill[0]].NowCoolTime == 0)
            {
                currentCool = null;
            }
            else
            {
                currentCool = Player.skillList[Player.CurrentSkill[0]].NowCoolTime.ToString() + "T";
            }
            skillImage1.transform.GetChild(0).GetComponent<Text>().text = currentCool;


        }
        if (Player.CurrentSkill[1] != Player.skillList.Count - 1)
        {
            string currentCool;
            skillImage2.fillAmount = (Player.skillList[Player.CurrentSkill[1]].CoolTime - (float)Player.skillList[Player.CurrentSkill[1]].NowCoolTime) /
            (float)Player.skillList[Player.CurrentSkill[1]].CoolTime;

            if (Player.skillList[Player.CurrentSkill[1]].NowCoolTime == 0)
            {
                currentCool = null;
            }
            else
            {
                currentCool = Player.skillList[Player.CurrentSkill[1]].NowCoolTime.ToString() + "T";
            }
            skillImage2.transform.GetChild(0).GetComponent<Text>().text = currentCool;
        }
        if (Player.CurrentSkill[2] != Player.skillList.Count - 1)
        {
            string currentCool;
            skillImage3.fillAmount = (Player.skillList[Player.CurrentSkill[2]].CoolTime - (float)Player.skillList[Player.CurrentSkill[2]].NowCoolTime) /
            (float)Player.skillList[Player.CurrentSkill[2]].CoolTime;

            if (Player.skillList[Player.CurrentSkill[2]].NowCoolTime == 0)
            {
                currentCool = null;
            }
            else
            {
                currentCool = Player.skillList[Player.CurrentSkill[2]].NowCoolTime.ToString() + "T";
            }
            skillImage3.transform.GetChild(0).GetComponent<Text>().text = currentCool;
        }

        if (Player.CurrentSkill[3] != Player.skillList.Count - 1)
        {
            string currentCool;
            skillImage4.fillAmount = (Player.skillList[Player.CurrentSkill[3]].CoolTime - (float)Player.skillList[Player.CurrentSkill[3]].NowCoolTime) /
            (float)Player.skillList[Player.CurrentSkill[3]].CoolTime;

            if (Player.skillList[Player.CurrentSkill[3]].NowCoolTime == 0)
            {
                currentCool = null;
            }
            else
            {
                currentCool = Player.skillList[Player.CurrentSkill[3]].NowCoolTime.ToString() + "T";
            }
            skillImage4.transform.GetChild(0).GetComponent<Text>().text = currentCool;
        }
    }

    public void SetSkillSprite()
    {
        skillImage1.sprite = Player.skillList[Player.CurrentSkill[0]].Sprite;
        skillImage1.transform.parent.gameObject.GetComponent<Image>().sprite = Player.skillList[Player.CurrentSkill[0]].Sprite;

        skillImage2.sprite = Player.skillList[Player.CurrentSkill[1]].Sprite;
        skillImage2.transform.parent.gameObject.GetComponent<Image>().sprite = Player.skillList[Player.CurrentSkill[1]].Sprite;

        skillImage3.sprite = Player.skillList[Player.CurrentSkill[2]].Sprite;
        skillImage3.transform.parent.gameObject.GetComponent<Image>().sprite = Player.skillList[Player.CurrentSkill[2]].Sprite;

        skillImage4.sprite = Player.skillList[Player.CurrentSkill[3]].Sprite;
        skillImage4.transform.parent.gameObject.GetComponent<Image>().sprite = Player.skillList[Player.CurrentSkill[3]].Sprite;
    }

    public void SetWeaponUI()
    {
        weaponUI.fillAmount = 1 - ((float)(Player.playerStat.MoveMent + 1) / (Player.playerStat.MaxMoveMent + 1));
        Debug.Log("무기 쿨 UI " + Player.playerStat.MaxMoveMent + ", " + Player.playerStat.MoveMent);
    }

    void Update()
    {
        if(hpBar.fillAmount != hpCurrentFill)
        {
            hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hpCurrentFill, Time.deltaTime * lerpSpeed);
        }
        if (mpBar.fillAmount != mpCurrentFill)
        {
            mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, mpCurrentFill, Time.deltaTime * lerpSpeed);
        }
        if (expBar.fillAmount != expCurrentFill)
        {
            expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, expCurrentFill, Time.deltaTime * lerpSpeed);
        }
    }

    IEnumerator UIOff()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.InUI = false;
    }
}
