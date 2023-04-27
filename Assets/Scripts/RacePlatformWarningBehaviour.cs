using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlatformWarningBehaviour : MonoBehaviour
{

    private GameObject canvasParent;

    // Start is called before the first frame update
    void Start()
    {
        canvasParent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(canvasParent);
        }
    }
}
