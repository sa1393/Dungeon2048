using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//, IBeginDragHandler, IEndDragHandler, IDragHandler
public class SkillReady : MonoBehaviour
{
    BoardManager boardManager;

    [SerializeField]
    GameObject skill_touch;

    public int skillNum;

    public static Vector2 defaultPosition;
    Sprite skilIcon;



    void Start()
    {
        boardManager = GameManager.instance.InGameScript.board;
    }
    public void Ready()
    {
        if (GameManager.instance.InAnimation)
            return;
        if (GameManager.instance.InUI)
            return;
        if (PlayerData.instance.player.GetComponent<Player>().CurrentSkill[skillNum] == PlayerData.instance.player.GetComponent<Player>().skillList.Count - 1) //잠금
            return;
        if (PlayerData.instance.player.GetComponent<Player>().skillList[PlayerData.instance.player.GetComponent<Player>().CurrentSkill[skillNum]].NowCoolTime != 0)
            return;
        if (PlayerData.instance.player.GetComponent<Player>().skillList[PlayerData.instance.player.GetComponent<Player>().CurrentSkill[skillNum]].Active == 2) //패시브
            return;
        if (PlayerData.instance.player.GetComponent<Player>().skillList[PlayerData.instance.player.GetComponent<Player>().CurrentSkill[skillNum]].Active == 1) //버프
        {
            Debug.Log("버프 시전");
            UseSkill(0, 0);
            return;
        }
        GameManager.instance.InSkill = true;
        boardManager = GameManager.instance.InGameScript.board;
        boardManager.CanSkill(PlayerData.instance.player.GetComponent<Player>().skillList[PlayerData.instance.player.GetComponent<Player>().CurrentSkill[skillNum]].Cross, PlayerData.instance.player.GetComponent<Player>().LocX, PlayerData.instance.player.GetComponent<Player>().LocY);
        skill_touch.SetActive(true);
        skill_touch.GetComponent<SkillCast>().skillNum = skillNum;
    }

    

    public void SkillCansel()
    {
        boardManager.BoardReset(); 
        InSkilOff();
    }

    public void UseSkill(int goalX, int goalY) //스킬 사용
    {
        StartCoroutine(PlayerData.instance.player.GetComponent<Player>().UseSkill(skillNum, goalX, goalY));
    }

    void InSkilOff()
    {
        GameManager.instance.InSkill = false;
    }
}
