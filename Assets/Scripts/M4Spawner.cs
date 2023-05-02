using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4Spawner : MonoBehaviour
{
    [SerializeField] private GameObject M4Prefab;
    [SerializeField] private GameObject[] M4SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Now, for testing
        SpawnM4();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnM4()
    {
        M4Prefab.transform.position = M4SpawnPoints[Random.Range(0, M4SpawnPoints.Length)].transform.position;
        M4Prefab.SetActive(true);
    }
}
