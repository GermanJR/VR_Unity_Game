using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RacePlayer1WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject player1WinCanvas;
    [SerializeField] private GameObject player2TriggerToDestroy;

    [SerializeField] private Animator player1CanvasAnimator;

    [SerializeField] private List<GameObject> player1ConffetiSystem;
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
            player1CanvasAnimator.SetTrigger("Enter");
            ActivateConfetti();
            Debug.Log("Player1 wins!");
        }
    }

    private void ActivateConfetti()
    {
        if (player1ConffetiSystem == null || player1ConffetiSystem.Count == 0)
        {
            Debug.LogWarning("List is null. No confetti objects were found for player 1.");
            return;
        }
        else
        {
            foreach (GameObject confetti in player1ConffetiSystem)
            {
                confetti.SetActive(true);
            }
        }
    }
}
