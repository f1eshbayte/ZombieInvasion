using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour, IShopItem
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isAutomatic = false;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private AudioClip _shootClip;

    [NonSerialized] private float _nextFireTime;

    private Coroutine _autoShootCoroutine;
    private bool _isShooting;

    protected AudioClip ShootClip => _shootClip;
    public int Damage => _damage;
    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;

    protected abstract void Shoot(Transform shootPoint, Vector2 direction, float maxDistance);
    
    protected void TryHitTarget(Transform shootPoint, Vector2 direction, float maxDistance, bool multiHitEnemies = false, float damageFalloff = 1f)
    {
        RaycastHit2D[] hits = GetHits(shootPoint.position, direction, maxDistance);

        DealDamage(hits, multiHitEnemies, damageFalloff);

        PlayShootEffects(shootPoint, direction, maxDistance);
    }


    public void Buy()
    {
        _isBuyed = true;
    }
    public void HandleShooting(Transform shootPoint, PlayerVisual playerVisual, bool isDown)
    {
        _isShooting = isDown;

        if (_isAutomatic)
        {
            if (isDown && _autoShootCoroutine == null)
                _autoShootCoroutine = playerVisual.StartCoroutine(AutoShoot(shootPoint, playerVisual));
            else if (!isDown && _autoShootCoroutine != null)
            {
                playerVisual.StopCoroutine(_autoShootCoroutine);
                _autoShootCoroutine = null;
            }
        }
        else
        {
            if (isDown && Time.time >= _nextFireTime)
            {
                Shoot(shootPoint, shootPoint.right, _maxDistance);
                playerVisual.PlayShoot();
                _nextFireTime = Time.time + _fireRate;
            }
        }
    }

    

    private RaycastHit2D[] GetHits(Vector2 origin, Vector2 direction, float maxDistance)
    {
        int mask = LayerMask.GetMask("Enemy", "Obstacle");
        var hits = Physics2D.RaycastAll(origin, direction, maxDistance, mask);
        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        return hits;
    }

    private void DealDamage(RaycastHit2D[] hits, bool multiHitEnemies, float damageFalloff)
    {
        float currentDamage = Damage;

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Door door))
                break;

            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Mathf.Max(1, Mathf.RoundToInt(currentDamage)));

                if (!multiHitEnemies)
                    break;

                currentDamage *= damageFalloff;
            }
        }
    }

    private void PlayShootEffects(Transform shootPoint, Vector2 direction, float maxDistance)
    {
        AudioSource.PlayClipAtPoint(ShootClip, shootPoint.position);
        Debug.DrawRay(shootPoint.position, direction.normalized * maxDistance, Color.red, 0.5f);
    }


    private IEnumerator AutoShoot(Transform shootPoint, PlayerVisual playerVisual)
    {
        while (_isShooting)
        {
            if (Time.time >= _nextFireTime)
            {
                Shoot(shootPoint, shootPoint.right, _maxDistance);
                playerVisual.PlayShoot();
                _nextFireTime = Time.time + _fireRate;
            }

            yield return null;
        }
    }
}