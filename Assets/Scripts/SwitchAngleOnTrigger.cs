using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAngleOnTrigger : MonoBehaviour
{
    [SerializeField] Transform zeroRotation, quarterRotation, fullRotation;
    [SerializeField] bool isZeroRotation, isQuarterRotation, isFullRotation;
    

    private void Start()
    {
        isZeroRotation = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<SoundHub>().BallSound1();
        if (isZeroRotation)
        {
            gameObject.transform.rotation = quarterRotation.rotation;
            isQuarterRotation = true;
            isZeroRotation = false;
        }
        if (isQuarterRotation)
        {
            gameObject.transform.rotation = fullRotation.rotation;
            isFullRotation = true;
            isQuarterRotation = false;
        }
        if (isFullRotation)
        {
            gameObject.transform.rotation = zeroRotation.rotation;
            isZeroRotation = true;
            isFullRotation = false;
        }
    }
}
