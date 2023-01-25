using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallPowerUp : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Vector3 ball1Offset, ball2Offset;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Instantiate(ballPrefab, transform.position + ball1Offset, transform.rotation);
            Instantiate(ballPrefab, transform.position + ball2Offset, transform.rotation);

            Destroy(gameObject);

        }
    }
}
