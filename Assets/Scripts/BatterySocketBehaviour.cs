using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatterySocketBehaviour : XRSocketInteractor
{

    public GameObject lightSphere;
    public GameObject canvasError;
    public GameObject cell;
    public GameObject insideAntiTeleport;
    public Animator doorAnimator;
    public Animator closerAnimator;

    private DoorLightBehaviour doorLightBehaviour;
    private AudioSource openSound;


    private bool hasCellEntered = false;

    protected override void Start()
    {
        base.Start();
        openSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        doorLightBehaviour = lightSphere.GetComponent<DoorLightBehaviour>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("EnergyCell"))
        {
            openSound.Play();
            doorLightBehaviour.ChangeToGreenLight();
            doorAnimator.SetTrigger("Open");
            closerAnimator.SetTrigger("Close");
            StartCoroutine(DisableCellCoroutine());
            hasCellEntered = true;
            Destroy(insideAntiTeleport);
        }
        else
        {
            canvasError.SetActive(true);
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        canvasError.SetActive(false);

        if (!hasCellEntered)
        {
            doorLightBehaviour.ChangeToRedLight();
        }
        base.OnSelectExited(args);
    }

    private IEnumerator DisableCellCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        cell.SetActive(false);
    }
}
