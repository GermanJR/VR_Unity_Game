using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer2WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject player2WinCanvas;
    [SerializeField] private GameObject player1TriggerToDestroy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(player1TriggerToDestroy);
            player2WinCanvas.SetActive(true);
            Debug.Log("Player2 wins!");
        }
    }
}
