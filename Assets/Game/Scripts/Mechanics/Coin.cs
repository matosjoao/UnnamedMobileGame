using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    [SerializeField] private int coinAmount = 1;
    [SerializeField] private AudioClip coinSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.transform.tag == "Player")
        {
            PlayerControl playerControl = collision.GetComponent<PlayerControl>();
            if(playerControl != null)
            {
                playerControl.AddCoins(coinAmount);
                _audioSource.PlayOneShot(coinSound);

                _spriteRenderer.enabled = false;
                _collider.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
