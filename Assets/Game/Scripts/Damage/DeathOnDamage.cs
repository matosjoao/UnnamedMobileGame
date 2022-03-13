using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IDamageable
{
    public bool IsDead { get; private set; }

    public event Action DeathEvent;

    private void Awake()
    {
        IsDead = false;
    }

    // Queremos que várias personagens tenham vida e que sofram damage
    // Desta maneira conseguimos associar este script sempre a um jogador, enimigo, que sofra damage
    // e depois no componente principal desse jogador ou enimigo implementamos a logica de tirar vida
    // Isto porque podemos querer fazer diferentes coisas ao sofrer dano, dependentemente de quem seja
    // Tocar um som diferente, se for o player diminuir vidas, se for um enimigo morrer logo...
    // Assim no componente principla do jogador subscrevemos para ouvir este evento "DamageEvent"
    public void TakeDamage(int damage)
    {
        IsDead = true;
        DeathEvent.Invoke();
    }
}
