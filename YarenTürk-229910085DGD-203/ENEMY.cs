using System;

public abstract class Enemy
{
    private int _health;
    private int _damage;

    public int Health
    {
        get => _health;
        protected set => _health = Math.Max(value, 0);
    }

    public int Damage
    {
        get => _damage;
        protected set => _damage = Math.Max(value, 0);
    }

    public Enemy(int health, int damage)
    {
        Health = health;
        Damage = damage;
    }

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
    }
}

