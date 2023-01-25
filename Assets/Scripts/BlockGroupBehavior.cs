using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupBehavior : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool shouldMoveDown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(shouldMoveDown)
        {
            MoveBlockDown();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            shouldMoveDown = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            shouldMoveDown = true;
        }
    }


    private void MoveBlockDown()
    {
        rb.transform.position = new Vector2(rb.position.x, rb.transform.position.y + -2f * Time.deltaTime);
    }
}
