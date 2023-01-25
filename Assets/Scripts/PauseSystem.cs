using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] public bool isPaused = false;
    [SerializeField] public bool hasGameResumed, canPauseAndUnPause;
    [SerializeField] float isPausedTime, isPausedTimeLimit;
    [SerializeField] GameObject pausedCanvas;

    private void Update()
    {
        if(canPauseAndUnPause == false)
        {
            isPausedTime += Time.deltaTime;
            if(isPausedTime > isPausedTimeLimit)
            {
                canPauseAndUnPause = true;
                hasGameResumed = false;
                isPausedTime = 0;
            }
        }

        if(isPaused)
        {
            pausedCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else if(!isPaused)
        {
            pausedCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && canPauseAndUnPause)
        {
            canPauseAndUnPause = false;
            if(!isPaused)
            {
                isPaused = true;
            }
            else if(isPaused)
            {
                isPaused = false;
                hasGameResumed = true;
            }
        }
    }
}
