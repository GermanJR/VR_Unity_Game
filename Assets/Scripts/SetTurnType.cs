using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    public void SetTypeFromIndex(int index)
    {
        try
        {
            if (index == 0)
            {
                snapTurn.enabled = false;
                continuousTurn.enabled = true;
            }
            else if (index == 1)
            {
                snapTurn.enabled = true;
                continuousTurn.enabled = false;
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Error changin turn type." + e.Message);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Turn type: Unexpected error -> " + e.Message);
        }
    }
}
