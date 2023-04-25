using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RaceStartingBarrierBehaviour : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            return;
        } 
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Destroy(gameObject);
        }
    }
}
