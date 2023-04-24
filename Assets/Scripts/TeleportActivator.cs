using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportActivator : MonoBehaviour
{

    public GameObject teleportator;

    public InputActionProperty activator;
    public InputActionProperty cancelator;

    public XRRayInteractor ray;

    [SerializeField] private XRDirectInteractor rightDirectInteractor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Not working
        //bool isRayHovering = ray.TryGetHitInfo(out Vector3 position, out Vector3 normalPosition, out int number, out bool valid);
        //teleportator.SetActive((!isRayHovering) && (cancelator.action.ReadValue<float>() == 0f && activator.action.ReadValue<float>() > 0.1f));    
        //teleportator.SetActive(cancelator.action.ReadValue<float>() == 0f && activator.action.ReadValue<float>() > 0.1f);    
        //Debug.Log("RayHover = "  + isRayHovering + " | " + (activator.action.ReadValue<float>() > 0.1f));
        //teleportator.SetActive(activator.action.ReadValue<float>() > 0.1f);



        //Partial solution -> Working for objects in hand, but not with UI interaction.
        teleportator.SetActive(rightDirectInteractor.interactablesSelected.Count == 0 && activator.action.ReadValue<float>() > 0.1f);

    }
}
