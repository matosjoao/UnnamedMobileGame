using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Vector3[] positions;
    [SerializeField] private bool IsAutomatic = false;

    private Transform _transform;
    private int _index;
    private bool _isMoving;

    public bool IsMoving
    {
        get { return _isMoving; }
        set { _isMoving = value; }
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (!IsMoving && !IsAutomatic) return;

        if (_transform.position == positions[_index])
        {
            if (_index == positions.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsMoving && !IsAutomatic) return;

        _transform.position = Vector2.MoveTowards(_transform.position, positions[_index], speed * Time.deltaTime);
    }
}
