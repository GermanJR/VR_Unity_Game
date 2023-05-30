using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsUIBehaviour : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    private int currentRoom;
    private bool hasBeenActivated = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !hasBeenActivated)
        {
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
        }
    }

    public void InitializeFirstRoom()
    {
        ChangeRoom(0);
    }

    public void SelectRoom(int index)
    {
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 7);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }
    
    public void ChangeRoom(int index)
    {
        currentRoom += index;
        SelectRoom(currentRoom);
    }
}
