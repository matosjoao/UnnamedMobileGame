using System;

public interface IDamageable
{
    void TakeDamage(bool dead = false);

    event Action OnDamageEvent;

    event Action OnDeathEvent;

}
