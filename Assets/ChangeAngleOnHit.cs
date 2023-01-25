using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAngleOnHit : MonoBehaviour
{
    [SerializeField] Transform zeroRotation, quarterRotation, fullRotation;
    [SerializeField] bool isZeroRotation, isQuarterRotation, isFullRotation, isRotating;
    [SerializeField] float switchTime;
    [SerializeField] float switchTimeLimit = 0.125f;
    
    private void Start()
    {
        isZeroRotation = true;
    }

    private void Update()
    {
        if(isRotating)
        {
            switchTime += Time.deltaTime;
            if(switchTime > switchTimeLimit)
            {
                if(isZeroRotation)
                {
                    gameObject.transform.rotation = quarterRotation.rotation;
                    isRotating = false;
                    switchTime = 0;
                    return;
                }
                if(isQuarterRotation)
                {
                    gameObject.transform.rotation = fullRotation.rotation;
                    isRotating = false;
                    switchTime = 0;
                    return;
                }
                if(isFullRotation)
                {
                    gameObject.transform.rotation = zeroRotation.rotation;
                    isRotating = false;
                    switchTime = 0;
                    return;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<SoundHub>().BallSound1();
        if(isZeroRotation)
        {
            //gameObject.transform.rotation = quarterRotation.rotation;
            isQuarterRotation = true;
            isZeroRotation = false;
            isRotating = true;
        }
        if (isQuarterRotation)
        {
            //gameObject.transform.rotation = fullRotation.rotation;
            isFullRotation = true;
            isQuarterRotation = false;
            isRotating = true;
        }
        if(isFullRotation)
        {
            //gameObject.transform.rotation = zeroRotation.rotation;
            isZeroRotation = true;
            isFullRotation = false;
            isRotating = true;
        }
    }
}
