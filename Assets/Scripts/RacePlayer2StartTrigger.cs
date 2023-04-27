using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RacePlayer2StartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private RaceGameStarterBehaviour raceGameStarterBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceGameStarterBehaviour.ChangeStatesForPlayer2();
            Debug.Log("Player2 is ready");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        raceGameStarterBehaviour.ChangeStatesForPlayer2();
        Debug.Log("Player2 exited zone");
    }
}
