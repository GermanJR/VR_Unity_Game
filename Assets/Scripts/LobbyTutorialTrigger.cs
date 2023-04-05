using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTutorialTrigger : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelLoader.LoadNewScene("InstructionsScene");
        }
    }
}
