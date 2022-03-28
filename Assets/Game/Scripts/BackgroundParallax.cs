using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private float parallaxEffect;

    private float _lenght, _startpos;

    private void Start()
    {
        _startpos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float distance = camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(_startpos + distance, transform.position.y, transform.position.z);

        if (temp > _startpos + _lenght) _startpos += _lenght;
        else if (temp < _startpos - _lenght) _startpos -= _lenght;
    }
}
