using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private enum TransitionState
    {
        Idle,
        AtTarget,
        Moving,
        Returning
    }

    public bool isToggle;
    public float speed;
    public Vector3 target;
    private Vector3 start;
    private TransitionState transitionState;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        transitionState = TransitionState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (transitionState)
        {
            case TransitionState.Idle:
                break;
            case TransitionState.AtTarget:
                if (!isToggle)
                {
                    transitionState = TransitionState.Returning;
                }
                break;
            case TransitionState.Moving:
                TryMove(target, TransitionState.AtTarget, 1);
                break;
            case TransitionState.Returning:
                TryMove(start, TransitionState.Idle, -1);
                break;
        }
    }

    public void LeverInteraction(bool toggleOn)
    {
        if (isToggle)
        {
            if (toggleOn)
            {
                transitionState = TransitionState.Moving;
            }
            else
            {
                transitionState = TransitionState.Returning;
            }
        }
        else
        {
            if (transitionState == TransitionState.Idle)
            {
                transitionState = TransitionState.Moving;
            }
        }
    }

    void TryMove(Vector3 currentTarget, TransitionState targetTransition, int factor)
    {
        Vector3 move = target - start;
        Vector3 direction = move.normalized;
        Vector3 change = direction * speed / 1000 * factor;
        if (Vector3.Distance(transform.position + change, currentTarget) >= Vector3.Distance(transform.position, currentTarget))
        {
            transitionState = targetTransition;
        }
        else
        {
            transform.position = transform.position + change;
        }
    }
}
