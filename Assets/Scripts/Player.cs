using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
  [SerializeField]
  private GameObject weaponGameObject;
  
  private Weapon weapon;
  private Movement movement;
  private Animator animator;

  private int lookDirection = 2;

  void Awake()
  {
    weapon = Instantiate(weaponGameObject, transform).GetComponent<Weapon>();
    animator = GetComponent<Animator>();
    movement = GetComponent<Movement>();
  }

  void FixedUpdate()
  {
    Walk();
    Attack();
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
