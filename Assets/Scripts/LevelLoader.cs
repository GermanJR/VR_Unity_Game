using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;

    [SerializeField] private GameObject networkManagerObject;

    private NetworkManager networkManager;

    private void Start()
    {
        try
        {
            networkManager = networkManagerObject.GetComponent<NetworkManager>();
        }
        catch (UnassignedReferenceException)
        {
            Debug.Log("Current scene doesn't need network reference.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewScene(string newSceneName)
    {
        StartCoroutine(LoadSceneCorroutine(newSceneName));
    }

    IEnumerator LoadSceneCorroutine(string sceneName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadNewOnlineScene(int roomIndex)
    {
        StartCoroutine(LoadOnlineSceneCorroutine(roomIndex));
    }

    IEnumerator LoadOnlineSceneCorroutine(int roomIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        networkManager.InitializeRoom(roomIndex);
    }

    public void LeaveRoomAndGoToLobby()
    {
        StartCoroutine(DisconnectAndReturnLobby());
    }

    IEnumerator DisconnectAndReturnLobby()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
