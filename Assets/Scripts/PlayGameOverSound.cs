using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameOverSound : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<SoundHub>().GameOverSound();
    }
}
