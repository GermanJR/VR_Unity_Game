using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyBulletBehaviour : MonoBehaviourPun
{
    public const byte DealDamageCode = 0;

    private const int PISTOL_BULLET_DAMAGE = 8;
    private const int M4_BULLET_DAMAGE = 5;

    private PlayerHealthController playerHealthController;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(photonView != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Name: " + gameObject.name + " | Tag: " + gameObject.tag + " | Collision with: " + collision.gameObject.name + " | Collision object tag: " + collision.gameObject.tag);
        if (!photonView.IsMine)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Gun") || collision.gameObject.CompareTag("AutomaticGun"))
        {
            return;
        }

        if (collision.gameObject.name == "XR Origin")
        {
            //Destroy(gameObject);
            photonView.RPC("DestroyBulletOverNetwork", RpcTarget.All);
            return;
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.tag == "PistolBullet")
        {
            //Debug.Log("Pistol bullet recibed.");
            //Debug.Log("Collision: " + collision.gameObject.name);
            playerHealthController = collision.transform.parent.parent.gameObject.GetComponent<PlayerHealthController>();
            //playerHealthController.RecibeDamage(PISTOL_BULLET_DAMAGE);

            object[] content = new object[] { PISTOL_BULLET_DAMAGE};
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent(DealDamageCode, content, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.tag == "M4Bullet")
        {
            playerHealthController = collision.transform.parent.parent.gameObject.GetComponent<PlayerHealthController>();
            //playerHealthController.RecibeDamage(M4_BULLET_DAMAGE);
            Debug.Log("M4 bullet recibed.");

            object[] content = new object[] { M4_BULLET_DAMAGE };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent(DealDamageCode, content, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
        }
        Destroy(gameObject);
        photonView.RPC("DestroyBulletOverNetwork", RpcTarget.Others);
    }

    [PunRPC]
    private void DestroyBulletOverNetwork()
    {
        Destroy(gameObject);
    }
}
