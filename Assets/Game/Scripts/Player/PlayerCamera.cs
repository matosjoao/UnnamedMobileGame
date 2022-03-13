using UnityEngine;

[RequireComponent(typeof(CharacterFacing))]
[RequireComponent(typeof(CharacterMovement))]
public class PlayerCamera : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private CharacterFacing _characterFacing;

    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float cameraTargetOffsetX = 2.0f; // Quanto offset vai estar a camera á nossa frente
    [Range(0.5f, 50.0f)]
    [SerializeField] private float cameraTargetFlipSpeed = 2.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float characterSpeedInfluence = 2.0f;

    [Header("Fire Position")]
    [SerializeField] private Transform firePosition;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float firePositionOffsetX = 2.0f;


    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterFacing = GetComponent<CharacterFacing>();
    }
    private void FixedUpdate()
    {
        // Controlo da Camera Target
        // Para que vá sempre para a nossa frente
        bool isFacinRight = _characterFacing.IsFacingRight();
        float targetOffsetX = isFacinRight ? cameraTargetOffsetX : -cameraTargetOffsetX;

        float currentOffsetX = Mathf.Lerp(cameraTarget.localPosition.x, targetOffsetX, Time.fixedDeltaTime * cameraTargetFlipSpeed);

        currentOffsetX += _characterMovement.CurrentVelocity.x * Time.fixedDeltaTime * characterSpeedInfluence;

        cameraTarget.localPosition = new Vector3(currentOffsetX, cameraTarget.localPosition.y, cameraTarget.localPosition.z);


        //Fire Position
        float fireOffsetX = isFacinRight ? firePositionOffsetX : -firePositionOffsetX;
        firePosition.localPosition = new Vector3(fireOffsetX, firePosition.localPosition.y, firePosition.localPosition.z);
    }
}
