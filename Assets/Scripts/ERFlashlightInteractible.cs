using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ERFlashlightInteractible : XRGrabInteractable
{
    [SerializeField] private GameObject lightObject;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;

    private PhotonView photonViewFlashlight;

    private AudioSource flashlightSound;

    // Start is called before the first frame update
    void Start()
    {
        photonViewFlashlight = GetComponent<PhotonView>();
        flashlightSound = GetComponent<AudioSource>();

        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(TurnLight);
    }

    private void TurnLight(ActivateEventArgs arg0)
    {
        flashlightSound.Play();
        lightObject.SetActive(!lightObject.activeSelf);
        photonViewFlashlight.RPC("TurnLightOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void TurnLightOverNetwork()
    {
        lightObject.SetActive(!lightObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        photonViewFlashlight.RequestOwnership();

        if (args.interactorObject.transform.gameObject.CompareTag("Right Hand"))
        {
            rightHand.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (args.interactorObject.transform.gameObject.CompareTag("Left Hand"))
        {
            leftHand.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.CompareTag("Right Hand"))
        {
            rightHand.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (args.interactorObject.transform.gameObject.CompareTag("Left Hand"))
        {
            leftHand.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        base.OnSelectExited(args);
    }
}
