using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterFacing : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateFacing(Vector2 movementInput)
    {
        if (movementInput.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    public bool IsFacingRight()
    {
        return _spriteRenderer.flipX == false;
    }
}
