using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    //config params
    [Range(0.5f, 5f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 100;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ballsAmountText;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] GameObject currentBall;
    
    //state variables
    [SerializeField] int currentScore = 0;
    [SerializeField] int currentBallsAmount = 1;
    [SerializeField] float ballResetTime, ballResetTimeLimit;
    [SerializeField] public bool isBallReset = false;
    [SerializeField] bool gameIsInSession;

    //cached refs
    SoundHub soundHub;
    Level level;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void Start()
    {
        level = FindObjectOfType<Level>();
        soundHub = FindObjectOfType<SoundHub>();
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
        ballsAmountText.text = currentBallsAmount.ToString();
    }
    
    void Update()
    {
        
        Time.timeScale = gameSpeed;
        if(currentBallsAmount == 0 && gameIsInSession)
        {
            FindObjectOfType<Level>().readyToLoadGameOverScene = true;
            gameIsInSession = false;
        }
        if(isBallReset)
        {
            ballResetTime += Time.deltaTime;
            if(ballResetTime > ballResetTimeLimit)
            {
                SpawnNewBallOnPaddle();
            }
        }
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
        if (currentScore == 1000 || currentScore == 2000 || currentScore == 3000 || currentScore == 4000 || currentScore == 5000 || currentScore == 6000 
            || currentScore == 7000 || currentScore == 8000 || currentScore == 9000 || currentScore == 10000 || currentScore == 11000 || currentScore == 12000 
            || currentScore == 13000 || currentScore == 14000 || currentScore == 15000)
        {
            FindObjectOfType<Level>().DisplayFreeBallEarnedText();
            currentBallsAmount++;
            soundHub.WinBallSound();
            ballsAmountText.text = currentBallsAmount.ToString();
        }
    }
    public void AddBall()
    {
        currentBallsAmount++;
        ballsAmountText.text = currentBallsAmount.ToString();
    }

    public void SpawnNewBallOnPaddle()
    {
        Transform paddlePos = FindObjectOfType<Paddle>().transform;
        currentBall = level.ballPrefab;
        GameObject newBall = Instantiate(currentBall, paddlePos.position, transform.rotation);
        newBall.SetActive(true);
        isBallReset = false;
        
    }

    public void SubtractBall()
    {
        currentBallsAmount--;
        ballsAmountText.text = currentBallsAmount.ToString();
        if (currentBallsAmount > 0)
        {
            isBallReset = true;
            ballResetTime = 0;
        }
    }

    public void ResetGame()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            soundHub.LoseBallSound();
            Destroy(collision.gameObject);
            SubtractBall();
        }
    }
    
}
