using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    #region Objects
    public Rigidbody2D rb;
    public Animator animator;
    #endregion 

    #region Properties
    float m_moveSpeed = 3f;
    float m_jumpForce = 12f;
    public bool m_movingLeft;
    public bool m_movingRight;
    public bool m_Jumping;
    bool m_Grounded;
    public bool m_FacingRight;
    bool m_Ducking;
    int m_timesJumped;
    float maxHealth;
    float currentHealth;
    public float percentageHealth;
    #endregion

    #region Events
    //public UnityEvent OnMoveEvent;
    //public void OnMovement()
    //{
    //    animator.SetBool("IsMoving", true);
    //}
    #endregion

    #region Start/Update/FixedUpdate
    // Start is called before the first frame update
    void Start()
    {
        m_FacingRight = true;
        m_timesJumped = 0;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // track input keys
        m_movingLeft = Input.GetKey("a");
        m_movingRight = Input.GetKey("d");
        m_Ducking = Input.GetKey("s");

        if (Input.GetKeyDown("space"))
        {
            m_Jumping = true;
            PlayerJump();
            m_timesJumped++;
        }
        percentageHealth = currentHealth / maxHealth;
    }

    void FixedUpdate()
    {
        DetectAnimationState();
        PlayerMove(m_movingLeft, m_movingRight, m_Ducking);
    }
    #endregion

    #region Methods

    #region DetectAnimationState
    void DetectAnimationState()
    {
        if (m_Jumping)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
            if (m_movingLeft || m_movingRight)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
    #endregion

    #region OnCollisionEnter/Exit
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && collision.collider.sharedMaterial.name == "Ground")
        {
            m_Jumping = false;
            m_Grounded = true;
            m_timesJumped = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.tag == "Ground" && collision.collider.sharedMaterial.name == "Ground")
        {
            m_Grounded = false;
        }
    }
    #endregion

    #region PlayerMove
    void PlayerMove(bool movingLeft, bool movingRight, bool Ducking)
    {
        if (m_Grounded)
        {
            if (movingLeft)
            {
                if (m_FacingRight)
                {
                    PlayerFlip();
                }
                rb.velocity = new Vector2(-m_moveSpeed, rb.velocity.y);
            }
            if (movingRight)
            {
                if (!m_FacingRight)
                {
                    PlayerFlip();
                }
                rb.velocity = new Vector2(m_moveSpeed, rb.velocity.y);
            }
            if (Ducking)
            {
                animator.SetBool("IsDucking", true);
            }
            else
            {
                animator.SetBool("IsDucking", false);
            }
        }
        else
        {
            if (movingLeft)
            {
                if (m_FacingRight)
                {
                    PlayerFlip();
                }
                rb.velocity = new Vector2(-m_moveSpeed, rb.velocity.y);
            }
            if (movingRight)
            {
                if (!m_FacingRight)
                {
                    PlayerFlip();
                }
                rb.velocity = new Vector2(m_moveSpeed, rb.velocity.y);
            }
        }

    }
    #endregion

    #region PlayerJump
    void PlayerJump()
    {
        if (m_timesJumped < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, m_jumpForce);
        }
    }
    #endregion

    #region FlipPlayer
    public void PlayerFlip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
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
