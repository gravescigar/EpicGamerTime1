using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    float _health;
    public bool _jumping;
    public bool _grounded;
    float _timeBetweenJumps = 1000f;
    public float _jumpcooldown;
    float m_jumpForce = 6f;
    int _random_number;
    int _timesjumped;

    // Start is called before the first frame update
    void Start()
    {
        _timesjumped = 0;
        _jumpcooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_jumpcooldown == 0)
        {
            Jump();
        }
        if (_jumpcooldown > 0)
            _jumpcooldown -= 1;
    }

    void Jump()
    {
        if (_timesjumped == 0 && _grounded)
        {
            _random_number = Random.Range(0, 2);
            if (_random_number < 1)
                rb.velocity = new Vector2(-3, m_jumpForce);
            else
                rb.velocity = new Vector2(3, m_jumpForce);
            _jumpcooldown = _timeBetweenJumps;
        }
    }

    #region OnCollisionEnter/Exit
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.sharedMaterial.name);
        if (collision.collider.tag == "Ground" && collision.collider.sharedMaterial.name == "Ground")
        {
            _jumping = false;
            _grounded = true;
            _timesjumped = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.tag == "Ground" && collision.collider.sharedMaterial.name == "Ground")
        {
            _grounded = false;
        }
    }
    #endregion
}
