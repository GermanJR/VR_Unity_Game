using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ERKeySocket : XRSocketInteractor
{

    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject keyObject;
    [SerializeField] private GameObject sphereLock;
    [SerializeField] private GameObject capsuleLock;
    [SerializeField] private ERMusicManager musicManager;

    private PhotonView photonView;

    private AudioSource openSound;

    // Start is called before the first frame update
    protected override void Start()
    {
        photonView = GetComponent<PhotonView>();
        socketActive = false;
        openSound = GetComponent<AudioSource>();
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
            keyObject.SetActive(false);
            openSound.Play();
            Debug.Log("Playing open sound");
            musicManager.ReturnToWaitingMusic();
            sphereLock.SetActive(false);
            capsuleLock.SetActive(false);
            photonView.RPC("OpenDoorOverNetwork", RpcTarget.Others);
        }

        base.OnSelectEntered(args);
    }

    [PunRPC]
    private void OpenDoorOverNetwork()
    {
        keyObject.SetActive(false);
        doorAnimator.SetTrigger("Open");
        sphereLock.SetActive(false);
        capsuleLock.SetActive(false);
        openSound.Play();
        musicManager.ReturnToWaitingMusic();
    }
}
