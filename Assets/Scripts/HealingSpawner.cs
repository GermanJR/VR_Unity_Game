using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSpawner : MonoBehaviourPun
{

    [SerializeField] private GameObject[] healingSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnCoroutine());

        }    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnCoroutine()
    {
        //int index;
        GameObject[] allOrbs;
        while (true)
        {
            yield return new WaitForSeconds((float)Random.Range(10, 26));

            allOrbs = GameObject.FindGameObjectsWithTag("HealingOrb"); ;
            if (allOrbs.Length != 0)
            {
                continue;
            }
            Debug.Log("Shoul spawn orb");
            /*
            index = Random.Range(0, healingSpawnPoints.Length);
            PhotonNetwork.Instantiate("HealingOrb", healingSpawnPoints[index].transform.position, healingSpawnPoints[index].transform.rotation);
            */
        }
    }
}
