using System.Collections;
using UnityEngine;

public class BridgeBreak : MonoBehaviour
{
    [SerializeField] private int lifeTime = 2;

    private int _lifeTime;
    private bool _isRunning;

    private void Start()
    {
        _lifeTime = lifeTime;
        _isRunning = false;
    }

    IEnumerator CountdownCourotine()
    {
        _isRunning = true;

        while (_lifeTime > 0)
        {
            yield return new WaitForSeconds(1f);

            _lifeTime--;
        }

        _isRunning = false;

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!_isRunning)
                StartCoroutine(CountdownCourotine());
        }
    }


}
