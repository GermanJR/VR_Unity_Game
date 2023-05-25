using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ERKeySocket : XRSocketInteractor
{

    [SerializeField] private Animator doorAnimator;

    private PhotonView photonView;

    // Start is called before the first frame update
    protected override void Start()
    {
        photonView = GetComponent<PhotonView>();
        socketActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSocket()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        socketActive = true;
        photonView.RPC("ActivateSocketOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void ActivateSocketOverNetwork()
    {
        socketActive = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (args.interactableObject.transform.CompareTag("Key"))
        {
            doorAnimator.SetTrigger("Open");
            GameObject key = args.interactableObject.transform.gameObject;
            key.SetActive(false);
            photonView.RPC("OpenDoorOverNetwork", RpcTarget.Others, key);
        }

        base.OnSelectEntered(args);
    }

    [PunRPC]
    private void OpenDoorOverNetwork(GameObject key)
    {
        key.SetActive(false);
        doorAnimator.SetTrigger("Open");
    }
}
