using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SnowBallEffect : MonoBehaviour
{
    private IObjectPool<SnowBallEffect> _poll;

    [SerializeField] private float lifeTime = 1f;

    public float LifeTime
    {
        get { return lifeTime; }
        set { lifeTime = Mathf.Clamp(value, 0, 1.0f); }
    }

    public void SetPool(IObjectPool<SnowBallEffect> poll) => _poll = poll;

    private void Update()
    {
        LifeTime -= Time.deltaTime;

        if(LifeTime <= 0)
        {
            if (_poll != null)
                _poll.Release(this);
            else
                Destroy(gameObject);
        }
    }
}
