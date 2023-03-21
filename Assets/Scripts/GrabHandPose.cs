using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrabHandPose : MonoBehaviour
{
    public float transitionDuration = 0.3f;

    public HandData rightHandPose;
    public HandData leftHandPose;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnsetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            if (handData.handType == HandData.HandModelType.Right)
            {
                SetHandDataValues(handData, rightHandPose);
            }
            else
            {
                SetHandDataValues(handData, leftHandPose);
            }

            StartCoroutine(SendHandDataRoutine(handData, finalHandPosition, finalHandRotation, finalFingerRotations, startingHandPosition, startingHandRotation, startingFingerRotations));
        }
    }

    public void UnsetPose(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            StartCoroutine(SendHandDataRoutine(handData, startingHandPosition, startingHandRotation, startingFingerRotations, finalHandPosition, finalHandRotation, finalFingerRotations));
        }
    }

    public void SetHandDataValues(HandData hand1, HandData hand2)
    {
        startingHandPosition = new Vector3(hand1.root.localPosition.x / hand1.root.localScale.x,
                                            hand1.root.localPosition.y / hand1.root.localScale.y,
                                            hand1.root.localPosition.z / hand1.root.localScale.z);
        finalHandPosition = new Vector3(hand2.root.localPosition.x / hand2.root.localScale.x,
                                            hand2.root.localPosition.y / hand2.root.localScale.y,
                                            hand2.root.localPosition.z / hand2.root.localScale.z);

        startingHandRotation = hand1.root.localRotation;
        finalHandRotation = hand2.root.localRotation;

        startingFingerRotations = new Quaternion[hand1.fingerBones.Length];
        finalFingerRotations = new Quaternion[hand1.fingerBones.Length];
    
        for (int i = 0; i < hand1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = hand1.fingerBones[i].localRotation;
            finalFingerRotations[i] = hand2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData handData, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        handData.root.localPosition = newPosition;
        handData.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            handData.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }

    public IEnumerator SendHandDataRoutine(HandData handData, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation,
                                            Vector3 startingPosition, Quaternion startingRotation, Quaternion[] startingBonesRotation)
    {
        float timer = 0;

        while(timer < transitionDuration)
        {
            Vector3 positionT = Vector3.Lerp(startingPosition, newPosition, timer / transitionDuration);
            Quaternion rotationT = Quaternion.Lerp(startingRotation, newRotation, timer / transitionDuration);

            handData.root.localPosition = positionT;
            handData.root.localRotation = rotationT;

            for (int i = 0; i < newBonesRotation.Length; i++)
            {
                handData.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i], timer / transitionDuration);
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }

#if UNITY_EDITOR
    [MenuItem("Tools/Mirror selected right pose")]
    public static void MirrorRightPose()
    {
        Debug.Log("Mirror right pose");

        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
    }
#endif

    public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
    {
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
        mirroredPosition.x *= -1;

        Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
        mirroredQuaternion.y *= -1;
        mirroredQuaternion.z *= -1;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.localRotation = mirroredQuaternion;

        for (int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
        }
    }
}
