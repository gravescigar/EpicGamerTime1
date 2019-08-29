using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{   
    [SerializeField]
    GameObject parent;
    Vector3 localScale;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.name == "Player")
        {
            PlayerController playerScript = parent.GetComponent<PlayerController>();
            health = playerScript.percentageHealth;
        }
        if (parent.name == "Slime")
        {
            SlimeController slimeScript = parent.GetComponent<SlimeController>();
            health = slimeScript.percentageHealth;
        }
        transform.localScale = new Vector3(localScale.x * health, localScale.y);
    }
}
