using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MoveState : State
{
   [SerializeField] private float _speed;

   private SpriteRenderer _spriteRenderer;
   private void Start()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
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
