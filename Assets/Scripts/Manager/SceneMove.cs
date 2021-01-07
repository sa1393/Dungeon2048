using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour
{
    


    public void GameStart()
    {
        GameManager.instance.turn = 1;
        SceneManager.LoadScene("Game");

    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GoMain()
    {
        SceneManager.LoadScene("Main");
    }

    

    public void appOut()
    {
        Application.Quit();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
