using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyPlayer1StartTrigger : MonoBehaviour
{
    [SerializeField] private AlleyGameStarter alleyGameStarter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alleyGameStarter.ChangeStatesForPlayer1();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alleyGameStarter.ChangeStatesForPlayer1();
        }
    }
}
