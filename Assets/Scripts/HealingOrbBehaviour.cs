using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrbBehaviour : MonoBehaviourPun
{
    private PlayerHealthController playerHealthController;

    public const byte HealCode = 1;

    private const int HEALING = 50;

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
        Debug.Log("Got a trigger with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            playerHealthController = other.gameObject.transform.parent.parent.gameObject.GetComponent<PlayerHealthController>();

            object[] content = new object[] { HEALING };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent(HealCode, content, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);

            Destroy(gameObject);
            photonView.RPC("DestroyOrbOverNetwork", RpcTarget.Others);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Got a collision with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealthController = collision.gameObject.transform.parent.parent.gameObject.GetComponent<PlayerHealthController>();

            object[] content = new object[] { HEALING };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent(HealCode, content, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);

            Destroy(gameObject);
            photonView.RPC("DestroyOrbOverNetwork", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void DestroyOrbOverNetwork()
    {
        Destroy(gameObject);
    }
}
