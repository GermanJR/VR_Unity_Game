using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class RacePlayer1WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject player1WinCanvas;
    [SerializeField] private GameObject player2TriggerToDestroy;

    [SerializeField] private Animator player1CanvasAnimator;

    [SerializeField] private List<GameObject> player1ConffetiSystem;

    [SerializeField] private UnityEvent playVictoryToneEvent;

    private PhotonView photonView;

    private bool hasPlayerWon = false;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayerWon)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            photonView.RPC("SendPlayer1VictoryOverNetwork", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SendPlayer1VictoryOverNetwork()
    {
        Destroy(player2TriggerToDestroy);
        player1WinCanvas.SetActive(true);
        player1CanvasAnimator.SetTrigger("Enter");
        photonView.RPC("ActivateConfettiOverTheNetwork", RpcTarget.All);
        playVictoryToneEvent.Invoke();
        hasPlayerWon = true;
        Debug.Log("Player1 wins!");
    }

    [PunRPC]
    private void ActivateConfettiOverTheNetwork()
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
