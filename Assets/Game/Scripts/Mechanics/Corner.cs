using UnityEngine;

public class Corner : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        //if (interactable != null)
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log("#Entrou2");

            Vector2 force = GetForceImpulse();

            interactable.ChangeDirection(force);
        }
    }

    private Vector2 GetForceImpulse()
    {
        if (_transform.eulerAngles.z == 270)
        {
            return new Vector2(0, -1);
        }
        else if (_transform.eulerAngles.z == 180)
        {
            return new Vector2(-1, 0);
        }
        else if (_transform.eulerAngles.z == 90)
        {
            return new Vector2(0, 1);
        }
        else
        {
            return new Vector2(1, 0);
        }
    }
}
