using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class AlleyGameStarter : MonoBehaviour
{
    [SerializeField] private PistolsSpawner pistolsSpawner;
    [SerializeField] private M4Spawner m4Spawner;
    
    [SerializeField] private GameObject p1BarrierSystem;
    [SerializeField] private GameObject p2BarrierSystem;

    [SerializeField] private GameObject p1StartingZone;
    [SerializeField] private GameObject p2StartingZone;

    [SerializeField] private GameObject textObject;

    //[SerializeField] private HealthBarManager healthBarManager;

    private bool isPlayer1Ready = false;
    private bool isPlayer2Ready = false;
    private bool hasMatchStarted = false;

    private PhotonView photonView;

    private TMP_Text readyText;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        readyText = textObject.GetComponent<TMP_Text>();
        readyText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMatchStarted)
        {
            return;
        }

        if (isPlayer1Ready && isPlayer2Ready)
        {
            StartCoroutine(StartMatchCoroutine());
            hasMatchStarted = true;
        }
    }

    IEnumerator StartMatchCoroutine()
    {
        readyText.text = "READY?";
        p1BarrierSystem.SetActive(true);
        p2BarrierSystem.SetActive(true);

        yield return new WaitForSeconds(3f);

        p1BarrierSystem.SetActive(false);
        p2BarrierSystem.SetActive(false); 
        readyText.text = "FIGHT!";

        pistolsSpawner.SpawnPistols();
        m4Spawner.SpawnM4();

        p1StartingZone.SetActive(false);
        p2StartingZone.SetActive(false);
        /*
        healthBarManager.gameObject.SetActive(true);
        healthBarManager.ActivateHealthBars();
        */
        yield return new WaitForSeconds(1f);
        
        readyText.text = "";
    }

    public void ChangeStatesForPlayer1()
    {
        photonView.RPC("UpdateStateOverNetworkP1", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateStateOverNetworkP1()
    {
        isPlayer1Ready = !isPlayer1Ready;
    }

    public void ChangeStatesForPlayer2()
    {
        photonView.RPC("UpdateStateOverNetworkP2", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateStateOverNetworkP2()
    {
        isPlayer2Ready = !isPlayer2Ready;
    }
}
