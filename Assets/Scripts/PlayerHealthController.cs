using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class PlayerHealthController : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private HealthBarManager healthBarManager;
    //[SerializeField] private TMP_Text remainingHealthText;
    [SerializeField] private TMP_Text endMatchTextObject;
    [SerializeField] private TMP_Text HPText;

    [SerializeField] private GameObject deadFlag;
    [SerializeField] private GameObject HPTextObject;

    [SerializeField] private GameObject winTextObject;
    [SerializeField] private GameObject loseTextObject;

    private bool isPlayerDead = false;
    private bool didPlayerWon = false;
    /*
    private bool gotYellow = false;
    private bool gotOrange = false;
    private bool gotRed = false;
    */
    private float health = 300f;

    //private bool gotHealthManager = false;
    float Health
    {
        get => health;
        set => health = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (!photonView.IsMine)
        {
            remainingHealthText.gameObject.SetActive(false);
        }
        remainingHealthText.text = "300/300";
        */
        //healthBarManager = GameObject.Find("HealthBarManager").GetComponent<HealthBarManager>();
        //Debug.Log("Health bar manager is: " + healthBarManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && !isPlayerDead)
        {
            XROrigin origin = FindObjectOfType<XROrigin>();
            origin.GetComponent<CharacterController>().enabled = false;
            origin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            origin.transform.Find("Camera Offset/RightHand").gameObject.SetActive(false);
            origin.transform.Find("Camera Offset/LeftHand").gameObject.SetActive(false);
            deadFlag.SetActive(true);
            photonView.RPC("ActivateDeadFlagOverNetwork", RpcTarget.Others);
            healthBarManager.UnableDeadPlayerBar();

            MakeRivalWin();
            StartCoroutine(LoseEndMatchCoroutine());

            Debug.Log("Player KO");
            isPlayerDead = true;
        }
        else if (didPlayerWon)
        {
            healthBarManager.UnableWinnerBar();
            StartCoroutine(WinEndMatchCoroutine());
        }
    }

    private void MakeRivalWin()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("MakeRivalWinOverNetwork", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void MakeRivalWinOverNetwork()
    {
        didPlayerWon = true;
    }

    [PunRPC]
    private void ActivateDeadFlagOverNetwork()
    {
        deadFlag.SetActive(true);
    }

    public void RecibeDamage(float damage)
    {
        if (!HPTextObject.activeSelf)
        {
            HPTextObject.SetActive(true);
        }

        //remainingHealthText.text = "";
        //Debug.Log(transform.gameObject.name);
        Debug.Assert(healthBarManager != null);
        
        if (healthBarManager == null && photonView.IsMine)
        {
            Debug.LogWarning("HealthBarManager was null.");
            healthBarManager = GameObject.Find("AlleyNetworkPlayer(Clone)/HealthBarManager").GetComponent<HealthBarManager>();
        }
        
        //Debug.Log("(RecibeDamage) Health bar manager is: " + healthBarManager);
        Health -= damage;
        /*
        if (!isPlayerDead)
        {
            remainingHealthText.text = Health + "/300";
            UpdateColorAccordingHealth(false);
        }
        else
        {
            remainingHealthText.text = "0/300";
            UpdateColorAccordingHealth(true);
        }
        */
        healthBarManager.UpdateHealthBars(Health);
        Debug.Log("Damage hit: " + damage + " Current HP: " + Health);
    }

    /*
    private void UpdateColorAccordingHealth(bool dead)
    {
        if (dead)
        {
            remainingHealthText.color = new Color(0, 0, 0);
            Debug.Log("Changed to black");
            return;
        }

        if (Health <= 150 && !gotYellow)
        {
            remainingHealthText.color = new Color(207,212,0);
            Debug.Log("Changed to yellow");
            gotYellow = true;
        }

        if (Health <= 100 && !gotOrange)
        {
            remainingHealthText.color = new Color(255, 144, 0);
            Debug.Log("Changed to orange");
            gotOrange = true;
        }
        
        if (Health <= 50 && !gotRed)
        {
            remainingHealthText.color = new Color(219,0,0);
            Debug.Log("Changed to red");
            gotRed = true;
        }
    }
    */

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 0:
                if (!photonView.IsMine)
                {
                    return;
                }
                object[] data = (object[])photonEvent.CustomData;
                var damage = data[0];
                //Debug.Log("Correct event recieved, damage sent -> " + float.Parse(damage.ToString()));
                RecibeDamage(float.Parse(damage.ToString()));
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    IEnumerator LoseEndMatchCoroutine()
    {
        AlleyMusicManager musicManager = FindAnyObjectByType<AlleyMusicManager>();
        musicManager.FadeCombatMusic();
        
        HPText.text = "";
        HPTextObject.SetActive(false);
        loseTextObject.SetActive(true);
        endMatchTextObject.gameObject.SetActive(true);
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 4 SECONDS.";        
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 3 SECONDS.";        
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 2 SECONDS.";        
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 1 SECOND."; 
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 0 SECONDS.";
        LevelLoader levelLoader = FindAnyObjectByType<LevelLoader>();
        levelLoader.LeaveRoomAndGoToLobby();
    }    
    
    IEnumerator WinEndMatchCoroutine()
    {
        AlleyMusicManager musicManager = FindAnyObjectByType<AlleyMusicManager>();
        musicManager.FadeCombatMusic();

        HPText.text = "";
        HPTextObject.SetActive(false);
        winTextObject.SetActive(true);
        endMatchTextObject.gameObject.SetActive(true);
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 4 SECONDS.";        
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 3 SECONDS.";        
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 2 SECONDS.";        
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 1 SECOND."; 
        
        yield return waitForSeconds;
        endMatchTextObject.text = "END OF THE MATCH\nRETURNING TO LOBBY\nIN 0 SECONDS.";
        LevelLoader levelLoader = FindAnyObjectByType<LevelLoader>();
        levelLoader.LeaveRoomAndGoToLobby();
    }
}
