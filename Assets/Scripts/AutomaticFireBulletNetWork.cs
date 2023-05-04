using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AutomaticFireBulletNetWork : MonoBehaviourPun
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    public float firerate = 8f;

    public InputActionProperty trigger;

    private Coroutine coroutine;
    private XRGrabInteractable interactable;

    private XRBaseController controller;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();

        controller = GetComponent<XRBaseController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    public void BeginFire()
    {
        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(FireRoutine());
    }

    [PunRPC]
    public void StopFire()
    {
        if (coroutine != null) StopCoroutine(coroutine);

    }

    public IEnumerator FireRoutine()
    {
        while (true)
        {
            GameObject spawnedBullet = Instantiate(bullet);
            /*
            spawnedBullet.AddComponent<HealthBarManager>();
            spawnedBullet.AddComponent<PlayerHealthController>();
            */
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 3);

            yield return new WaitForSeconds(1f / firerate);
        }
    }
}
