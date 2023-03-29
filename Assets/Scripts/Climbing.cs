using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climbing : MonoBehaviour
{
    public CharacterController character;
    public static ActionBasedController climbingHand;
    private ContinuousMoveProviderBase continuousMovement;

    private ActionBasedController previousHand;
    private Vector3 previousPos;
    private Vector3 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        continuousMovement = GetComponent<ContinuousMoveProviderBase>();
    }

    void Update()
    {

        if (climbingHand)
        {
            if (previousHand == null)
            {
                previousHand = climbingHand;
                previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
            }
            if (climbingHand.name != previousHand.name)
            {
                previousHand = climbingHand;
                previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
                //Debug.Log("DIFFERENT HAND NOW");
            }
            continuousMovement.enabled = false;
            Climb();
        }
        else
        {
            continuousMovement.enabled = true;
        }
    }

    void Climb()
    {
        currentVelocity = (climbingHand.positionAction.action.ReadValue<Vector3>() - previousPos) / Time.deltaTime;
        character.Move(transform.rotation * -currentVelocity * Time.deltaTime);

        previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
    }
}
