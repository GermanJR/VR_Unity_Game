using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer1StartTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceGameStarterBehaviour.isPlayer1Ready = true;
            Debug.Log("Player1 is ready");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceGameStarterBehaviour.isPlayer1Ready = false;
            Debug.Log("Player1 exited zone");
        }
    }
}
