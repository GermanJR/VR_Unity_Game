using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ERBatterySocketCyan : XRSocketInteractor
{
    [SerializeField] private GameObject correctCyanCell;
    [SerializeField] private Color correctColor;
    [SerializeField] private ERZone1Manager eRZone1Manager;

    private PhotonView photonView;

    // Start is called before the first frame update
    protected override void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (args.interactableObject.transform.CompareTag("CyanCell"))
        {
            Debug.Log("CyanCell entered correctly");
            correctCyanCell.GetComponent<ERNetworkInteractible>().enabled = false;
            GetComponent<Renderer>().material.color = correctColor;
            photonView.RPC("CorrectBatteryOverNetwork", RpcTarget.Others);
            eRZone1Manager.ChangeForCyanCell();
        }
        base.OnSelectEntered(args);
    }

    [PunRPC]
    private void CorrectBatteryOverNetwork()
    {
        correctCyanCell.GetComponent<ERNetworkInteractible>().enabled = false;
        GetComponent<Renderer>().material.color = correctColor;
    }
}
