using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        int musicSessionCount = FindObjectsOfType<GameSession>().Length;
        if (musicSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
