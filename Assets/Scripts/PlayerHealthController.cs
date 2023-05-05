using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerHealthController : MonoBehaviourPun
{
    [SerializeField] private HealthBarManager healthBarManager;

    private bool isPlayerDead = false;

    private float health = 300f;

    float Health
    {
        get => health;
        set => health = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        //healthBarManager = GameObject.Find("HealthBarManager").GetComponent<HealthBarManager>();
        Debug.Log("Health bar manager is: " + healthBarManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && photonView.IsMine && !isPlayerDead)
        {
            XROrigin origin = FindObjectOfType<XROrigin>();
            origin.GetComponent<CharacterController>().enabled = false;
            origin.transform.Find("Camera Offset/RightHand").gameObject.SetActive(false);
            origin.transform.Find("Camera Offset/LeftHand").gameObject.SetActive(false);
            Debug.Log("Player KO");
            isPlayerDead = true;
        }
    }

    public void RecibeDamage(float damage)
    {
        healthBarManager = GameObject.Find("HealthBarManager").GetComponent<HealthBarManager>();
        Debug.Log("(RecibeDamage) Health bar manager is: " + healthBarManager);
        Health -= damage;
        healthBarManager.UpdateHealthBars(Health);
        Debug.Log("Damage hit: " + damage);
    }
}
