using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnBallCollide : MonoBehaviour
{
    private Rigidbody2D thisRb;


    private void Start()
    {
        thisRb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            var otherRb = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            otherRb = thisRb.velocity;
        }
    }
}
