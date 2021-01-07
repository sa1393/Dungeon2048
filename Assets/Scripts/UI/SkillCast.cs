using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCast : MonoBehaviour
{
    BoardManager boardManager;

    public GameObject[] cast = new GameObject[4];

    public int skillNum;

    void Start()
    {
        boardManager = GameManager.instance.InGameScript.board;
    }
    public void Cast()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if ((mousePos.y + 1.6f) < 0 || (mousePos.y + 1.6f) > 5.5f || (mousePos.x + 2.8f) < 0 || (mousePos.x + 2.8f) > 5.5f)
        {
            SkillCansel();
        }
        else
        {
            int goalX = (int)((mousePos.x + 2.8f) / 1.1f); //좌표 측정
            int goalY = (int)((mousePos.y + 1.6f) / 1.1f);
            UseSkill(goalX, goalY);
            SkillCansel();
        }
    }

    public void SkillCansel()
    {
        boardManager.BoardReset();
        StartCoroutine(InSkilOff());
    }

    public void UseSkill(int goalX, int goalY) //스킬 사용
    {
        StartCoroutine(PlayerData.instance.player.GetComponent<Player>().UseSkill(skillNum, goalX, goalY));
    }

    IEnumerator InSkilOff()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.InSkill = false;
        gameObject.SetActive(false);
    }
}
