using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingAutoDoorBehaviour : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject canvas;
    public TMP_Text textRemainignCans;

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
                Destroy(canvas);
                Debug.Log("All cans destroyed.");
                isOpened = true;
            }

            cansRemaining = GameObject.FindGameObjectsWithTag("Can").Length;
            textRemainignCans.text = cansRemaining.ToString();
            Debug.Log("Remaining cans = " + cansRemaining);
        }
    }
}
