using System;

public interface IDamageable
{
    void TakeDamage(int damage);

    event Action DeathEvent;

    bool IsDead { get; }
}
