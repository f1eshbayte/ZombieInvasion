using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField, Range(0f, 1f)] private float damageFalloff = 0.7f; // падение урона для каждого следующего врага

    protected override void Shoot(Transform shootPoint, Vector2 direction, float maxDistance)
    {
        TryHitTarget(shootPoint, direction, maxDistance, multiHitEnemies: true, damageFalloff: damageFalloff);
    }
}

