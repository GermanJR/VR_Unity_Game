using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer2WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject player2WinCanvas;
    [SerializeField] private GameObject player1TriggerToDestroy;

    [SerializeField] private Animator player2CanvasAnimator;

    [SerializeField] private List<GameObject> player2ConfettiSystem;


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
            player2CanvasAnimator.SetTrigger("Enter");
            ActivateConfetti();
            Debug.Log("Player2 wins!");
        }
    }

    private void ActivateConfetti()
    {
        if (player2ConfettiSystem == null || player2ConfettiSystem.Count == 0)
        {
            Debug.LogWarning("List is null. No confetti objects were found for player 1.");
            return;
        }
        else
        {
            foreach (GameObject confetti in player2ConfettiSystem)
            {
                confetti.SetActive(true);
            }
        }
    }
}
