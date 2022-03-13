using UnityEngine;
using System;

public interface IInteractable
{
    void ChangeDirection(Vector2 direction);

    event Action<Vector2> ChangeDirectionEvent;
}
