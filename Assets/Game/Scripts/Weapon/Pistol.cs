using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot(Transform shootPoint, Vector2 direction, float maxDistance)
    {
        // Debug.Log("Shoot  ИЗ ПИСТОЛЕТА!");
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, maxDistance, LayerMask.GetMask("Enemy"));

        if (hit.collider != null && hit.collider.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
                Debug.Log("попал по зомби");
            }
        }
        
        Debug.DrawRay(shootPoint.position, direction * maxDistance, Color.red, 0.5f);
    }
}
