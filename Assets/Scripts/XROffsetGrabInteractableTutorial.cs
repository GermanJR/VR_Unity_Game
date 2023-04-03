using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Use this script for tutorial, not sending network data.

public class XROffsetGrabInteractableTutorial : XRGrabInteractable
{
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;


    // Start is called before the first frame update
    void Start()
    {
        if(!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offet Grab Pivot");
            attachPoint.transform.SetParent(transform, false);
            attachTransform = attachPoint.transform;
        }
        else
        {
            initialLocalPosition = attachTransform.localPosition;
            initialLocalRotation = attachTransform.localRotation;
        }
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialLocalPosition;
            attachTransform.localRotation = initialLocalRotation;
        }

        base.OnSelectEntered(args);
    }
}
