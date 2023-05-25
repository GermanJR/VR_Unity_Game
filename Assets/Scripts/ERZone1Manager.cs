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

    [SerializeField] private ERKeySocket eRKeySocket;

    [SerializeField] private Material lightOffMaterial;
    [SerializeField] private Material lightOnMaterial;

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
            light.transform.parent.gameObject.GetComponent<Renderer>().material = lightOffMaterial;
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
            eRKeySocket.ActivateSocket();
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
            Debug.Log("Restored a light");
            light.gameObject.SetActive(true);
            light.transform.parent.gameObject.GetComponent<Renderer>().material = lightOnMaterial;
        }
        photonView.RPC("RestorePowerOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void RestorePowerOverNetwork()
    {
        foreach (Light light in lights)
        {
            light.gameObject.SetActive(true);
            light.transform.parent.gameObject.GetComponent<Renderer>().material = lightOnMaterial;
        }
    }

    private void SpawnKey()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        Debug.Log("Spawning key");

        int keyIndex = Random.Range(0, keySpawns.Length);

        key.transform.position = keySpawns[keyIndex].position;

        key.SetActive(true);

        photonView.RPC("SpawnKeyOverNetwork", RpcTarget.Others, keySpawns[keyIndex].position);
    }

    [PunRPC]
    private void SpawnKeyOverNetwork(Vector3 spawnTransform)
    {
        key.transform.position = spawnTransform;
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

        cyanCellCorrect = true;
        photonView.RPC("ChangeForCyanCellOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void ChangeForCyanCellOverNetwork()
    {
        cyanCellCorrect = true;
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
