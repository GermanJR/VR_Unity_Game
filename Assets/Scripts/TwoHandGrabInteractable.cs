using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using System;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    public XRSimpleInteractable secondHandGrabPoint;

    public enum TwoHandRotationType {None, First, Second}
    public TwoHandRotationType twoHandRotationType;

    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    private XRBaseInteractor secondInteractor;
    private PhotonView photonView;

    private Quaternion attachInitialRotation;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        secondHandGrabPoint = GetComponentInChildren<XRSimpleInteractable>();

        secondHandGrabPoint.onSelectEntered.AddListener(OnSecondHandGrab);
        secondHandGrabPoint.onSelectExited.AddListener(OnSecondHandRelease);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondInteractor && isSelected)
        {
            selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;

        switch (twoHandRotationType)
        {
            case TwoHandRotationType.None:
                targetRotation = selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
                break;
            case TwoHandRotationType.First:
                targetRotation = selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
                break;
            case TwoHandRotationType.Second:
                targetRotation = selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
                break;
            default:
                Debug.LogError("Error on hand type rotation.");
                throw new Exception("The hand rotation type is not valid.");
        }

        return targetRotation;
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        Debug.Log("Second hand entered.");
        secondInteractor = interactor;
    }
    
    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        Debug.Log("Second hand released.");
        secondInteractor = null;
    }

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor args)
    {
        Debug.Log("First grab entered.");
        //attachInitialRotation = transform.localRotation;
        //attachInitialRotation = args.interactorObject.transform.localRotation;
        attachInitialRotation = args.attachTransform.localRotation;
        photonView.RequestOwnership();
        base.OnSelectEntered(args);
    }
    
    [System.Obsolete]
    protected override void OnSelectExited(XRBaseInteractor args)
    {
        Debug.Log("First grab exited.");
        base.OnSelectExited(args);
        secondInteractor = null;
        //transform.localRotation = attachInitialRotation;
        //args.interactorObject.transform.localRotation = attachInitialRotation;
        args.attachTransform.localRotation = attachInitialRotation;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
