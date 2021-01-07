using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
	public int locX; //x 좌표
	public int locY; //y 좌표

	float xBasicLoc = -2.25f; // 0,0 위치
    float yBasicLoc = -1.15f;
    float xInterval = 1.125f; //간격
    float yInterval = 1.125f;

	private float inverseMoveTime = 5f;
	
	private Rigidbody2D rb2D;

	private bool isMoving;

	public int LocX { get => locX; set => locX = value; }
	public int LocY { get => locY; set => locY = value; }
    public float XInterval { get => xInterval; set => xInterval = value; }
    public float YInterval { get => yInterval; set => yInterval = value; }
	public float XBasicLoc { get => xBasicLoc; set => xBasicLoc = value; }
	public float YBasicLoc { get => yBasicLoc; set => yBasicLoc = value; }

	void Start()
    {
		inverseMoveTime = 3f;
		rb2D = GetComponent<Rigidbody2D>();
	}

	protected bool CheckWall(int desX, int desY) // 벽 체크
	{
		if (desX > 4 || desX < 0 || desY > 4 || desY < 0) return true;
		else return false;
	}
	protected bool CheckMapNull(int desX, int desY) //아무거나 있는지 체크
	{
		if (GameManager.instance.InGameScript.map[desX, desY] == null) return true;
		else return false;
	}
	protected bool CheckEnemy(int desX, int desY) // 적 체크
	{
		if (GameManager.instance.InGameScript.map[desX, desY] != null && GameManager.instance.InGameScript.map[desX, desY].GetComponent<Enemy>() != null) return true;
		else return false;
	}
	protected bool CheckSpirit(int desX, int desY) // 영혼 체크
	{
		if (GameManager.instance.InGameScript.map[desX, desY] != null && GameManager.instance.InGameScript.map[desX, desY].GetComponent<PlayerSpirit>() != null) return true;
		else return false;
	}
	protected bool CheckPlayer(int desX, int desY) // 플레이어 체크
	{
		if (GameManager.instance.InGameScript.map[desX, desY] != null && GameManager.instance.InGameScript.map[desX, desY].GetComponent<Player>() != null) return true;
		else return false;
	}
	protected void SetAnimationDelay(float animationDelay)
	{
		if(GameManager.instance.InGameScript.animationDelay < animationDelay) //기존 딜레이보다 클시 바꿈
			GameManager.instance.InGameScript.animationDelay = animationDelay;
	}
	protected void SetCounterAnimeDelay(float counterAnimeDelay)
	{
		if (GameManager.instance.InGameScript.counterAnimeDelay < counterAnimeDelay) //기존 딜레이보다 클시 바꿈
			GameManager.instance.InGameScript.counterAnimeDelay = counterAnimeDelay;
	}
	protected void SetSpiritAnimationDelay(float spiritAnimationDelay)
	{
		if (GameManager.instance.InGameScript.spiritAnimationDelay < spiritAnimationDelay) //기존 딜레이보다 클시 바꿈
			GameManager.instance.InGameScript.spiritAnimationDelay = spiritAnimationDelay;
	}
	protected bool CheckTier(int desX, int desY) // 티어 체크
	{
		if(GameManager.instance.InGameScript.map[LocX, LocY] != null && GameManager.instance.InGameScript.map[desX, desY] != null)
		{
			if (CheckEnemy(locX, locY))
			{
				if (GameManager.instance.map[LocX, LocY].GetComponent<Enemy>().enemyStat.Tier == GameManager.instance.map[desX, desY].GetComponent<Enemy>().enemyStat.Tier) return true;
			}
			else if (CheckPlayer(locX, locY))
			{
				if (GameManager.instance.map[LocX, LocY].GetComponent<PlayerBase>().playerStat.Tier == GameManager.instance.map[desX, desY].GetComponent<PlayerBase>().playerStat.Tier) return true;

			}
			else if (CheckSpirit(locX, locY))
			{
				if (GameManager.instance.map[LocX, LocY].GetComponent<PlayerBase>().playerStat.Tier == GameManager.instance.map[desX, desY].GetComponent<PlayerBase>().playerStat.Tier) return true;

			}
		}

		return false;
	}
	protected bool CheckEqul(int desX, int desY) // 같은지 체크
	{
		if (!CheckMapNull(desX, desY) && CheckEnemy(desX, desY))
		{
			if (string.Equals(GameManager.instance.map[LocX, LocY].GetComponent<Enemy>().Name, GameManager.instance.map[desX, desY].GetComponent<Enemy>().Name)
				&& GameManager.instance.map[LocX, LocY].GetComponent<Enemy>().enemyStat.Evolution == GameManager.instance.map[desX, desY].GetComponent<Enemy>().enemyStat.Evolution
				&& GameManager.instance.map[LocX, LocY].GetComponent<Enemy>().enemyStat.Tier == GameManager.instance.map[desX, desY].GetComponent<Enemy>().enemyStat.Tier)
				return true;
			else return false;
		}
		else return false;
	}

	protected void Move(int locX, int locY, int desX, int desY) // 현재 위치에서 목표위치로 이동 (자기좌표, 목적지좌표)
	{
		GameObject temp = GameManager.instance.map[locX, locY];
		GameManager.instance.map[locX, locY] = null;
		GameManager.instance.map[desX, desY] = temp;

		float goalX = XBasicLoc + (desX * XInterval);
		float goalY = YBasicLoc + (desY * YInterval);

		StartCoroutine(SmoothMovement(new Vector2(goalX, goalY)));
	}
	int a;
	protected IEnumerator SmoothMovement(Vector2 end) //부드러운 움직임
	{
		Vector2 offSet = new Vector2(transform.position.x, transform.position.y);
		float dis = Vector2.Distance(offSet, end);
		inverseMoveTime = 15f;

		float time = 0;

		while (true)
		{
			transform.position = Vector2.Lerp(transform.position, end, inverseMoveTime * Time.deltaTime);
			offSet = new Vector2(transform.position.x, transform.position.y);
			
			if (Vector2.Distance(offSet, end) < 0.05f)
			{
				
				transform.position = end;
				break;
			}
			time += 0.001f;
			yield return new WaitForSeconds(0.001f);
		}

	}
}
