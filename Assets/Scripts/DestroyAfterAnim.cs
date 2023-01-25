using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnim : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    private void Start()
    {
        parentObject = GetComponentInParent<GameObject>();
        
        Destroy(parentObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    
}
