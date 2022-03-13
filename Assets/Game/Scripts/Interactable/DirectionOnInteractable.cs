using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionOnInteractable : MonoBehaviour, IInteractable
{
    public event Action<Vector2> ChangeDirectionEvent;

    public void ChangeDirection(Vector2 direction)
    {
        ChangeDirectionEvent.Invoke(direction);
    }
}
