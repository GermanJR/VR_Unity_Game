using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class RacePlayer2WinTrigger : MonoBehaviour
{

    [SerializeField] private GameObject player2WinCanvas;
    [SerializeField] private GameObject player1TriggerToDestroy;

    [SerializeField] private Animator player2CanvasAnimator;

    [SerializeField] private List<GameObject> player2ConfettiSystem;

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
            photonView.RPC("SendPlayer2VictoryOverNetwork", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SendPlayer2VictoryOverNetwork()
    {
        Destroy(player1TriggerToDestroy);
        player2WinCanvas.SetActive(true);
        player2CanvasAnimator.SetTrigger("Enter");
        photonView.RPC("ActivateConfettiOverTheNetwork", RpcTarget.All);
        playVictoryToneEvent.Invoke();
        hasPlayerWon = true;
        Debug.Log("Player2 wins!");
    }

    [PunRPC]
    private void ActivateConfettiOverTheNetwork()
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
