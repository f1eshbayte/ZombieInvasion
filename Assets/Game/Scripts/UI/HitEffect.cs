using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Image))]
public class HitEffect : MonoBehaviour
{
    [SerializeField] private float _fadeDuratoin = 0.5f;
    [SerializeField] private float _maxDamage = 100f;

    private Player _player;
    private Image _hitEffect;
    private Tween _fadeTween;
    private float _defaultAlpha = 0f;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _hitEffect = GetComponent<Image>();
        SetAlpha(_defaultAlpha);
    }


    private void OnEnable()
    {
        _player.DamageTaken += OnHandleDamageEffect;
    }

    private void OnDisable()
    {
        _player.DamageTaken -= OnHandleDamageEffect;
    }

    private void OnHandleDamageEffect(int damage)
    {
        float addAlpha = Mathf.Clamp01(damage / _maxDamage);
        float currentAlpha = _hitEffect.color.a;
        float targetAlpha = Mathf.Clamp01(currentAlpha + addAlpha);

        _fadeTween?.Kill();

        SetAlpha(targetAlpha);
        _fadeTween = _hitEffect.DOFade(_defaultAlpha, _fadeDuratoin);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _hitEffect.color;
        color.a = alpha;
        _hitEffect.color = color;
    }
}