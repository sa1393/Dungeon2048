using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public List<PlayerSpirit> playerSpiritList = new List<PlayerSpirit>(); //생성된 플레이어 혼 리스트
    public List<Enemy> enemyList = new List<Enemy>(); //생성된 적 리스트

    public GameObject[,] map; //좌표
    
    public GameObject playerPrefebs;
    public GameObject playerSpiritPrefeb;
    public List<GameObject> EnemyPrefebs = new List<GameObject>();

    public BoardManager board;
    public GameObject player;
    public Text turnText;
    public Text moneyText;

    int ranX;
    int ranY;

    public int horizontal = 0;
    public int vertical = 0;

    float XBasicLoc = -2.25f; // 0,0 위치
    float YBasicLoc = -1.15f;
    float XInterval = 1.125f; //간격
    float YInterval = 1.125f;

    public float animationDelay = 0; //플레이어, 혼, 적마다 애니메이션 길이
    public float spiritAnimationDelay = 0; //혼마다 딜레이
    public float counterAnimeDelay = 0; //적의 반격 딜레이

    public int gameMoney;

    private void Awake()
    {
        GameManager.instance.InGameScript = this;
        GameManager.instance.InGame = true;

        map = new GameObject[5, 5];

        GameManager.instance.map = map;

        player = Instantiate(playerPrefebs, new Vector2(XBasicLoc + (XInterval * 2), YBasicLoc + (YInterval * 2)), Quaternion.identity);

        map[2, 2] = player;

        player.GetComponent<Player>().LocX = 2;
        player.GetComponent<Player>().LocY = 2;

    }

    void Start()
    {
        board = gameObject.GetComponent<BoardManager>();
        CreateEnemy();
        turnText.text = GameManager.instance.turn.ToString();
        moneyText.text = GameManager.instance.money.ToString();

        GameManager.instance.InGame = true;
        GameManager.instance.InSkill = false;
        GameManager.instance.InUI = false;
        GameManager.instance.InMove = false;
        GameManager.instance.InAnimation = false;

    }

    private void Update()
    {
        if (GameManager.instance.InUI) return;
        if (GameManager.instance.InSkill) return;
        if (!GameManager.instance.InGame) return;
        if (GameManager.instance.InMove) return;
        if (GameManager.instance.InAnimation) return;

#if UNITY_ANDROID
        
#endif
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");


        if (vertical != 0 || horizontal != 0)
        {
            int dir = (horizontal == 1) ? 2 : ((horizontal == -1) ? 1 : (vertical == 1) ? 3 : 4);
            StartCoroutine(Move(dir));
            horizontal = 0;
            vertical = 0;
        }
    }

    public void CreatePlayerSpirit()
    {
        while (true)
        {
            int check = 0;
            ranX = UnityEngine.Random.Range(0, board.BoardWidth);
            ranY = UnityEngine.Random.Range(0, board.BoardHeight);

            foreach (PlayerSpirit players in playerSpiritList)
            {
                if (players.LocX == ranX && players.LocY == ranY) check = 1;
            }

            foreach (Enemy enemys in enemyList)
            {
                if (enemys.LocX == ranX && enemys.LocY == ranY) check = 1;
            }
            if (PlayerData.instance.player.GetComponent<Player>().LocX == ranX && PlayerData.instance.player.GetComponent<Player>().LocY == ranY)
            {
                check = 1;
            }

            if (check == 0) break;
        }

        if (GameManager.instance.doTutorial == 1 && GameManager.instance.turn < 6)
        {
            ranX = 3;
            ranY = 1;
        }

        GameObject playerSpirit = Instantiate(playerSpiritPrefeb, new Vector2(XBasicLoc + (XInterval * ranX), YBasicLoc + (YInterval * ranY)), Quaternion.identity);
        map[ranX, ranY] = playerSpirit;
        playerSpirit.GetComponent<PlayerSpirit>().LocX = ranX;
        playerSpirit.GetComponent<PlayerSpirit>().LocY = ranY;

        playerSpiritList.Add(playerSpirit.GetComponent<PlayerSpirit>());
    }

    private void CreateEnemy()
    {
        while (true)
        {
            int check = 0;
            ranX = UnityEngine.Random.Range(0, board.BoardWidth);
            ranY = UnityEngine.Random.Range(0, board.BoardHeight);
            
            foreach(PlayerSpirit players in playerSpiritList)
            {
                if(players.LocX == ranX && players.LocY == ranY) check = 1;
            }

            foreach (Enemy enemys in enemyList)
            {
                if (enemys.LocX == ranX && enemys.LocY == ranY) check = 1;
            }
            if(PlayerData.instance.player.GetComponent<Player>().LocX == ranX && PlayerData.instance.player.GetComponent<Player>().LocY == ranY)
            {
                check = 1;
            }

            if(check == 0) break;

        }
        if(GameManager.instance.doTutorial == 1 && GameManager.instance.turn == 1)
        {
            ranX = 2;
            ranY = 1;
        }

        if (GameManager.instance.doTutorial == 1 && GameManager.instance.turn == 2)
        {
            ranX = 0;
            ranY = 4;
        }
        if (GameManager.instance.doTutorial == 1 && GameManager.instance.turn == 3)
        {
            ranX = 4;
            ranY = 4;
        }
        if (GameManager.instance.doTutorial == 1 && GameManager.instance.turn == 4)
        {
            ranX = 1;
            ranY = 2;
        }
        if (GameManager.instance.doTutorial == 1 && GameManager.instance.turn == 5)
        {
            ranX = 0;
            ranY = 4;
        }
        if (GameManager.instance.doTutorial == 1 && GameManager.instance.turn == 6)
        {
            ranX = 4;
            ranY = 0;
        }

        GameObject enemy = Instantiate(EnemyPrefebs[0], new Vector2(XBasicLoc + (XInterval * ranX), YBasicLoc + (YInterval * ranY)), Quaternion.identity);
        map[ranX, ranY] = enemy;
        enemy.GetComponent<Enemy>().LocX = ranX;
        enemy.GetComponent<Enemy>().LocY = ranY;
        int enemyTier = (GameManager.instance.turn / 20) + 1;
        enemy.GetComponent<Enemy>().SetStat(1, enemyTier, "Idle");


        enemyList.Add(enemy.GetComponent<Enemy>());

        if (GameOverCheck()) StartCoroutine(GameManager.instance.GameOver());
    }

    public IEnumerator Move(int dir) // 1 왼쪽 2 오른쪽 3 위 4 아래
    {
        if (!GameManager.instance.InGame)
            yield break;
        if (GameManager.instance.InUI)
            yield break;
        if (GameManager.instance.InSkill)
            yield break;
        if (GameManager.instance.InMove)
            yield break;
        if (GameManager.instance.InAnimation)
            yield break;
        

        GameManager.instance.turn++;
        turnText.text = GameManager.instance.turn.ToString();
        GameManager.instance.InAnimation = true;
        animationDelay = 0;
        counterAnimeDelay = 0;

        int dirX = 0;
        int dirY = 0;
        switch (dir)
        {
            case 1:
                dirX = -1;
                dirY = 0;
                playerSpiritList = playerSpiritList.OrderBy(x => x.LocX).ToList();
                enemyList = enemyList.OrderBy(x => x.LocX).ToList() ;

                break;
            case 2:
                dirX = 1;
                dirY = 0;
                playerSpiritList = playerSpiritList.OrderByDescending(x => x.LocX).ToList();
                enemyList = enemyList.OrderByDescending(x => x.LocX).ToList();
                break;
            case 3:
                dirX = 0;
                dirY = 1;
                playerSpiritList = playerSpiritList.OrderByDescending(x => x.LocY).ToList();
                enemyList = enemyList.OrderByDescending(x => x.LocY).ToList();
                break;
            case 4:
                dirX = 0;
                dirY = -1;
                playerSpiritList = playerSpiritList.OrderBy(x => x.LocY).ToList();
                enemyList = enemyList.OrderBy(x => x.LocY).ToList();
                break;
        }

        player.GetComponent<Player>().MoveAndAttack(dirX, dirY);

        yield return new WaitForSeconds(animationDelay);
        yield return new WaitForSeconds(counterAnimeDelay);

        if (GameManager.instance.doTutorial == 1)
            GameManager.instance.InAnimation = false;
        animationDelay = 0;
        counterAnimeDelay = 0;
        spiritAnimationDelay = 0;

        for(int i = 0; i < playerSpiritList.Count; i++)
        {
            playerSpiritList[i].MoveAndAttack(dirX, dirY);
            yield return new WaitForSeconds(spiritAnimationDelay);
            spiritAnimationDelay = 0;
        }

        yield return new WaitForSeconds(animationDelay);
        animationDelay = 0;

        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].Move(dirX, dirY);
        }

        yield return new WaitForSeconds(animationDelay);
        animationDelay = 0;


        if (GameManager.instance.doTutorial == 0)
            yield return new WaitForSeconds(0.2f);

        CreateEnemy();
        PlayerData.instance.player.GetComponent<Player>().LevelUI();
        GameManager.instance.InAnimation = false;
    
    }

    public void GetMoney(int money)
    {
        this.gameMoney += money;
        //while (true)
        //{
        //    if (tempMoney / 1000 > 1)
        //    {
        //        tempMoney = tempMoney / 1000;

        //    }
        //}
    }
    public void ApplyMoney()
    {
        GameManager.instance.money += this.gameMoney;
        gameMoney = 0;
    }

    private bool GameOverCheck()
    {
        int over = 0;
        for (int i = 0; i < board.BoardWidth; i++)
        {
            for (int j = 0; j < board.BoardHeight; j++)
            {
                if (map[i, j] == null)
                {
                    over = 1;
                }
            }
        }

        if (over == 0) return true;
        else return false;
    }
}
