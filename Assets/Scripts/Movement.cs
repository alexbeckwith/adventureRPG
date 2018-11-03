using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
  [SerializeField]
  private float speed = 1.25f;

  private Rigidbody2D rb2d;
  private SpriteRenderer spriteRenderer;
  private Animator animator;

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public int Move(Vector2 movement)
  {
    var direction = GetDirection(movement);
    rb2d.position += movement * Time.deltaTime * speed;
    UpdateSprite(direction);

    return direction;
  }

  int GetDirection(Vector2 velocity)
  {
    if (velocity.x != 0)
    {
      return velocity.x > 0 ? 1 : 3;
    }

    if (velocity.y != 0)
    {
      return velocity.y > 0 ? 0 : 2;
    }

    return -1;
  }

  void UpdateSprite(int direction)
  {
    if (animator && spriteRenderer) {
      bool isWalking = direction != -1;
      animator.SetBool("isWalking", isWalking);

      if (isWalking) {
        animator.SetInteger("direction", direction);
        spriteRenderer.flipX = direction == 3;
      }
    }
  }
}
