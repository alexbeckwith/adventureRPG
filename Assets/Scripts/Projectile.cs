using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Projectile : MonoBehaviour
{
  [SerializeField]
  private float lifetime = 2.0f;

  private Movement movement;
  private Vector2 velocity = new Vector2(0, 0);

  public void Awake()
  {
    movement = GetComponent<Movement>();
  }

  public void FixedUpdate()
  {
    if (velocity.magnitude > 0) {
      movement.Move(velocity);
    }
  }

  public void Shoot(Vector2 direction)
  {
    velocity = direction;
    movement.Move(velocity * 2);
    Destroy(gameObject, lifetime);
  }
}
