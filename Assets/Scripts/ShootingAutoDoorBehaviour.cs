using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAutoDoorBehaviour : MonoBehaviour
{
    public Animator doorAnimator;

    private int cansRemaining = -1;
    private bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened)
        {
            if (cansRemaining == 0)
            {
                doorAnimator.SetTrigger("open");
                Debug.Log("All cans destroyed.");
                isOpened = true;
            }

            cansRemaining = GameObject.FindGameObjectsWithTag("Can").Length;
            Debug.Log("Remaining cans = " + cansRemaining);
        }
    }
}
