using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed;
    PauseSystem pauseSystem;

    private void Start()
    {
        pauseSystem = FindObjectOfType<PauseSystem>();
    }
    void FixedUpdate()
    {
        if(!pauseSystem.isPaused)
        {
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed) * Time.deltaTime);
        }
        
    }
}
