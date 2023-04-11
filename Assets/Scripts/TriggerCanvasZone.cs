using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCanvasZone : MonoBehaviour
{

    private Animator canvasAnimator;
    private AudioSource[] sounds;

    private AudioSource enterSound;
    private AudioSource hideSound;


    // Start is called before the first frame update
    void Start()
    {
        canvasAnimator = transform.parent.gameObject.GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();

        enterSound = sounds[0];
        hideSound = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got trigger entered: " + other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger entered correct");
            canvasAnimator.SetTrigger("Enter");
            enterSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger: " + other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exited was correct");
            canvasAnimator.SetTrigger("Hide");
            hideSound.Play();
        }
    }
}
