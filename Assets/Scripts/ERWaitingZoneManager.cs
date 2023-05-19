using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ERWaitingZoneManager : MonoBehaviourPun
{

    [SerializeField] private TMP_Text numberOfPlayersText;
    
    [SerializeField] private GameObject startNowTextObject;
    [SerializeField] private GameObject allJoinedTextObject;


    private bool isRoomClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numberOfPlayersText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            startNowTextObject.SetActive(false);
            allJoinedTextObject.SetActive(true);
            CloseRoom();
            isRoomClosed = true;
        }
        else if (isRoomClosed && !(PhotonNetwork.CurrentRoom.PlayerCount == 4))
        {
            startNowTextObject.SetActive(true);
            allJoinedTextObject.SetActive(false);
            ReopenRoom();
            isRoomClosed = false;
        }
    }

    private void ReopenRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
        Debug.Log("Opening room again.");
    }

    private void CloseRoom()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        Debug.Log("Closing room, 4 players joined.");
    }
}
