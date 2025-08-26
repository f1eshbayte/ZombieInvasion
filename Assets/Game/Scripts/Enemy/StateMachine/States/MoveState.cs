using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class MoveState : State
{
   [SerializeField] private float _speed;

   private SpriteRenderer _spriteRenderer; 
    private Animator _animator;
   
   
   private const string WalkAnimate = "Walk";
   
   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void OnEnable()
   {
      _animator.Play(WalkAnimate);
   }

   private void OnDisable()
   {
      _animator.StopPlayback();
   }

   private void Update()
   {
      transform.position = Vector2.MoveTowards(transform.position, TargetPlayer.transform.position, _speed * Time.deltaTime);
      Flip();
   }

   private void Flip()
   {
      if (TargetPlayer.transform.position.x > transform.position.x)
         _spriteRenderer.flipX = true;
      else
         _spriteRenderer.flipX = false;
   }
}
