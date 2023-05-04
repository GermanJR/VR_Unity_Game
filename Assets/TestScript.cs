using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] private PlayerHealthController playerHealthController;


    private void Awake()
    {
        healthBarManager = GameObject.Find("HealthBarManager").GetComponent<HealthBarManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
