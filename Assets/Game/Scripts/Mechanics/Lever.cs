using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Lever : MonoBehaviour
{
    [SerializeField] private List<MovingPlatform> platforms = new List<MovingPlatform>();

    private HingeJoint2D _hingeJoint2D;
    private bool _isMoving;

    private void Awake()
    {
        _hingeJoint2D = GetComponent<HingeJoint2D>();
    }

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, -45);
    }

    private void FixedUpdate()
    {
        if(_hingeJoint2D.jointAngle > 30 && _isMoving)
        {
            _isMoving = false;
            MovePlatforms(false);
        }
        else if (_hingeJoint2D.jointAngle < -30 && !_isMoving)
        {
            _isMoving = true;
            MovePlatforms(true);
        }
    }

    private void MovePlatforms(bool isMoving)
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].IsMoving = isMoving;
        }
    }
}
