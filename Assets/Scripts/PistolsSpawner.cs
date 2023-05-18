using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PistolsSpawner : MonoBehaviour
{
    private const int NUMBER_TO_SPAWN = 3;

    [SerializeField] private GameObject[] pistols;
    [SerializeField] private GameObject[] pistolSpawnPoints;

    private PhotonView photonView;

    private bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPistols()
    {
        if (spawned)
        {
            return;
        }
        else
        {
            photonView.RPC("SpawnPistolsOverNetwork", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SpawnPistolsOverNetwork()
    {
        List<int> spawnedIndexes = new List<int>();
        int spawnedPistols = 0;
        int indexToSpawn = Random.Range(0, pistolSpawnPoints.Length);
        bool isIndexTaken;

        while (spawnedPistols <= NUMBER_TO_SPAWN - 1)
        {
            isIndexTaken = false;

            foreach (int index in spawnedIndexes)
            {
                if (indexToSpawn == index)
                {
                    isIndexTaken = true;
                }
            }

            if (isIndexTaken)
            {
                indexToSpawn = Random.Range(0, pistolSpawnPoints.Length);
            }
            else
            {
                pistols[spawnedPistols].SetActive(true);
                pistols[spawnedPistols].transform.position = pistolSpawnPoints[indexToSpawn].gameObject.transform.position;
                spawnedIndexes.Add(indexToSpawn);
                spawnedPistols++;
            }
        }
    }
}
