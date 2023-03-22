using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverBehaviour : MonoBehaviour
{
    public float angleBetweenThreshold = 1f;
    public HingeJoinState status = HingeJoinState.None;

    public UnityEvent OnMinReached;
    public UnityEvent OnMaxReached;

    public enum HingeJoinState {Min, Max, None}

    private HingeJoint hingeJoint;

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float angleWithMinValue = Mathf.Abs(hingeJoint.angle - hingeJoint.limits.min);
        float angleWithMaxValue = Mathf.Abs(hingeJoint.angle - hingeJoint.limits.max);

        if (angleWithMinValue < angleBetweenThreshold)
        {
            if(status != HingeJoinState.Min)
            {
                Debug.Log("Triggering min event.");
                OnMinReached.Invoke();
            }
            status = HingeJoinState.Min;
        }
        else if(angleWithMaxValue < angleBetweenThreshold)
        {
            if(status != HingeJoinState.Max)
            {
                Debug.Log("Triggering MAX event.");
                OnMaxReached.Invoke();
            }
            status = HingeJoinState.Max;
        }
        else
        {
            status = HingeJoinState.None;
        }
    }
}
