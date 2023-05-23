using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERZone1Manager : MonoBehaviourPun
{
    [SerializeField] private GameObject redCell;
    [SerializeField] private GameObject cyanCell;
    [SerializeField] private GameObject key;

    [SerializeField] private Transform[] redCellSpawns;
    [SerializeField] private Transform[] cyanCellSpawns;
    [SerializeField] private Transform[] keySpawns;

    [SerializeField] private Light[] lights;

    private bool redCellCorrect = false;
    private bool cyanCellCorrect = false;

    //private bool cellsSpawned = false;
    private bool keySpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        key.SetActive(false);
        foreach (Light light in lights)
        {
            light.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (redCellCorrect && cyanCellCorrect && !keySpawned)
        {
            RestorePower();
            SpawnKey();
            keySpawned = true;
        }
    }

    private void RestorePower()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        foreach (Light light in lights)
        {
            light.gameObject.SetActive(true);
        }
        photonView.RPC("RestorePowerOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void RestorePowerOverNetwork()
    {
        foreach (Light light in lights)
        {
            light.gameObject.SetActive(true);
        }
    }

    private void SpawnKey()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        Debug.Log("Spawning key");

        int keyIndex = Random.Range(0, cyanCellSpawns.Length);

        key.transform.position = keySpawns[keyIndex].position;

        key.SetActive(true);

        photonView.RPC("SpawnKeyOverNetwork", RpcTarget.Others, keySpawns[keyIndex]);
    }

    [PunRPC]
    private void SpawnKeyOverNetwork(Transform spawnTransform)
    {
        key.transform.position = spawnTransform.position;
        key.SetActive(true);
    }

    public void ChangeForRedCell()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        redCellCorrect = true;
        photonView.RPC("ChangeForRedCellOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void ChangeForRedCellOverNetwork()
    {
        redCellCorrect = true;
    }
    
    public void ChangeForCyanCell()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        redCellCorrect = true;
        photonView.RPC("ChangeForCyanCellOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void ChangeForCyanCellOverNetwork()
    {
        redCellCorrect = true;
    }

    public void SpawnCells()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        Debug.Log("Spawning cells");

        int redCellIndex = Random.Range(0, redCellSpawns.Length);
        int cyanCellIndex = Random.Range(0, cyanCellSpawns.Length);

        int[] positions = { redCellIndex, cyanCellIndex };

        redCell.transform.position = redCellSpawns[positions[0]].position;
        cyanCell.transform.position = redCellSpawns[positions[1]].position;

        photonView.RPC("SpawnCellsOverNetwork", RpcTarget.Others, positions);
    }

    [PunRPC]
    private void SpawnCellsOverNetwork(int[] positions)
    {
        redCell.transform.position = redCellSpawns[positions[0]].position;
        cyanCell.transform.position = redCellSpawns[positions[1]].position;
    }
}
