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
    [SerializeField] private GameObject currentJoinedTextObject;
    [SerializeField] private GameObject startGameTextObject;
    [SerializeField] private GameObject canvasObject;

    //[SerializeField] private ERZone1Spawner eRZone1Spawner;

    [SerializeField] private Animator doorAnimator;

    [SerializeField] private ERZone1Manager eRZone1Manager;
    [SerializeField] private ERCountdownTimer eRCountdownTimer;


    private bool isRoomClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            return;
        }

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
        Debug.Log("Closing room, 4 players joined or game started.");
    }

    public void Play()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        StartCoroutine(StartPlayingCoroutine());
    }

    IEnumerator StartPlayingCoroutine()
    {
        CloseRoom();
        DeactivateAllTextAndEnableStarting();
        photonView.RPC("DeactivateAllTextAndEnableStartingOverNetwork", RpcTarget.Others);
        yield return new WaitForSeconds(3f);
        OpenDoor();
        canvasObject.SetActive(false);
        photonView.RPC("OpenDoorOverNetwork", RpcTarget.Others);
        eRZone1Manager.SpawnCells();
        eRCountdownTimer.StartTimer();
    }

    [PunRPC]
    private void DeactivateAllTextAndEnableStartingOverNetwork()
    {
        allJoinedTextObject.SetActive(false);
        startNowTextObject.SetActive(false);
        currentJoinedTextObject.SetActive(false);
        startGameTextObject.SetActive(true);
        numberOfPlayersText.gameObject.SetActive(false);
    }

    [PunRPC]
    private void OpenDoorOverNetwork()
    {
        doorAnimator.SetTrigger("Open");
        canvasObject.SetActive(false);
    } 
    
    private void DeactivateAllTextAndEnableStarting()
    {
        allJoinedTextObject.SetActive(false);
        startNowTextObject.SetActive(false);
        currentJoinedTextObject.SetActive(false);
        startGameTextObject.SetActive(true);
        numberOfPlayersText.gameObject.SetActive(false);
    }

    private void OpenDoor()
    {
        doorAnimator.SetTrigger("Open");
    }
}
