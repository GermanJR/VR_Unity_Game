using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerHealthController : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private HealthBarManager healthBarManager;

    private bool isPlayerDead = false;

    private float health = 300f;

    //private bool gotHealthManager = false;
    float Health
    {
        get => health;
        set => health = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //healthBarManager = GameObject.Find("HealthBarManager").GetComponent<HealthBarManager>();
        //Debug.Log("Health bar manager is: " + healthBarManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && !isPlayerDead)
        {
            XROrigin origin = FindObjectOfType<XROrigin>();
            origin.GetComponent<CharacterController>().enabled = false;
            origin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            origin.transform.Find("Camera Offset/RightHand").gameObject.SetActive(false);
            origin.transform.Find("Camera Offset/LeftHand").gameObject.SetActive(false);
            Debug.Log("Player KO");
            isPlayerDead = true;
        }
    }

    public void RecibeDamage(float damage)
    {
        Debug.Log(transform.gameObject.name);
        Debug.Assert(healthBarManager != null);
        
        if (healthBarManager == null && photonView.IsMine)
        {
            Debug.LogWarning("HealthBarManager was null.");
            healthBarManager = GameObject.Find("AlleyNetworkPlayer(Clone)/HealthBarManager").GetComponent<HealthBarManager>();
        }
        
        Debug.Log("(RecibeDamage) Health bar manager is: " + healthBarManager);
        Health -= damage;
        healthBarManager.UpdateHealthBars(Health);
        Debug.Log("Damage hit: " + damage + " Current HP: " + Health);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 0)
        {
            Debug.Log("Event recieved with code: " + photonEvent.Code);

        }

        switch (photonEvent.Code)
        {
            case 0:
                if (!photonView.IsMine)
                {
                    return;
                }
                object[] data = (object[])photonEvent.CustomData;
                var damage = data[0];
                Debug.Log("Correct event recieved, damage sent -> " + float.Parse(damage.ToString()));
                RecibeDamage(float.Parse(damage.ToString()));
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
