using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyBulletBehaviour : MonoBehaviourPun
{

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

        if (collision.gameObject.name == "XR Origin" || (!photonView.IsMine))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.tag == "PistolBullet")
        {
            /*
            playerHealthController = GameObject.Find("AlleyNetworkPlayer (Clone)").GetComponent<PlayerHealthController>();
            playerHealthController.RecibeDamage(PISTOL_BULLET_DAMAGE);
            */
            
            Debug.Log("Pistol bullet recibed.");
            Debug.Log("Collision: " + collision.gameObject.name);
            playerHealthController = collision.transform.parent.parent.gameObject.GetComponent<PlayerHealthController>();
            playerHealthController.RecibeDamage(PISTOL_BULLET_DAMAGE);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.tag == "M4Bullet")
        {
            playerHealthController = collision.transform.parent.parent.gameObject.GetComponent<PlayerHealthController>();
            playerHealthController.RecibeDamage(M4_BULLET_DAMAGE);
            Debug.Log("M4 bullet recibed.");
            Destroy(gameObject);
        }
    }
}
