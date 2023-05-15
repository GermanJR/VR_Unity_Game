using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletActivateNetWork : MonoBehaviourPun
{
    //public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;

    private AudioSource shotSound;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);

        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireBullet(ActivateEventArgs args)
    {
        shotSound.Play();
        //photonView.RPC("FireBulletOverNetwork", RpcTarget.All);

        
        GameObject spawnedBullet = PhotonNetwork.Instantiate("Bullet Alley", spawnPoint.position, spawnPoint.rotation);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        /*
        PhotonView.Destroy(spawnedBullet);
        Destroy(spawnedBullet, 3);
        */
    }

    /*
    [PunRPC]
    private void FireBulletOverNetwork()
    {
        GameObject spawnedBullet = PhotonNetwork.Instantiate("Bullet Alley", new Vector3(0,0,0), new Quaternion(0,0,0,0));
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
    }
    */
}
