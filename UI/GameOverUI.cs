using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text score;
    public Text maxScore;
    public Text money;

    private void Start()
    {
        score.text = GameManager.instance.turn.ToString();
        maxScore.text = GameManager.instance.turn.ToString();
        money.text = GameManager.instance.InGameScript.gameMoney.ToString();
        GameManager.instance.InGameScript.ApplyMoney();

    }

}
