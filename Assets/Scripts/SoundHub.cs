using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHub : MonoBehaviour
{
    [SerializeField] AudioClip currentBreakSound, blockSound0, blockSound1, blockSound2, blockSound3, ballSound1, ballSound2, winBallSound, loseBallSound,
        gameOverSound, winLevelSound;
    [SerializeField] float breakSoundTime, breakSoundStage1Time, breakSoundStage2Time, breakSoundStage3Time, breakSoundStage4Time, breakSoundStage5Time;
    [SerializeField] [Range(0, 1)] float ballSoundVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float breakSoundVolume = 0.5f;
    [SerializeField] bool stage1, stage2, stage3, stage4, stage5;

    private void Update()
    {
        if(!stage1)
        {
            currentBreakSound = blockSound0;
        }
        if(stage1)
        {
            currentBreakSound = blockSound1;
        }
        if(stage2)
        {
            currentBreakSound = blockSound2;
        }
        if (stage3)
        {
            currentBreakSound = blockSound3;
        }
        if (stage4)
        {
            currentBreakSound = winBallSound;
            stage1 = false;
            stage2 = false;
            stage3 = false;
            stage4 = false;
        }
        

    }
    public void BallSound1()
    {
        AudioSource.PlayClipAtPoint(ballSound1, Camera.main.transform.position, ballSoundVolume);
    }
    
    public void BallSound2()
    {
        AudioSource.PlayClipAtPoint(ballSound2, Camera.main.transform.position);
    }
    
    public void BreakSound()
    {
        AudioSource.PlayClipAtPoint(currentBreakSound, Camera.main.transform.position, breakSoundVolume);
        if (!stage1 && !stage2 && !stage3 && !stage4 && !stage5)
        {
            stage1 = true;
            return;
        }
        if (stage1 && !stage2 && !stage3 && !stage4 && !stage5)
        {
            stage2 = true;
            return;
        }
        if (stage2 && !stage3 && !stage4 && !stage5)
        {
            stage3 = true;
            return;
        }
        if (stage3 && !stage4 && !stage5)
        {
            stage4 = true;
            return;
        }
        if (stage4 && !stage5)
        {
            stage5 = true;
            return;
        }

    }

    public void WinBallSound()
    {
        AudioSource.PlayClipAtPoint(winBallSound, Camera.main.transform.position);
    }
    
    public void LoseBallSound()
    {
        AudioSource.PlayClipAtPoint(loseBallSound, Camera.main.transform.position);
    }
    public void GameOverSound()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }
    public void WinLevelSound()
    {
        AudioSource.PlayClipAtPoint(winLevelSound, Camera.main.transform.position);
    }
}
