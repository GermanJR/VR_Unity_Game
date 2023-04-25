using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RacePlayer2StartTrigger : MonoBehaviour
{
    /*
    [SerializeField] private List<GameObject> player2NotReadyTexts;
    [SerializeField] private List<GameObject> player2ReadyTexts;
    */
    [SerializeField] private GameObject canvas;

    [SerializeField] private RaceGameStarterBehaviour raceGameStarterBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceGameStarterBehaviour.isPlayer2Ready = true;
            raceGameStarterBehaviour.ChangeStatesForPlayer2();
            //SwitchTexts();
            Debug.Log("Player2 is ready");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RaceGameStarterBehaviour.isPlayer2Ready = false;
        raceGameStarterBehaviour.ChangeStatesForPlayer2();
        //SwitchTexts();
        Debug.Log("Player2 exited zone");
    }

    /*
    private void SwitchTexts()
    {
        if (player2NotReadyTexts.Count != player2ReadyTexts.Count)
        {
            Debug.LogError("Text lists have different capacities");
            return;
        }

        canvas.GetComponent<PhotonView>().RPC("UpdateStateOverNetwork", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateStateOverNetwork()
    {
        for (int i = 0; i < player2NotReadyTexts.Count; i++)
        {
            player2NotReadyTexts[i].SetActive(!player2NotReadyTexts[i].activeSelf);
            player2ReadyTexts[i].SetActive(!player2ReadyTexts[i].activeSelf);
        }
    }
    */
}
