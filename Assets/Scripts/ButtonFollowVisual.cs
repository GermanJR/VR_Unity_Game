/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5f;
    public float followAngleThreshold = 45f;
    
    private Vector3 initialLocalPosition;
    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    private bool freezed = false;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)args.interactorObject;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if (pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                freezed = false;
            }
        }
    }
    public void Reset(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freezed = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRPokeInteractor)
        {
            freezed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPosition, Time.deltaTime * resetSpeed);
        }
    }
}
*/
