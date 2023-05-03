using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Microlight.MicroBar;

public class HealthBarManager : MonoBehaviour
{
    private const float MAX_PLAYER_HEALTH = 300f;

    [SerializeField] private MicroBar canvasPlayerBar;
    [SerializeField] private MicroBar networkPlayerBar;

    [SerializeField] private GameObject canvasHealthText;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateHealthBars()
    {
        photonView.RPC("ActivateHealthBarsOverNetwork", RpcTarget.All);
    }

    [PunRPC]
    private void ActivateHealthBarsOverNetwork()
    {
        canvasPlayerBar.gameObject.SetActive(true);
        networkPlayerBar.gameObject.SetActive(true);
        canvasHealthText.SetActive(true);

        canvasPlayerBar.Initialize(MAX_PLAYER_HEALTH);
        networkPlayerBar.Initialize(MAX_PLAYER_HEALTH);
    }

    public void UpdateHealthBars(float newValue)
    {
        photonView.RPC("UpdateHealthBarsOverNetwork", RpcTarget.All, newValue);
    }

    [PunRPC]
    private void UpdateHealthBarsOverNetwork(float newValue)
    {
        canvasPlayerBar.UpdateHealthBar(newValue);
        networkPlayerBar.UpdateHealthBar(newValue);
    }
}
