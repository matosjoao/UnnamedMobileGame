using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Transform _transform;
    private bool _isRotating;

    [SerializeField] private float rotateSpeed = 50;
    [SerializeField] private bool isAutomatic;

    public bool IsRotating
    {
        get { return _isRotating; }
        set { _isRotating = value; }
    }

    private void Awake()
    {
        _transform = transform;

        if (isAutomatic) IsRotating = true;
    }

    private void Update()
    {
        if (!IsRotating) return;

        _transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
    }
}
