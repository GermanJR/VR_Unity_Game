using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSecondGrabPointSimpleInteractible : XRSimpleInteractable
{
    public GameObject rightHand;
    public GameObject leftHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor args)
    {
        if (args.CompareTag("Right Hand"))
        {
            rightHand.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (args.CompareTag("Left Hand"))
        {
            leftHand.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        base.OnSelectEntered(args);
    }

    [System.Obsolete]
    protected override void OnSelectExited(XRBaseInteractor args)
    {
        if (args.CompareTag("Right Hand"))
        {
            rightHand.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (args.CompareTag("Left Hand"))
        {
            leftHand.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        base.OnSelectExited(args);
    }
}
