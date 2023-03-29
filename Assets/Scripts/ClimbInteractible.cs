using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractible : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        if (args.interactorObject is XRDirectInteractor)
        {
            Climbing.climbingHand = args.interactorObject.transform.GetComponent<ActionBasedController>();
        }

        base.OnSelectEntered(args);
    }


    protected override void OnSelectExited(SelectExitEventArgs args)
    {

        if (Climbing.climbingHand && Climbing.climbingHand.name == args.interactorObject.transform.name)
        {
            Climbing.climbingHand = null;
        }
        base.OnSelectExited(args);
    }
}
