using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBall : MonoBehaviour
{
    // config params
    
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactorMin = -.5f;
    [SerializeField] float randomFactorMax = 1f;
    [SerializeField] float maxVelocityAllowed = 15f;
    [SerializeField] float startingVelocity1, startingVelocity2;

    // cached component references
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AudioSource audioSource;

    // state
    [SerializeField] Vector2 currentVelocity;

    // script refs
    Level level;
    SoundHub soundHub;
    GameSession gameSession;

    private void Awake()
    {
        level = FindObjectOfType<Level>();
        soundHub = FindObjectOfType<SoundHub>();
        gameSession = FindObjectOfType<GameSession>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startingVelocity1 = Random.Range(-maxVelocityAllowed, maxVelocityAllowed);
        startingVelocity2 = Random.Range(-maxVelocityAllowed, maxVelocityAllowed);
        rb.velocity = new Vector2(startingVelocity1, startingVelocity2);
        audioSource = GetComponent<AudioSource>();
        gameSession.AddBall();
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Breakable")
        {
            var collisionColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            spriteRenderer.color = collisionColor;
            soundHub.BallSound1();
        }
        else if (collision.gameObject.tag != "Breakable")
        {
            soundHub.BallSound2();
        }
        Vector2 velocityTweak = new Vector2(Random.Range(randomFactorMin, randomFactorMax), Random.Range(randomFactorMin, randomFactorMax));

        
        rb.velocity += velocityTweak;
        if (rb.velocity.x > maxVelocityAllowed)
        {
            rb.velocity = new Vector2(maxVelocityAllowed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxVelocityAllowed)
        {
            rb.velocity = new Vector2(-maxVelocityAllowed, rb.velocity.y);
        }
        if (rb.velocity.y > maxVelocityAllowed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityAllowed);
        }
        else if (rb.velocity.y < -maxVelocityAllowed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocityAllowed);
        }
    }
}
