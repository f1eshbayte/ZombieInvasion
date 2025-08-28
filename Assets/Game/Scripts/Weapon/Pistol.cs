using UnityEngine;

public class Pistol : Weapon
{
    protected override void Shoot(Transform shootPoint, Vector2 direction, float maxDistance)
    {
        TryHitTarget(shootPoint, direction, maxDistance);
    }
}