using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
