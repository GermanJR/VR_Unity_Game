using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviourPun
{
    public Transform head;
    public Transform rightHand;
    public Transform leftHand;

    public Animator rightHandAnimator;
    public Animator leftHandAnimator;
    
    private Transform headOrigin;
    private Transform rightHandOrigin;
    private Transform leftHandOrigin;

    //private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        XROrigin origin = FindObjectOfType<XROrigin>();

        headOrigin = origin.transform.Find("Camera Offset/Main Camera");
        rightHandOrigin = origin.transform.Find("Camera Offset/RightHand");
        leftHandOrigin = origin.transform.Find("Camera Offset/LeftHand");
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            head.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);

            MapPosition(head, headOrigin);
            MapPosition(rightHand, rightHandOrigin);
            MapPosition(leftHand, leftHandOrigin);
        }
    }

    void MapPosition(Transform target, Transform originTransform)
    {
        target.position = originTransform.position;
        target.rotation = originTransform.rotation;
    }
}
