using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float XBasicLoc = -2.25f; // 0,0 위치
    protected float YBasicLoc = -1.15f;
    protected float XInterval = 1.125f; //간격
    protected float YInterval = 1.125f;

    float damage = 0; //공격력
    int cross = 0; //사거리
    int useMp = 0; //마나
    int coolTime = 0; //쿨타임
    int nowCoolTime = 0; //남은 쿨
    int id = 0;
    int rating = 0;
    int active = 0; //액티브스킬
    protected Animator skillAnime;

    string name = null;
    string info = null;

    protected GameObject player_skill;

    protected Sprite sprite;
    public Skill(int id, string name, string info, float damage, int cross, int useMp, int coolTime, int rating, int active)
    {
        this.Id = id;
        this.Name = name;
        if(active == 0)
        {
            this.Info = "유형 : 액티브\n스킬 공격력 : 플레이어 공격력 x " + damage + "\n" + info;
        }
        else if(active == 1)
        {
            this.Info = "유형 : 버프\n" + info;
        }
        else
        {
            this.Info = "유형 : 패시브\n" + info;

        }

        this.Damage = damage;
        this.Cross = cross;
        this.UseMp = useMp;
        this.CoolTime = coolTime;
        this.Active = active;
        this.Rating = rating;
        nowCoolTime = 0;

        player_skill = GameObject.Find("Player_Skill");
        sprite = GameManager.instance.skillIcons[id];
        skillAnime = player_skill.GetComponent<Animator>();
    }

    public int Cross { get => cross; set => cross = value; }
    public int UseMp { get => useMp; set => useMp = value; }
    public int CoolTime { get => coolTime; set => coolTime = value; }
    public string Name { get => name; set => name = value; }
    public string Info { get => info; set => info = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int NowCoolTime { get => nowCoolTime; set => nowCoolTime = value; }
    public int Rating { get => rating; set => rating = value; }
    public float Damage { get => damage; set => damage = value; }
    public int Id { get => id; set => id = value; }
    public int Active { get => active; set => active = value; }

    public virtual void Use(int goalX, int goalY)
    {

    }

    protected void Act(int goalX, int goalY)
    {
        skillAnime.SetTrigger("skill" + (Id + 1));
        int resultdamage = (int)(PlayerData.instance.player.GetComponent<Player>().playerStat.Damage * Damage);
        GameManager.instance.StartCoroutine(GameManager.instance.map[goalX, goalY].GetComponent<Enemy>().Hurt(resultdamage, true, 0, 0));
        player_skill.transform.position = new Vector2(XBasicLoc + XInterval * goalX, YBasicLoc + YInterval * goalY);
        NowCoolTime = coolTime;
    }

    protected void Buff(int goalX, int goalY)
    {
        Debug.Log("진짜 버프");
        goalX = PlayerData.instance.player.GetComponent<Player>().locX;
        goalY = PlayerData.instance.player.GetComponent<Player>().locY;
        skillAnime.SetTrigger("skill" + (Id + 1));
        player_skill.transform.position = new Vector2(XBasicLoc + XInterval * goalX, YBasicLoc + YInterval * goalY);
        NowCoolTime = coolTime;
    }

    protected void Passive(int goalX, int goalY)
    {
    }
}
