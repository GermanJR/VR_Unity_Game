using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            return;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Destroy(gameObject);
        }
    }
}
