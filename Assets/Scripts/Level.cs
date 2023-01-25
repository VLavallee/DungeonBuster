using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    //params
    [SerializeField] int breakableBlocks; //Serialized for debugging
    [SerializeField] int ballsInPlay; //Serialized for debugging
    [SerializeField] bool readyToLoadNextScene = false;
    [SerializeField] bool freeBallTextActive;
    [SerializeField] float freeBallTextTime, freeBallTextTimer;
    public bool readyToLoadGameOverScene = false;
    [SerializeField] float winSceneTime, winSceneTimeLimit;
    [SerializeField] float loseSceneTime, loseSceneTimeLimit;

    //cached refs
    SceneLoader sceneLoader;
    [SerializeField] GameObject freeBallEarnedText;
    [SerializeField] GameObject levelCompleteText;
    [SerializeField] public GameObject ballPrefab;

    private void Awake()
    {
        //SoundManager.Initialize();
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        levelCompleteText.SetActive(false);
        freeBallTextActive = false;
        freeBallEarnedText.SetActive(false);
    }

    private void Update()
    {
        if(readyToLoadNextScene)
        {
            levelCompleteText.SetActive(true);
            winSceneTime += Time.deltaTime;
            if(winSceneTime > winSceneTimeLimit)
            {
                sceneLoader.LoadNextScene();
            }
        }
        if(readyToLoadGameOverScene)
        {
            loseSceneTime += Time.deltaTime;
            if(loseSceneTime > loseSceneTimeLimit)
            {
                sceneLoader.LoadGameOverScreen();
            }
        }
        if(freeBallTextActive)
        {
            freeBallEarnedText.SetActive(true);
            freeBallTextTime += Time.deltaTime;
            if(freeBallTextTime > freeBallTextTimer)
            {
                freeBallEarnedText.SetActive(false);
                freeBallTextActive = false;
            }
        }
    }

    public void DisplayFreeBallEarnedText()
    {
        freeBallTextTime = 0f;
        freeBallTextActive = true;
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void AddBallToPlay()
    {
        ballsInPlay++;
    }

    public void SubtractBallFromPlay()
    {
        ballsInPlay--;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        
        if(breakableBlocks <= 0)
        {
            FindObjectOfType<SoundHub>().WinLevelSound();
            if (FindObjectOfType<Ball>() != null)
            {
                FindObjectOfType<Ball>().speed = 1f;
            }
            readyToLoadNextScene = true;
            //var activeBall = GameObject.FindGameObjectsWithTag("Ball");
            //if(activeBall != null)
            //{
            //    foreach (GameObject ball in activeBall)
            //    {
            //        var activeRB = ball.GetComponent<Rigidbody2D>();
            //        activeRB.velocity = new Vector2(0, 0);
            //        activeRB.gravityScale = -.1f;
            //    }
            //}
        }
    }
}
