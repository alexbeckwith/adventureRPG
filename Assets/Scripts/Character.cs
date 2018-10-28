using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public GameObject projectile;

  protected Rigidbody2D rb2d;
  protected SpriteRenderer spriteRenderer;
  protected Animator animator;
  protected float playerSpeed = 3.0f;

  protected float projectileSpeed = 7.0f;
  protected float projectileLifetime = 2.0f;
  protected float projectileRate = 0.66f;
  protected float nextProjectileTime = 0.0f; 

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void FixedUpdate()
  {
    var velocity = GetVelocity(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    var direction = GetDirection(velocity);
    rb2d.velocity = velocity;

    bool isWalking = direction != -1;
    animator.SetBool("isWalking", isWalking);

    if (isWalking) {
      animator.SetInteger("direction", direction);
      spriteRenderer.flipX = direction == 3;
    }
  }

  void Update()
  {
    if (Input.GetButtonDown("Fire1") && Time.time > nextProjectileTime) {
      animator.SetTrigger("isAttacking");
      Shoot();
    }
  }

  Vector2 GetVelocity(float horizontal, float vertical) 
  {
    return new Vector2(horizontal, vertical) * playerSpeed;
  }

  int GetDirection(Vector2 velocity) 
  {
    if (velocity.x != 0) {
      return velocity.x > 0 ? 1 : 3;
    } 

    if (velocity.y != 0) {
      return velocity.y > 0 ? 0 : 2;
    }

    return -1;
  }

  void Shoot()
  {
    nextProjectileTime = Time.time + projectileRate;
    Vector3 lookRotation = new Vector3(0, 0, 0);
    Vector2 velocity = new Vector2(0, 0);

    switch (animator.GetInteger("direction")) {
      // up
      case 0:
        velocity.y = 1;
        break;
      // down
      case 2:
        velocity.y = -1;
        lookRotation.z = -180;
        break;
      // right
      case 1:
        velocity.x = 1;
        lookRotation.z = -90;
        break;
      // left
      case 3:
      default:
        velocity.x = -1;
        lookRotation.z = 90;
        break;
    }
    
    var arrow = Instantiate(projectile, transform.position, Quaternion.Euler(lookRotation));
    arrow.GetComponent<Rigidbody2D>().velocity = velocity * projectileSpeed;

    Destroy(arrow, projectileLifetime);
  }
}
