using System;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IDamageable
{
    private PlayerControl _playerControl;

    public event Action OnDamageEvent;

    public event Action OnDeathEvent;

    private void Awake()
    {
        _playerControl = GetComponent<PlayerControl>();
    }

    // Queremos que várias personagens tenham vida e que sofram damage
    // Desta maneira conseguimos associar este script sempre a um jogador, enimigo, que sofra damage
    // e depois no componente principal desse jogador ou enimigo implementamos a logica de tirar vida
    // Isto porque podemos querer fazer diferentes coisas ao sofrer dano, dependentemente de quem seja
    // Tocar um som diferente, se for o player diminuir vidas, se for um enimigo morrer logo...
    // Assim no componente principla do jogador subscrevemos para ouvir este evento "DamageEvent"
    public void TakeDamage(bool dead = false)
    {
        // Is dead?
        if ((_playerControl != null && _playerControl.Lifes <= 1) || dead)
        {
            OnDeathEvent.Invoke();
        }
        else if(_playerControl != null)
        {
            OnDamageEvent.Invoke();
        }
    }
}
