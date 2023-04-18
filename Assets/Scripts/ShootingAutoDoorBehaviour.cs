using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingAutoDoorBehaviour : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject canvas;
    public TMP_Text textRemainignCans;
    public GameObject insideAntiTeleport;

    private AudioSource openSound;

    private int cansRemaining = -1;
    private bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        openSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened)
        {
            if (cansRemaining == 0)
            {
                openSound.Play();
                doorAnimator.SetTrigger("open");
                Destroy(canvas);
                Debug.Log("All cans destroyed.");
                isOpened = true;
                Destroy(insideAntiTeleport);
            }

            cansRemaining = GameObject.FindGameObjectsWithTag("Can").Length;
            textRemainignCans.text = cansRemaining.ToString();
        }
    }
}
