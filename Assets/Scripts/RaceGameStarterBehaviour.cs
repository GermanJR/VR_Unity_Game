using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RaceGameStarterBehaviour : MonoBehaviour
{
    private bool isPlayer1Ready = false;
    private bool isPlayer2Ready = false;

    [SerializeField] private GameObject playerStartSeparator;
    [SerializeField] private GameObject textObject;

    [SerializeField] private Animator startBarrierAnimator;

    [SerializeField] private List<GameObject> player1NotReadyTexts;
    [SerializeField] private List<GameObject> player1ReadyTexts;

    [SerializeField] private List<GameObject> player2NotReadyTexts;
    [SerializeField] private List<GameObject> player2ReadyTexts;

    private TMP_Text textPlayerStart;

    private PhotonView photonView;

    private AudioSource startRaceAudio;

    private bool hasRaceStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        textPlayerStart = textObject.GetComponent<TMP_Text>();
        textPlayerStart.text = "";

        photonView = GetComponent<PhotonView>();
        startRaceAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasRaceStarted)
        {
            return;
        }

        if (isPlayer1Ready && isPlayer2Ready)
        {
            startRaceAudio.Play();
            StartCoroutine(StartRaceCoroutine());
            hasRaceStarted = true;
        }
    }

    IEnumerator StartRaceCoroutine()
    {
        playerStartSeparator.SetActive(true);
        textPlayerStart.text = "3";
        yield return new WaitForSeconds(1f);
        textPlayerStart.text = "2";
        yield return new WaitForSeconds(1f);
        textPlayerStart.text = "1";
        yield return new WaitForSeconds(1f);
        textPlayerStart.text = "GO!";
        startBarrierAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        textPlayerStart.text = "";
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void ChangeStatesForPlayer1()
    {
        if (player1NotReadyTexts.Count != player1ReadyTexts.Count)
        {
            Debug.LogError("Text lists have different capacities");
            return;
        }

        photonView.RPC("UpdateStateOverNetworkP1", RpcTarget.All);
    }
  
    public void ChangeStatesForPlayer2()
    {
        if (player2NotReadyTexts.Count != player2ReadyTexts.Count)
        {
            Debug.LogError("Text lists have different capacities");
            return;
        }

        photonView.RPC("UpdateStateOverNetworkP2", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateStateOverNetworkP1()
    {
        for (int i = 0; i < player1NotReadyTexts.Count; i++)
        {
            player1NotReadyTexts[i].SetActive(!player1NotReadyTexts[i].activeSelf);
            player1ReadyTexts[i].SetActive(!player1ReadyTexts[i].activeSelf);
        }

        isPlayer1Ready = !isPlayer1Ready;
    }

    [PunRPC]
    private void UpdateStateOverNetworkP2()
    {
        for (int i = 0; i < player2NotReadyTexts.Count; i++)
        {
            player2NotReadyTexts[i].SetActive(!player2NotReadyTexts[i].activeSelf);
            player2ReadyTexts[i].SetActive(!player2ReadyTexts[i].activeSelf);
        }

        isPlayer2Ready = !isPlayer2Ready;
    }
}
