using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer1StartTrigger : MonoBehaviour
{
    /*
    [SerializeField] private List<GameObject> player1NotReadyTexts;
    [SerializeField] private List<GameObject> player1ReadyTexts;
    */
    [SerializeField] private GameObject canvas;

    [SerializeField] private RaceGameStarterBehaviour raceGameStarterBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceGameStarterBehaviour.isPlayer1Ready = true;
            raceGameStarterBehaviour.ChangeStatesForPlayer1();
            //SwitchTexts();
            Debug.Log("Player1 is ready");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceGameStarterBehaviour.isPlayer1Ready = false;
            raceGameStarterBehaviour.ChangeStatesForPlayer1();
            //SwitchTexts();
            Debug.Log("Player1 exited zone");
        }
    }

    /*
    private void SwitchTexts()
    {
        if(player1NotReadyTexts.Count != player1ReadyTexts.Count)
        {
            Debug.LogError("Text lists have different capacities");
            return;
        }

        canvas.GetComponent<PhotonView>().RPC("UpdateStateOverNetwork", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateStateOverNetwork()
    {
        for (int i = 0; i < player1NotReadyTexts.Count; i++)
        {
            player1NotReadyTexts[i].SetActive(!player1NotReadyTexts[i].activeSelf);
            player1ReadyTexts[i].SetActive(!player1ReadyTexts[i].activeSelf);
        }
    }
    */
}
