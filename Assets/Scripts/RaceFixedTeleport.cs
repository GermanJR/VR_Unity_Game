using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFixedTeleport : MonoBehaviour
{

    [SerializeField] private Transform teleportationPoint;

    private Vector3 teleportationPosition;

    // Start is called before the first frame update
    void Start()
    {
        teleportationPosition = teleportationPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = teleportationPosition;
        }
    }
}
