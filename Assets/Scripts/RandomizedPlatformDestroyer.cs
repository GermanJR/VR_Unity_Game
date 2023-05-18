using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedPlatformDestroyer : MonoBehaviour
{
    private const string BREAK_TAG = "BreakPlatform";

    private GameObject[] platforms = new GameObject[3];
    [SerializeField] private bool destroyTwoPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;

        foreach (Transform child in gameObject.transform)
        {
            platforms[index] = child.gameObject;
            index++;
        }

        int indexToChangeTag = Random.Range(0, 3);
        if (!destroyTwoPlatforms)
        {
            platforms[indexToChangeTag].gameObject.tag = BREAK_TAG;
            platforms[indexToChangeTag].gameObject.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            platforms[indexToChangeTag].gameObject.tag = BREAK_TAG;
            platforms[indexToChangeTag].gameObject.GetComponent<Collider>().isTrigger = true;

            int takenIndex = indexToChangeTag;
            int newIndexSecondPlatform = Random.Range(0, 3);
            while (takenIndex == newIndexSecondPlatform)
            {
                newIndexSecondPlatform = Random.Range(0, 3);
            }

            platforms[newIndexSecondPlatform].gameObject.tag = BREAK_TAG;
            platforms[newIndexSecondPlatform].gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
