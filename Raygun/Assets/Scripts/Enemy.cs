using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public float GetHealth()
    {
        return health;
    }
}
