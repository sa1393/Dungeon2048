using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    float Interval = 1.125f; //간격

    int boardWidth = 5;
    int boardHeight = 5;

    GameObject[,] gameBoard;
    public GameObject[,] canSkillBoard;
    Sprite[,] tileSprite;

    Sprite[] groundTiles;
    Sprite[] canSkillTile;
    Sprite[] cantSkillTile;

    Transform boardHolder;

    public GameObject instance;

    public int BoardWidth { get => boardWidth; set => boardWidth = value; }
    public int BoardHeight { get => boardHeight; set => boardHeight = value; }

    private void Awake()
    {
        groundTiles = Resources.LoadAll<Sprite>("ui/environment/tile/tile/");
        canSkillTile = Resources.LoadAll<Sprite>("ui/environment/tile/able");
        cantSkillTile = Resources.LoadAll<Sprite>("ui/environment/tile/unable");
    }

    private void Start()
    {
        BoardInitialize();
    }

    public void BoardInitialize() //보드 처음
    {
        instance = new GameObject();
        instance.AddComponent<SpriteRenderer>();

        gameBoard = new GameObject[BoardWidth, BoardHeight];
        tileSprite = new Sprite[BoardWidth, BoardHeight];
        canSkillBoard = new GameObject[BoardWidth, BoardHeight];

        boardHolder = new GameObject("Board").transform;

        Debug.Log("보드 실행");
        for (int i = 0; i < BoardWidth; i++)
        {
            for (int j = 0; j < BoardHeight; j++)
            {
                canSkillBoard[i, j] = Instantiate(instance, new Vector2(i * Interval, j * Interval), Quaternion.identity);
                canSkillBoard[i, j].transform.localScale = new Vector3((float)1 / boardWidth * 3.6f, (float)1 / boardHeight * 3.6f, 1);
                canSkillBoard[i, j].GetComponent<SpriteRenderer>().sprite = null;
                canSkillBoard[i, j].transform.SetParent(boardHolder);
                canSkillBoard[i, j].GetComponent<SpriteRenderer>().sortingOrder = 1;

                
                gameBoard[i, j] = Instantiate(instance, new Vector2(i * Interval, j * Interval), Quaternion.identity);
                gameBoard[i, j].transform.localScale = new Vector3((float)1 / boardWidth * 3.6f, (float)1 / boardHeight * 3.6f, 1);
                tileSprite[i, j] = groundTiles[Random.Range(0, groundTiles.Length)];
                gameBoard[i, j].GetComponent<SpriteRenderer>().sprite = tileSprite[i, j];
                gameBoard[i, j].transform.SetParent(boardHolder);
            }
        }
        boardHolder.transform.position = new Vector2(-2.25f, -1.08f);
    }

    public void BoardReset() //보드 초기화
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            for (int j = 0; j < BoardHeight; j++)
            {
                canSkillBoard[i, j].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    public void CanSkill(int cross, int locX, int locY) // 사거리 플레이어 위치
    {
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {

                canSkillBoard[i, j].GetComponent<SpriteRenderer>().sprite = cantSkillTile[Random.Range(0, cantSkillTile.Length)];
            }
        }
        for (int i = cross * -1; i <= cross; i++)
        {
            for (int j = cross * -1; j <= cross; j++)
            {
                if (locX + i < 0 || locX + i > 4 || locY + j > 4 || locY + j < 0) continue;
                else if (GameManager.instance.map[i + locX, j + locY] == null || GameManager.instance.map[locX + i, locY + j].GetComponent<Enemy>() != null)
                {
                    canSkillBoard[locX + i, locY + j].GetComponent<SpriteRenderer>().sprite = canSkillTile[Random.Range(0, canSkillTile.Length)];
                }
            }
        }
    }
}