using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
  [SerializeField]
  private GameObject weaponGameObject;
  [SerializeField]
  private Slider healthSlider;
  
  private Weapon weapon;
  private Health health;
  private Movement movement;
  private Animator animator;

  private int lookDirection = 2;
  private float invincibilityTime = 1.0f;
  private float invincibilityEnd = 0.0f;

  void Awake()
  {
    weapon = Instantiate(weaponGameObject, transform).GetComponent<Weapon>();
    health = GetComponent<Health>();
    animator = GetComponent<Animator>();
    movement = GetComponent<Movement>();

    health.OnDied += OnDied;
    health.OnTakeDamage += OnTakeDamage;
  }

  void Update()
  {
    Walk();
    Attack();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    Debug.Log((Time.time, invincibilityEnd));
    if (!IsInvincible() && collision.gameObject.tag == "Enemy") {
      health.TakeDamage(1);
    }
  }

  bool IsInvincible()
  {
    return Time.time < invincibilityEnd;
  }

  void OnDied()
  {
    movement.enabled = false; 
  }

  void OnTakeDamage(float health)
  {
    invincibilityEnd = Time.time + invincibilityTime;
    healthSlider.value = health;
  }

  Vector2 GetVelocity(float horizontal, float vertical)
  {
    return new Vector2(horizontal, vertical);
  }

  void Walk()
  {
    var direction = movement.Move(
        new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
    );

    if (direction != -1) {
        lookDirection = direction;
    }
  }

  void Attack()
  {
    if (Input.GetButtonDown("Fire1") && weapon != null) {
      if (weapon.Attack(lookDirection)) {
        Debug.Log(lookDirection);
        animator.SetTrigger("isAttacking");
      }
    }
  }
}
