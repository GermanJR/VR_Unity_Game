using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ERCountdownTimer : MonoBehaviourPun
{
    [SerializeField] private GameObject[] gameOverFloors;

    [SerializeField] private Transform[] gameOverSpawnPoints;

    [SerializeField] private int time;
    [SerializeField] private TMP_Text timeLeftText;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0 && !gameOver)
        {
            StopCoroutine(TimerCoroutine());
            TriggerGameOver();
        }
    }

    public void StartTimer()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        StartCoroutine(TimerCoroutine());
        photonView.RPC("StartTimerOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void StartTimerOverNetwork()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            time -= 1;
            UpdateTimeLeftInterface();
        }
    }

    private void UpdateTimeLeftInterface()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        timeLeftText.text = GetDisplayTime(time);
        photonView.RPC("UpdateTimeLeftInterfaceOverNetwork", RpcTarget.Others, timeLeftText.text);
    }

    [PunRPC]
    private void UpdateTimeLeftInterfaceOverNetwork(string updatedTime)
    {
        timeLeftText.text = updatedTime;
    }

    private String GetDisplayTime(int timeToDisplay)
    {
        if (timeToDisplay <= 0)
        {
            return "0";
        }

        int minutes = (int)Math.Truncate((float)timeToDisplay / 60);
        int seconds = timeToDisplay % 60;

        if (seconds < 10)
        {
            return "0" + minutes + ":0" + seconds;
        }
        else
        {
            return "0" + minutes + ":" + seconds;
        }
    }

    public void StopTimerForWin()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        StopCoroutine(TimerCoroutine());
        photonView.RPC("StopTimerForWinOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void StopTimerForWinOverNetwork()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        StopCoroutine(TimerCoroutine());
    }



    private void TriggerGameOver()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        foreach (GameObject floor in gameOverFloors)
        {
            floor.tag = "GameOver";
        }

        GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        int spawnIndex = 0;

        Debug.Log("Game Over, players found = " + currentPlayers.Length);

        foreach (GameObject player in currentPlayers)
        {
            player.transform.position = gameOverSpawnPoints[spawnIndex].position;
            spawnIndex++;
        }
        gameOver = true;
        photonView.RPC("TriggerGameOverOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void TriggerGameOverOverNetwork()
    {
        foreach (GameObject floor in gameOverFloors)
        {
            floor.tag = "GameOver";
        }

        GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        int spawnIndex = 0;
        foreach (GameObject player in currentPlayers)
        {
            player.transform.position = gameOverSpawnPoints[spawnIndex].position;
            spawnIndex++;
        }
        gameOver = true;
    }
}
