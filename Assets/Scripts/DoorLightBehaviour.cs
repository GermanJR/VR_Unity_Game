using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightBehaviour : MonoBehaviour
{

    public Color redColor;
    public Color greenColor;

    private MeshRenderer mesh;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        material = mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToGreenLight()
    {
        material.SetColor("_EmissionColor", greenColor * Mathf.LinearToGammaSpace(2f));
    }
    
    public void ChangeToRedLight()
    {
        material.SetColor("_EmissionColor", redColor * Mathf.LinearToGammaSpace(2f));
    }
}
