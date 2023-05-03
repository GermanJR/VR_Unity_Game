using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AlleyNetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject alleyPlayerSpawned;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        alleyPlayerSpawned = PhotonNetwork.Instantiate("AlleyNetworkPlayer", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(alleyPlayerSpawned);
    }
}
