using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform parent;

    public static PlayerController sharedInstance;
    public DeathView deathView;
    public CapsuleCollider2D feetCollider;

    public float temporaryMaxDistance;

    //Player movement variables
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float runningSpeed = 2.0f;
    [SerializeField] Vector2 movement;
    //[SerializeField] Vector3 characterRotationVector;
    SpriteRenderer m_spriteRenderer;
    public bool facingLeft;

    Rigidbody2D m_rigidBody2D;
    Animator animator;

    Vector3 startPosition;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_VERTICAL_VELOCITY = "verticalVelocity";
    const string STATE_IS_RUNNING = "isRunning";

    [SerializeField]
    int healthPoints, manaPoints;
    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 200, MAX_MANA = 30, MIN_HEALTH = 0, MIN_MANA = 0;
    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.3f;

    public LayerMask groundMask;
    [SerializeField] float rayLenght = 2f;

    public GameObject gameOverSound;

    void Awake()
    {
        SetSingleton();
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverSound = GameObject.Find("GameOverSFX");
        facingLeft = false;
        m_spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        deathView.ResetDeathViewValues();
        GameManager.sharedInstance.collectedCoins = 0;
        temporaryMaxDistance = 0;
        SetInitialPoints();
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
        this.transform.position = startPosition;
        m_rigidBody2D.velocity = Vector2.zero;
        CameraFollow.sharedInstance.ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        parent = this.transform.parent;

        JumpCheck();

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        animator.SetFloat(STATE_VERTICAL_VELOCITY, m_rigidBody2D.velocity.y);

        if (m_rigidBody2D.velocity.x != 0f)
        {
            animator.SetBool(STATE_IS_RUNNING, true);
        }
        else if(m_rigidBody2D.velocity.x == 0f)
        {
            animator.SetBool(STATE_IS_RUNNING, false);
        }

        SpriteFlip();

        //Liea que se dibuja para simbolizar el raycast que detecta si el suelo esta siendo pisado
        Debug.DrawRay(this.transform.position, Vector2.down * rayLenght, Color.red);

        SetFeetCollider();
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame && GetInput().x != 0)
        {
            MoveCharacter(GetInput());
        }
        //Si no estamos dentro de la partida la velocidad del personaje pasara a ser 0
        /*else
        {
            m_rigidBody2D.velocity = new Vector2(0f, m_rigidBody2D.velocity.y);
        }*/
        

    }

    void SpriteFlip()
    {
        if (GetInput().x > 0.1f)
        {
            facingLeft = false;
            m_spriteRenderer.flipX = facingLeft;
        }
        else if (GetInput().x < -0.1f)
        {
            facingLeft = true;
            m_spriteRenderer.flipX = facingLeft;
        }

        
    }

    //Apply a vertical force to the character
    public void Jump(bool superJump)
    {
        float jumpForceFactor = jumpForce;

        
        if (IsTouchingTheGround())
        {
            if (superJump && manaPoints >= SUPERJUMP_COST)
            {
                 manaPoints -= SUPERJUMP_COST;
                 jumpForceFactor *= SUPERJUMP_FORCE;
             }

            GetComponent<AudioSource>().Play();
            m_rigidBody2D.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }

    void JumpCheck()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.LeftShift))
            {
                Jump(true);
            }
            else if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.LeftShift))
            {
                Jump(false);
            }
        }
    }

    //Returns true if the character is touching the ground, and false if it is not
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, rayLenght, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //This method receive as parameter a vector2 (like GetInput()) set it as parameter 
    //of the rigidbody2D velocity
    void MoveCharacter(Vector2 direction)
    {
        m_rigidBody2D.velocity = new Vector2(direction.x * runningSpeed, m_rigidBody2D.velocity.y);
    }


    //This method receive the axis input and returnsa Vector2 with the movement
    Vector2 GetInput()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), m_rigidBody2D.velocity.y);
        return movement;
    }

    public void Die()
    {
        gameOverSound.GetComponent<AudioSource>().Play();
        deathView.SetDeathViewValues(GetTravelledDistance());
        SetMaxScore();
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    void SetSingleton()
    {
        if (sharedInstance == null) { sharedInstance = this; }
    }

    void SetInitialPoints() { healthPoints = INITIAL_HEALTH; manaPoints = INITIAL_MANA; }

    public void CollectHealth(int points)
    {
        healthPoints = Mathf.Clamp(healthPoints += points, MIN_HEALTH, MAX_HEALTH);

        DeathCheck(healthPoints);
    }

    void DeathCheck(int health)
        {
            if (health <= 0)
            {
                Die();
            }
        }

    public void CollectMana(int points)
    {
        manaPoints = Mathf.Clamp(manaPoints += points, MIN_MANA, MAX_MANA);
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        float distance = this.transform.position.x - startPosition.x;
        if (distance > temporaryMaxDistance)
        {
            temporaryMaxDistance = distance;
            return distance;
        }
        else
        {
            return temporaryMaxDistance;
        }
    }

    void SetMaxScore()
    {
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }

    void SetFeetCollider()
    {
        if (IsTouchingTheGround())
        {
            feetCollider.enabled = true;
        }
        else
        {
            feetCollider.enabled = false;
        }
    }

    
}
