using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Microlight.MicroBar;
using Unity.XR.CoreUtils;

public class HealthBarManager : MonoBehaviourPun
{
    private const float MAX_PLAYER_HEALTH = 300f;

    private MicroBar canvasPlayerBar;
    [SerializeField] private MicroBar networkPlayerBar;

    private GameObject canvasHealthText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateHealthBars()
    {
        Debug.Log("ActivateHealthBars was called.");
        if (true)
        {
            Debug.Log("Inside photonView.isMine");
            XROrigin origin = FindObjectOfType<XROrigin>();

            Transform canvasTransform = origin.gameObject.transform.Find("CanvasPlayer");
            GameObject canvasObject = canvasTransform.gameObject;
            Debug.Log(canvasObject.name);
            GameObject barObject = canvasObject.transform.Find("SpriteRendererMicroBar").gameObject;
            GameObject textObject = canvasObject.transform.GetChild(1).gameObject;

            Debug.Assert(0 == 0);
            Debug.Assert(origin != null);
            Debug.Assert(canvasTransform != null);
            Debug.Assert(canvasObject != null);
            Debug.Assert(barObject != null);
            Debug.Assert(textObject != null);

            Component[] barObjectComponents = barObject.GetComponents<Component>();

            foreach (Component component in barObjectComponents)
            {
                Debug.Log("Got component: " + component.name);
            }

            canvasPlayerBar = barObject.GetComponent<MicroBar>();
            canvasHealthText = textObject;

            Debug.Assert(canvasPlayerBar != null);
            Debug.Assert(canvasHealthText != null);

            /*
            canvasPlayerBar = GameObject.Find("CanvasPlayer/SpriteRendererMicroBar").GetComponent<MicroBar>();
            canvasHealthText = GameObject.Find("CanvasPlayer/HealthText");
            */
            canvasPlayerBar.gameObject.SetActive(true);
            canvasHealthText.SetActive(true);
            canvasPlayerBar.Initialize(MAX_PLAYER_HEALTH);

            networkPlayerBar.gameObject.SetActive(true);
            networkPlayerBar.Initialize(MAX_PLAYER_HEALTH);
        }

        photonView.RPC("ActivateHealthBarsOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void ActivateHealthBarsOverNetwork()
    {
        networkPlayerBar.gameObject.SetActive(true);
        networkPlayerBar.Initialize(MAX_PLAYER_HEALTH);
    }

    public void UpdateHealthBars(float newValue)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        //canvasPlayerBar.UpdateHealthBar(newValue);
        networkPlayerBar.UpdateHealthBar(newValue);
        photonView.RPC("UpdateHealthBarsOverNetwork", RpcTarget.Others, newValue);
    }

    [PunRPC]
    private void UpdateHealthBarsOverNetwork(float newValue)
    {
        networkPlayerBar.UpdateHealthBar(newValue);
    }
}