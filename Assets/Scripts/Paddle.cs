using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX, maxX;
    //[SerializeField] bool hasInitialBallSpawned = false;
    [SerializeField] Transform spawnPoint;
    [SerializeField] public Vector2 ballSpawnPoint;
    
    

    //cached refs
    GameSession gameSession;
    PauseSystem pauseSystem;
    Ball ball;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        pauseSystem = FindObjectOfType<PauseSystem>();
    }
    void Update()
    {
        if(!pauseSystem.isPaused)
        {
            ballSpawnPoint = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
            transform.position = paddlePos;
        }
    }
        
    private float GetXPos()
    {
        if(gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
