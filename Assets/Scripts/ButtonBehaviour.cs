using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{

    public UnityEvent OnButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected.");
        if (collision.gameObject.CompareTag("Button"))
        {
            Debug.Log("Correct collision");
            OnButtonPressed.Invoke();
        }
    }
}
