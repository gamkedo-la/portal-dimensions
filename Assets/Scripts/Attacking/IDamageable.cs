using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);

    Transform GetTransform();
}
