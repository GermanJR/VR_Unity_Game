using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERZone1Spawner : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int positionIndex = 0;
        foreach (GameObject player in players)
        {
            player.transform.position = spawnPoints[positionIndex].position;
            positionIndex++;
        }
    }
}
