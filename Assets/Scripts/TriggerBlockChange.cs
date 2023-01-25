using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlockChange : MonoBehaviour
{
    [SerializeField] GameObject[] deactivateOnTrigger;
    [SerializeField] GameObject[] activateOnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            foreach(GameObject currentlyActiveObject in deactivateOnTrigger)
            {
                currentlyActiveObject.SetActive(false);
            }
            foreach(GameObject currentlyInactiveObject in activateOnTrigger)
            {
                currentlyInactiveObject.SetActive(true);
            }
        }
    }
}
