using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpointManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> checkpoints;
    [SerializeField] private Vector3 vectorPoint;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            vectorPoint = player.transform.position;
            Debug.Log("New checkpoint reached.");
        }
        else if (other.CompareTag("FallZone"))
        {
            player.transform.position = vectorPoint;
            Debug.Log("Respawning on checkpoint");
        }
    }
}
