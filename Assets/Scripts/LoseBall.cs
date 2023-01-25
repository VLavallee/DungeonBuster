using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseBall : MonoBehaviour
{
    Level level;
    [SerializeField] AudioClip loseBallSound;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }
}
