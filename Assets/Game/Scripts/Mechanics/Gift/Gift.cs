using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Gift : MonoBehaviour
{
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private Animator _animator;

    [SerializeField] private AudioClip pickSound;
    [SerializeField] private Sprite[] images;

    private int _random;
    private int _amount;
    private int _randomAnim;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _random = Random.Range(0, images.Length);
        _randomAnim = Random.Range(0, 2);
        _amount = _randomAnim > 0 ? Random.Range(15, 50) : 10;

        _spriteRenderer.sprite = images[_random];
        _animator.SetInteger(GiftAnimationKeys.Animation, _randomAnim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.transform.tag == "Player")
        {
            PlayerControl playerControl = collision.GetComponent<PlayerControl>();
            if(playerControl != null)
            {
                // Adicionar score
                playerControl.AddScore(_amount);
                // Reproduzir som
                _audioSource.PlayOneShot(pickSound);
                // Adicionar efeito especial
                if(_randomAnim > 0)
                {
                    //TODO:: Ir buscar a pool do GameController
                    var confettiEffect = playerControl.GetConfetti();
                    confettiEffect.transform.position = transform.position;
                    confettiEffect.transform.rotation = transform.rotation;
                    confettiEffect.LifeTime = 1.0f;
                }
                //TODO:: Ir buscar a pool do GameController
                var scoreEffect = playerControl.GetScorePopup();
                scoreEffect.transform.position = transform.position;
                scoreEffect.transform.rotation = Quaternion.identity;
                scoreEffect.transform.localScale = Vector3.one;
                scoreEffect.Init(_amount, _randomAnim > 0 ? true : false);

                //TODO:: Alterar para pool do GameController??
                _spriteRenderer.enabled = false;
                _collider.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}

public static class GiftAnimationKeys
{
    public const string Animation = "Animation";
}

