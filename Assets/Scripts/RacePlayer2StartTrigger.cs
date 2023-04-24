using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer2StartTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceGameStarterBehaviour.isPlayer2Ready = true;
            Debug.Log("Player2 is ready");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RaceGameStarterBehaviour.isPlayer2Ready = false;
        Debug.Log("Player2 exited zone");
    }
}
