using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class AutomaticFireBullet : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    public float firerate = 8f;

    public InputActionProperty trigger;

    private Coroutine coroutine;
    private XRGrabInteractable interactable;

    private XRBaseController controller;

    private AudioSource shotSound;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        //interactable.activated.AddListener(Fire);

        controller = GetComponent<XRBaseController>();

        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginFire()
    {
        if (coroutine!= null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(FireRoutine());
    }

    public void StopFire()
    {
        if (coroutine != null) StopCoroutine(coroutine);

    }

    /*
    public void Fire(ActivateEventArgs args)
    {
        while(trigger.action.ReadValue<float>() > 0.1f)
        {
            if (Time.time - lastFired > 1 / firerate)
            {
                lastFired = Time.time;

                GameObject spawnedBullet = Instantiate(bullet);
                spawnedBullet.transform.position = spawnPoint.position;
                spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
                Destroy(spawnedBullet, 3);
            }
        }
    }
    */
    public IEnumerator FireRoutine()
    {
        while (true)
        {
            shotSound.Play();
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 3);

            yield return new WaitForSeconds(1f / firerate);
        }
    } 
}
