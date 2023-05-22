using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERZone1Manager : MonoBehaviourPun
{
    [SerializeField] private GameObject redCell;
    [SerializeField] private GameObject cyanCell;

    [SerializeField] private Transform[] redCellSpawns;
    [SerializeField] private Transform[] cyanCellSpawns;
    [SerializeField] private Transform[] keySpawns;

    private bool redCellCorrect = false;
    private bool cyanCellCorrect = false;

    private bool cellsSpawned = false;
    private bool keySpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (redCellCorrect && cyanCellCorrect)
        {
            SpawnKey();
            keySpawned = true;
        }
    }

    private void SpawnKey()
    {
        throw new System.NotImplementedException();
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

    private void SpawnCells()
    {
        if (!photonView.IsMine)
        {
            return;
        }

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
