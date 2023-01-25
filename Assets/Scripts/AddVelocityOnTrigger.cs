using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocityOnTrigger : MonoBehaviour
{
    [SerializeField] float velocityToAdd;
    [SerializeField] bool addVelocityRight, addVelocityLeft, addVelocityUp, addVelocityDown;
    [SerializeField] Rigidbody2D ballRB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if(addVelocityRight)
            {
                ballRB.velocity = new Vector2(velocityToAdd, 0);
            }
            if(addVelocityLeft)
            {
                ballRB.velocity = new Vector2(-velocityToAdd, 0);
            }
            if(addVelocityUp)
            {
                ballRB.velocity = new Vector2(0, velocityToAdd);
            }
            if(addVelocityDown)
            {
                ballRB.velocity = new Vector2(0, -velocityToAdd);
            }
        }
    }
}
