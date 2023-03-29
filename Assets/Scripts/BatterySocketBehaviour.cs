using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatterySocketBehaviour : XRSocketInteractor
{

    public GameObject lightSphere;

    private DoorLightBehaviour doorLightBehaviour;

    // Update is called once per frame
    void Update()
    {
        doorLightBehaviour = lightSphere.GetComponent<DoorLightBehaviour>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("EnergyCell"))
        {
            doorLightBehaviour.ChangeToGreenLight();
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        doorLightBehaviour.ChangeToRedLight();
        base.OnSelectExited(args);
    }
}
