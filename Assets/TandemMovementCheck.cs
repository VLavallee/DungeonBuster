using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TandemMovementCheck : MonoBehaviour
{
    [SerializeField] GameObject firstObj, secondObj, thirdObj, fourthObj;
    [SerializeField] bool firstObjHasReachedCheckPoint, secondObjHasReachedCheckPoint, thirdObjHasReachedCheckPoint, fourthObjHasReachedCheckPoint,
        firstObjCanMove, secondObjCanMove, thirdObjCanMove, fourthObjCanMove, allObjectsAtCheckPoint;

    private void Update()
    {
        if(firstObjHasReachedCheckPoint)
        {
            if(!secondObjHasReachedCheckPoint || !thirdObjHasReachedCheckPoint || !fourthObjHasReachedCheckPoint)
            firstObjCanMove = false;
        }
        if(secondObjHasReachedCheckPoint)
        {
            if(!firstObjHasReachedCheckPoint || !thirdObjHasReachedCheckPoint || !fourthObjHasReachedCheckPoint)
            secondObjCanMove = false;
        }
        if(thirdObjHasReachedCheckPoint)
        {
            if(!firstObjHasReachedCheckPoint || !secondObjHasReachedCheckPoint || !fourthObjHasReachedCheckPoint)
            thirdObjCanMove = false;
        }
        if(fourthObjHasReachedCheckPoint)
        {
            if(!firstObjHasReachedCheckPoint || !secondObjHasReachedCheckPoint || !thirdObjHasReachedCheckPoint)
            fourthObjCanMove = false;
        }
        if(firstObjHasReachedCheckPoint && secondObjHasReachedCheckPoint && thirdObjHasReachedCheckPoint && fourthObjHasReachedCheckPoint)
        {
            ResumeTandemMovement();
        }
    }
    private void ResumeTandemMovement()
    {
        firstObjCanMove = true;
        secondObjCanMove = true;
        thirdObjCanMove = true;
        fourthObjCanMove = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.CompareTag("First Obj") && collision.CompareTag("Check Point"))
        {
            firstObjHasReachedCheckPoint = true;
        }
        if (gameObject.CompareTag("Second Obj") && collision.CompareTag("Check Point"))
        {
            secondObjHasReachedCheckPoint = true;
        }
        if (gameObject.CompareTag("Third Obj") && collision.CompareTag("Check Point"))
        {
            thirdObjHasReachedCheckPoint = true;
        }
        if (gameObject.CompareTag("Fourth Obj") && collision.CompareTag("Check Point"))
        {
            fourthObjHasReachedCheckPoint = true;
        }
    }

}
