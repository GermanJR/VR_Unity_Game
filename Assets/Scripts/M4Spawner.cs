using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class M4Spawner : MonoBehaviour
{
    [SerializeField] private GameObject M4Prefab;
    [SerializeField] private GameObject[] M4SpawnPoints;

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

    public void SpawnM4()
    {
        if (spawned)
        {
            return;
        }
        else 
        {
            photonView.RPC("SpawnM4OverNetwork", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SpawnM4OverNetwork()
    {
        M4Prefab.transform.position = M4SpawnPoints[Random.Range(0, M4SpawnPoints.Length)].transform.position;
        M4Prefab.SetActive(true);
    }
}
