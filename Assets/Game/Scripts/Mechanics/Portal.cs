using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal portalDestination;
    [SerializeField] private float boost = 1f;
    [SerializeField] private Transform bottom;

    private float _enterVelocity;
    private Vector2 _exitPosition;
    private Vector3 _direction;
    private bool _teleportInProcess;

    void Start()
    {
        _exitPosition = transform.position;
        _direction = Vector3.Normalize(transform.position - bottom.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterMovement _characterMovement))
        {
            if (!_teleportInProcess)
            {
                TeleportIn(collision.gameObject, _characterMovement);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _teleportInProcess = false;
    }

    private void TeleportIn(GameObject gameObject, CharacterMovement characterMovement)
    {
        // Obter velocidade a que o objeto entra
        _enterVelocity = Vector3.Magnitude(characterMovement.CurrentVelocity);

        // Esconder objeto
        gameObject.SetActive(false);

        // Ativar Particles
        //teleportingParticles.transform.position = enteringGameObject.transform.position;
        //teleportingParticles.SetActive(true);

        // Processar teleport
        portalDestination.TeleportOut(gameObject, characterMovement, _enterVelocity); 

    }
    public void TeleportOut(GameObject gameObject, CharacterMovement characterMovement, float enterVelocity)
    {
        // Colocar que estamos a fazer teleport
        // Se não tivermos isto vai crashar e andar a saltar de um portal para o outro
        // Uma vez que entra logo no collider do portal de destino
        _teleportInProcess = true;

        // Alterar a velocidade de saída do objeto
        characterMovement.CurrentVelocity = _direction * enterVelocity * boost;
        
        // Mostrar objeto
        gameObject.SetActive(true);
        
        // Alterar a posição do RigidBody2D
        characterMovement.Teleport(_exitPosition);

        // Ativar Particles
        //teleportingParticles.transform.position = g.transform.position;
        //teleportingParticles.SetActive(true);
    }
}
