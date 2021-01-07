using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class InputManager : MonoBehaviour
{
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistanceX;  //minimum distance for a swipe to be registered
    private float dragDistanceY;

    public enum SwipeDirection
    {
        None = 0, Left = 1, Right = 2, Up = 4, Down = 8
    }

    void Start()
    {
        dragDistanceX = Screen.width * 4.7f / 100;
        dragDistanceY = Screen.height * 8 / 100; //dragDistance is 15% height of the screen
    }

    private void Update()
    {
        if (!GameManager.instance.InGame)
            return;
        if (GameManager.instance.InUI)
            return;
        if (GameManager.instance.InSkill)
            return;
        if (GameManager.instance.InMove)
            return;
        if (GameManager.instance.InAnimation)
            return;
        if (GameManager.instance.doTutorial == 1)
            return;
        Swipe();
    }

    public void Swipe()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) 
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
              
            }
            else if (touch.phase == TouchPhase.Ended) 
            {
                lp = touch.position;

                if (Mathf.Abs(lp.x - fp.x) > dragDistanceX || Mathf.Abs(lp.y - fp.y) > dragDistanceY)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   
                        if ((lp.x > fp.x))
                        {   //Right 
                            StartCoroutine(GameManager.instance.InGameScript.Move(2));
                        }
                        else
                        {   //Left 
                            StartCoroutine(GameManager.instance.InGameScript.Move(1));
                        }
                    }
                    else
                    {   
                        if (lp.y > fp.y)
                        {   //Up 
                            StartCoroutine(GameManager.instance.InGameScript.Move(3));
                        }
                        else
                        {   //Downswipe
                            StartCoroutine(GameManager.instance.InGameScript.Move(4));
                        }
                    }
                }
                else
                { 
                    Debug.Log("Tap");
                }
            }
        }
    }

}
