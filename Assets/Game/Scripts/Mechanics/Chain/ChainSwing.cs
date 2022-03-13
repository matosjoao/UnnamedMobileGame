using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ChainSwing : MonoBehaviour
{
    private HingeJoint2D _hingeJoint2D;
    private JointMotor2D _jointMotor2D;
    private Transform _transform;

    [SerializeField] private float swingSpeed = 60;
    [SerializeField] private Vector3 upperAngle = new Vector3(0, 0, 300);
    [SerializeField] private Vector3 lowerAngle = new Vector3(0, 0, 60);

    private void Awake()
    {
        _hingeJoint2D = GetComponent<HingeJoint2D>();
        _jointMotor2D = _hingeJoint2D.motor;
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_transform.localEulerAngles == upperAngle)
        {
            _jointMotor2D.motorSpeed = -swingSpeed;
            _hingeJoint2D.motor = _jointMotor2D;
        }

        if (_transform.localEulerAngles == lowerAngle)
        {
            _jointMotor2D.motorSpeed = swingSpeed;
            _hingeJoint2D.motor = _jointMotor2D;
        } 
    }
}
