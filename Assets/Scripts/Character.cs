using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  protected Rigidbody2D rb2d;
  protected SpriteRenderer spriteRenderer;
  protected Animator animator;
  protected float playerSpeed = 4f;

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    var horizontal = Input.GetAxisRaw("Horizontal");
    var vertical = Input.GetAxisRaw("Vertical");
    rb2d.velocity = new Vector2(horizontal, vertical) * playerSpeed;

    bool isWalking = animator.GetBool("isWalking");
    bool shouldBeWalking = horizontal != 0 || vertical != 0;

    if (Input.GetMouseButtonDown(0)) {
      animator.SetTrigger("isAttacking");
    } 

    if (isWalking != shouldBeWalking) {
      animator.SetBool("isWalking", shouldBeWalking);
    }

    if (horizontal != 0) {
      animator.SetInteger("direction", horizontal == 1 ? 1 : 3);
      spriteRenderer.flipX = horizontal == -1;
    } else if (vertical != 0) {
      animator.SetInteger("direction", vertical == 1 ? 0 : 2);
    }
  }
}
