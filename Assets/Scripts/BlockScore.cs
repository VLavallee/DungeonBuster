using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScore : MonoBehaviour
{
    public int blocksRemaining;
    public bool allBlocksDestroyed = false;

    private void Awake()
    {
        blocksRemaining = transform.childCount;
    }
    private void Update()
    {
        blocksRemaining = transform.childCount;

        if(blocksRemaining == 0)
        {
            allBlocksDestroyed = true;
        }
    }


}
