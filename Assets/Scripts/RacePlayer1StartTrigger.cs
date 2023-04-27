using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer1StartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private RaceGameStarterBehaviour raceGameStarterBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceGameStarterBehaviour.ChangeStatesForPlayer1();
            Debug.Log("Player1 is ready");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceGameStarterBehaviour.ChangeStatesForPlayer1();
            Debug.Log("Player1 exited zone");
        }
    }
}
