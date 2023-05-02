using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolsSpawner : MonoBehaviour
{
    private const int NUMBER_TO_SPAWN = 3;

    [SerializeField] private GameObject pistolPrefab;
    [SerializeField] private GameObject[] pistolSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Now, for testing
        SpawnPistols();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPistols()
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
                Instantiate(pistolPrefab);
                pistolPrefab.transform.position = pistolSpawnPoints[indexToSpawn].gameObject.transform.position;
                spawnedIndexes.Add(indexToSpawn);
                spawnedPistols++;
            }
        }
    }
}
