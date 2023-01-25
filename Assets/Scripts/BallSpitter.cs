using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpitter : MonoBehaviour
{
    [SerializeField] GameObject theCapturedBall;
    [SerializeField] Rigidbody2D capturedBallRB;
    [SerializeField] bool shootRight, shootLeft, shootUp, shootDown;
    [SerializeField] bool ballIsCaptured, readyToFire, hasFired;
    
    [SerializeField] Transform ballCapturePoint;
    [SerializeField] float launchValue, readyToFireTime, readyToFireTimer, hasFiredTime, hasFiredTimer;
    [SerializeField] float originalAutoMoveSpeed, temporaryMoveSpeedMultiplier;

    private void Update()
    {
        if(readyToFire)
        {
            readyToFireTime += Time.deltaTime;
            if(readyToFireTime > readyToFireTimer)
            {
                if(shootRight)
                {
                    capturedBallRB.velocity = new Vector2(launchValue, launchValue / 2);
                }
                if(shootLeft)
                {
                    capturedBallRB.velocity = new Vector2(-launchValue, launchValue / 2);
                }
                if(shootUp)
                {
                    capturedBallRB.velocity = new Vector2(0, launchValue);
                }
                if (shootDown)
                {
                    capturedBallRB.velocity = new Vector2(0, -launchValue);
                }
                ballIsCaptured = false;
                hasFired = true;
                readyToFire = false;
                readyToFireTime = 0;
                
            }
        }
        if(hasFired)
        {
            GetComponent<AutoMovement>().moveSpeed = originalAutoMoveSpeed;
            hasFiredTime += Time.deltaTime;
            if(hasFiredTime > hasFiredTimer)
            {
                hasFired = false;
                hasFiredTime = 0;
            }
        }
        if(ballIsCaptured)
        {
            theCapturedBall.transform.position = ballCapturePoint.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFired && !ballIsCaptured && !readyToFire && collision.gameObject.tag == "Ball")
        {
            theCapturedBall = collision.gameObject;
            capturedBallRB = collision.gameObject.GetComponent<Rigidbody2D>();
            ballIsCaptured = true;
            originalAutoMoveSpeed = GetComponent<AutoMovement>().moveSpeed;
            GetComponent<AutoMovement>().moveSpeed = GetComponent<AutoMovement>().moveSpeed * temporaryMoveSpeedMultiplier;
        }

        if(ballIsCaptured && collision.gameObject.tag == "Launch Point")
        {
            readyToFire = true;
        }
    }
}
