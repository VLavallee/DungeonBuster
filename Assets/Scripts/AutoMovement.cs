using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float moveSpeed = 1;
    [SerializeField] float xPosMin, xPosMax, yPosMin, yPosMax;
    [SerializeField] bool canMove, isMovingRight, isMovingLeft, isMovingUp, isMovingDown, isLooping, isDelayedMovement;
    [SerializeField] Vector2 objectPosition;
    [SerializeField] bool shouldStopAtMinAndMax;
    [SerializeField] float stopTime, stopTimeLimit, startTime, startTimeLimit;

    PauseSystem pauseSystem;
    //Tandem Movement

    private void Start()
    {
        pauseSystem = FindObjectOfType<PauseSystem>();

        if(!isDelayedMovement)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
    void Update()
    {
        if(!pauseSystem.isPaused)
        {
            if (isDelayedMovement && !canMove)
            {
                startTime += Time.deltaTime;
                if (startTime > startTimeLimit)
                {
                    canMove = true;
                    startTime = 0;
                }
            }
            if (canMove)
            {
                if (isMovingUp)
                {
                    MoveUp();
                }
                if (isMovingDown)
                {
                    MoveDown();
                }
                if (isMovingRight)
                {
                    MoveRight();
                }
                if (isMovingLeft)
                {
                    MoveLeft();
                }
                if (objectPosition.y == yPosMax)
                {
                    if (!isLooping)
                    {
                        isMovingUp = false;
                    }
                    if (shouldStopAtMinAndMax && !isLooping)
                    {
                        stopTime += Time.deltaTime;
                        if (stopTime > stopTimeLimit)
                        {
                            isMovingDown = true;
                            stopTime = 0;
                        }
                    }
                }
                if (objectPosition.y == yPosMin)
                {
                    if (!isLooping)
                    {
                        isMovingDown = false;
                    }
                    if (shouldStopAtMinAndMax && !isLooping)
                    {
                        stopTime += Time.deltaTime;
                        if (stopTime > stopTimeLimit)
                        {
                            isMovingUp = true;
                            stopTime = 0;
                        }
                    }
                }

                if (objectPosition.x == xPosMax)
                {
                    if (!isLooping)
                    {
                        isMovingRight = false;
                    }
                    if (shouldStopAtMinAndMax && !isLooping)
                    {
                        stopTime += Time.deltaTime;
                        if (stopTime > stopTimeLimit)
                        {
                            isMovingLeft = true;
                            stopTime = 0;
                        }
                    }
                }
                if (objectPosition.x == xPosMin)
                {
                    if (!isLooping)
                    {
                        isMovingLeft = false;
                    }
                    if (shouldStopAtMinAndMax && !isLooping)
                    {
                        stopTime += Time.deltaTime;
                        if (stopTime > stopTimeLimit)
                        {
                            isMovingRight = true;
                            stopTime = 0;
                        }
                    }
                }
            }
        }
        
        
    }
    
    

    private void MoveUp()
    {
        objectPosition = new Vector2(transform.position.x, transform.position.y);
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        objectPosition.y = Mathf.Clamp(GetYPos(), yPosMin, yPosMax);
        transform.position = objectPosition;
    }
    private void MoveDown()
    {
        objectPosition = new Vector2(transform.position.x, transform.position.y);
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        objectPosition.y = Mathf.Clamp(GetYPos(), yPosMin, yPosMax);
        transform.position = objectPosition;
    }
    private void MoveRight()
    {
        objectPosition = new Vector2(transform.position.x, transform.position.y);
        transform.Translate(moveSpeed * Time.deltaTime, 0 , 0);
        objectPosition.x = Mathf.Clamp(GetXPos(), xPosMin, xPosMax);
        transform.position = objectPosition;
    }
    private void MoveLeft()
    {
        objectPosition = new Vector2(transform.position.x, transform.position.y);
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        objectPosition.x = Mathf.Clamp(GetXPos(), xPosMin, xPosMax);
        transform.position = objectPosition;
    }

    private float GetXPos()
    {
        return transform.position.x;
    }
    private float GetYPos()
    {
        return transform.position.y;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loop Left") && objectPosition.y == yPosMax)
        {
            objectPosition.y = yPosMax;
            isMovingUp = false;
            isMovingLeft = true;
        }

        else if (collision.gameObject.CompareTag("Loop Down") && objectPosition.x == xPosMin)
        {
            objectPosition.x = xPosMin;
            isMovingLeft = false;
            isMovingDown = true;
        }

        else if (collision.gameObject.CompareTag("Loop Right") && objectPosition.y == yPosMin)
        {
            objectPosition.y = yPosMin;
            isMovingDown = false;
            isMovingRight = true;
        }

        else if (collision.gameObject.CompareTag("Loop Up") && objectPosition.x == xPosMax)
        {
            objectPosition.x = xPosMax;
            isMovingRight = false;
            isMovingUp = true;
        }
    }
}
