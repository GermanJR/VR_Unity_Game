using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBreakPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BreakPlatform"))
        {
            Destroy(other.gameObject);
            Debug.Log("Player landed on fake platform");
        }
    }
}
