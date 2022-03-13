using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SwitchAnimationKeys
{
    public const string IsGoingDown = "IsGoingDown";
}

public class Switch : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool isStickySwitch;
    [SerializeField] private List<Bridge> bridgesToInteract = new List<Bridge>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _animator.SetBool(SwitchAnimationKeys.IsGoingDown, true);

        RotateBridges(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isStickySwitch) return;

        _animator.SetBool(SwitchAnimationKeys.IsGoingDown, false);

        RotateBridges(false);
    }

    private void RotateBridges(bool isRotating)
    {
        for (int i = 0; i < bridgesToInteract.Count; i++)
        {
            bridgesToInteract[i].IsRotating = isRotating;
        }
    }
}