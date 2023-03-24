using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class LightSaberBehaviour : XRGrabInteractable
{

    private GameObject laser;
    private Vector3 fullSize;

    private bool grabbed = false;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        laser = transform.Find("SingleLine-LightSaber").gameObject;
        fullSize = laser.transform.localScale;
        laser.transform.localScale = new Vector3(fullSize.x, 0, fullSize.z);
    }

    // Update is called once per frame
    void Update()
    {


        LaserLogicController();
    }

    private void LaserLogicController()
    {
        if (grabbed && laser.transform.localScale.y < fullSize.y)
        {
            laser.SetActive(true);
            laser.transform.localScale += new Vector3(0, 0.0001f, 0);
        }
        else if (!grabbed && laser.transform.localScale.y > 0)
        {
            laser.transform.localScale += new Vector3(0, -0.0001f, 0);
        }
        else if (!grabbed)
        {
            laser.SetActive(false);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        photonView.RequestOwnership();
        grabbed = true;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        grabbed = false;
        base.OnSelectExited(args);
    }
}
