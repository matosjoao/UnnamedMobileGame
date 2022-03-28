using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class SwitchAnimationKeys
{
    public const string IsGoingDown = "IsGoingDown";
}

public class Switch : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool isStickySwitch;
    [SerializeField] private List<Bridge> bridgesToInteract = new List<Bridge>();
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera zoomOutCam;

    private bool _isOnPlayerCam;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _isOnPlayerCam = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !isStickySwitch)
        {
            SwitchPriority(true);
        }

        _animator.SetBool(SwitchAnimationKeys.IsGoingDown, true);

        RotateBridges(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isStickySwitch) return;

        if (collision.transform.tag == "Player")
        {
            SwitchPriority(false);
        }

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

    private void SwitchPriority(bool zoom)
    {
        if (zoom && _isOnPlayerCam)
        {
            zoomOutCam.gameObject.SetActive(true);
            playerCam.gameObject.SetActive(false);
            playerCam.Priority = 0;
            zoomOutCam.Priority = 1;
            _isOnPlayerCam = false;
        }

        if(!zoom && !_isOnPlayerCam)
        {
            zoomOutCam.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);
            playerCam.Priority = 1;
            zoomOutCam.Priority = 0;
            _isOnPlayerCam = true;
        }
    }
}