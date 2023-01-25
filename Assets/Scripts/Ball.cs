using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] private float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] private float xPush;
    [SerializeField] private float xPushMin, xPushMax;
    
    [SerializeField] float ballSavedLaunchValue;
    [SerializeField] float timeSinceLaunched, timeSinceLaunchedLimit;
    [SerializeField] public float speed;
    
    
    
    
    
    // cached component references
    [SerializeField] Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    [SerializeField] AudioSource audioSource;
    
    // state
    
    [SerializeField] bool hasLaunched = false;
    [SerializeField] bool isSavedBall = false;
    [SerializeField] bool alignedRight, alignedLeft, alignedUp, alignedDown;
    [SerializeField] bool canHoldBall = true;
    [SerializeField] bool hasVelocityBeenSaved;
    [SerializeField] bool hasGameResumed;
    [SerializeField] Vector2 savedVelocity;
    [SerializeField] Vector2 _velocity;
    [SerializeField] Vector2 ballSpawnPointOnPaddle;
    [SerializeField] Transform ballSaverHoldPosition;

    // script refs
    Level level;
    SoundHub soundHub;
    GameSession gameSession;
    [SerializeField] Paddle paddle1;
    PauseSystem pauseSystem;

    private void Awake()
    {
        level = FindObjectOfType<Level>();
        soundHub = FindObjectOfType<SoundHub>();
        gameSession = FindObjectOfType<GameSession>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        paddle1 = FindObjectOfType<Paddle>();
        
    }
    void Start()
    {
        hasLaunched = false;
        pauseSystem = FindObjectOfType<PauseSystem>();
    }
    
    void Update()
    {
        if(!pauseSystem.isPaused)
        {
            savedVelocity = _velocity;
        }
        ballSpawnPointOnPaddle = FindObjectOfType<Paddle>().ballSpawnPoint;

        if (!hasLaunched && !isSavedBall)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        if(isSavedBall && !hasLaunched)
        {
            LaunchWithRightClick();
            
        }
        if(!hasLaunched && isSavedBall)
        {
            LockBallToSaverLauncher();
        }

        if(!canHoldBall)
        {
            timeSinceLaunched += Time.deltaTime;
            if(timeSinceLaunched > timeSinceLaunchedLimit)
            {
                canHoldBall = true;
            }
        }
    }

    private void LockBallToSaverLauncher()
    {

        Vector2 ballSaverPos = new Vector2(ballSaverHoldPosition.transform.position.x, ballSaverHoldPosition.transform.position.y);
        transform.position = new Vector2(ballSaverHoldPosition.transform.position.x, ballSaverHoldPosition.transform.position.y);
    }

    void FixedUpdate()
    {
        if(!pauseSystem.isPaused && !pauseSystem.hasGameResumed && hasLaunched)
        {
            rb.velocity = rb.velocity.normalized * speed;
            _velocity = rb.velocity;
        }
        else if(!pauseSystem.isPaused && pauseSystem.hasGameResumed)
        {
            rb.velocity = savedVelocity;
            _velocity = rb.velocity;
        }
        else if(pauseSystem.isPaused)
        {
            if(!hasVelocityBeenSaved)
            {
                savedVelocity = rb.velocity;
                hasVelocityBeenSaved = true;
            }
            if(hasVelocityBeenSaved)
            {
                rb.velocity = rb.velocity.normalized * 0;
            }
            
        }
        
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = new Vector2(ballSpawnPointOnPaddle.x, ballSpawnPointOnPaddle.y);
    }

    private void LaunchWithRightClick()
    {
        if(isSavedBall && !hasLaunched)
        {
            if(Input.GetMouseButtonDown(1))
            {
                
                canHoldBall = false;
                if (alignedRight)
                {
                    rb.velocity = new Vector2(ballSavedLaunchValue, 0);
                }
                else if(alignedLeft)
                {
                    rb.velocity = new Vector2(-ballSavedLaunchValue, 0);
                }
                else if (alignedUp)
                {
                    rb.velocity = new Vector2(0, ballSavedLaunchValue);
                }
                else if (alignedDown)
                {
                    rb.velocity = new Vector2(0, -ballSavedLaunchValue);
                }
                alignedRight = false;
                alignedLeft = false;
                alignedUp = false;
                alignedDown = false;
                
                
                soundHub.BallSound1();
                hasLaunched = true;
            }
            
        }
    }
    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0) && !isSavedBall)
        {
            xPush = Random.Range(xPushMin, xPushMax);
            hasLaunched = true;
            rb.velocity = new Vector2(xPush, yPush);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLaunched)
        {
            //SoundManager.PlaySound(SoundManager.Sound.ballHitSound);
            //soundHub.BallSound1();
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
            
            rb.velocity = Vector2.Reflect(_velocity, collision.GetContact(0).normal);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball Saver Right")
        {
            
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Right").transform;
            alignedRight = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Right Small")
        {

            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Right Small").transform;
            alignedRight = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Left")
        {
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Left").transform;
            alignedLeft = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Left Small")
        {
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Left Small").transform;
            alignedLeft = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Up")
        {
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Up").transform;
            alignedUp = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Up Small")
        {
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Up Small").transform;
            alignedUp = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Down")
        {
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Down").transform;
            alignedDown = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
        if (collision.gameObject.tag == "Ball Saver Down Small")
        {
            ballSaverHoldPosition = GameObject.FindGameObjectWithTag("Ball Saver Hold Position Down Small").transform;
            alignedDown = true;
            isSavedBall = true;
            hasLaunched = false;
            LockBallToSaverLauncher();
            
        }
    }


}
