using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class ScorePopup : MonoBehaviour
{
    private IObjectPool<ScorePopup> _poll;
    private TextMeshPro _textMesh;

    [SerializeField] private TMP_Text scoreTextField;
    [SerializeField] private float lifeTime = 1.0f;
    [SerializeField] private float disappearSpeed = 3f;
    [SerializeField] private float increaseScaleAmount = .5f;
    [SerializeField] private float decreaseScaleAmount = .5f;

    private float _lifeTime;
    public float LifeTime
    {
        get { return _lifeTime; }
        set { _lifeTime = Mathf.Clamp(value, 0, lifeTime); }
    }
    private bool _isActive;
    private Color _textColor;
    private Vector3 _moveVector;
    private static int _sortingOrder;

    public void SetPool(IObjectPool<ScorePopup> poll) => _poll = poll;

    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Init(int value, bool isCritical)
    {
        _isActive = true;
        _textMesh.SetText(value.ToString());
        if (isCritical)
        {
            _textMesh.fontSize = 9;
            _textColor = new Color(235, 68, 65, 1);
        }
        else
        {
            _textMesh.fontSize = 7;
            _textColor = new Color(0, 0, 0, 1);
        }
        _textMesh.color = _textColor;
        LifeTime = lifeTime;
        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
        _moveVector = new Vector3(-.8f, 1) * 40f;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        // Move Up
        transform.position += new Vector3(0, 2f) * Time.deltaTime;

        // Move Up/Right
        //transform.position += _moveVector * Time.deltaTime;
        //_moveVector -= _moveVector * 8f * Time.deltaTime;

        if (LifeTime > lifeTime * .5f)
        {
            // Primeira parte do tempo
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            // Segunda parte do tempo
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if(_textColor.a < 0)
            {
                _isActive = false;
                if (_poll != null)
                    _poll.Release(this);
                else
                    Destroy(gameObject);
            }
        }
    }

}
