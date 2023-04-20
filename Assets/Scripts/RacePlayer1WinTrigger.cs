using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RacePlayer1WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject player1WinCanvas;
    [SerializeField] private GameObject player2TriggerToDestroy;

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
            Destroy(player2TriggerToDestroy);
            player1WinCanvas.SetActive(true);
            Debug.Log("Player1 wins!");
        }
    }
}
