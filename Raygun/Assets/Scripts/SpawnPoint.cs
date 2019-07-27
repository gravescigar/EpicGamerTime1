using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPlayer()
    {
        //Instantiate<GameObject>(player, transform.position, Quaternion.identity);
        GameObject newPlayer = Object.Instantiate(player) as GameObject;
        newPlayer.transform.position = transform.position;
    }
}
