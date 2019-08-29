using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    #region Objects
    [SerializeField]
    Rigidbody2D slimeRb;
    Transform player;
    #endregion

    #region Properties
    float awakeDistance;
    float aggroDistance;
    bool jumping;
    bool grounded;
    int times_jumped;
    public float jump_cooldown;
    float jumpcooldowntime;
    float distanceToPlayer;
    float jumpForce;
    bool woke;
    bool aggro;
    float maxHealth;
    float currentHealth;
    public float percentageHealth;
    #endregion

    #region Start/Update/FixedUpdate
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aggroDistance = 8f;
        awakeDistance = 15f;
        times_jumped = 0;
        jumpcooldowntime = 150f;
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        jumpForce = 3f;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        percentageHealth = currentHealth / maxHealth;
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= awakeDistance)
        {
            woke = true;
        }
        if (woke)
        {
            if (distanceToPlayer <= aggroDistance)
            {
                aggro = true;
                // When Player is at the left side of the Slime 
                if (transform.position.x > player.position.x)
                {
                    if (distanceToPlayer < jumpForce)
                        Jump(-distanceToPlayer, jumpForce * 2f);
                    else
                        Jump(-jumpForce, jumpForce * 2f);
                }
                // When Player is at the right side of the Slime
                else if (transform.position.x < player.position.x)
                {
                    if (distanceToPlayer < jumpForce)
                        Jump(distanceToPlayer, jumpForce * 2f);
                    else
                        Jump(jumpForce, jumpForce * 2f);
                }
                //When their x position is equal
                else
                {
                    Jump(slimeRb.velocity.x, jumpForce * 2f);
                }
            }
            else
            {
                aggro = false;
                Act();
            }
        }
        if (jump_cooldown > 0)
        {
            jump_cooldown--;
        }
    }
    #endregion

    #region Methods

    #region Act
    void Act()
    {
        if (!aggro)
        {
            if (Random.Range(0,2) == 0)
            {
                Jump(-jumpForce + 1, jumpForce * 2);
            }
            else
            {
                Jump(jumpForce - 1, jumpForce * 2);
            }
        }
    }
    #endregion

    #region Jump
    void Jump(float jumpForceSideways, float jumpForceUpwards)
    {
        if (times_jumped == 0 && jump_cooldown == 0 && grounded)
        {
            slimeRb.velocity = new Vector2(jumpForceSideways, jumpForceUpwards);
            times_jumped++;
            jump_cooldown = jumpcooldowntime;
            jumping = true;
        }
    }
    #endregion

    #region OnCollisionEnter/Exit
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "Player")
        {
            grounded = true;
            jumping = false;
            times_jumped = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "Player")
        {
            grounded = false;
        }
    }
    #endregion

    #region TakeDamage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    #endregion

    #endregion
}
