using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{
  private Movement movement;

  private float nextChangeDirectionTime = 0.0f;
  private Vector2 movementDirection;

  void Awake()
  {
    movement = GetComponent<Movement>();
    movementDirection = GetRandomDirection();
  }

  void FixedUpdate()
  {
    Walk();
  }

  void Walk()
  {
    if (ShouldChangeDirection()) {
        nextChangeDirectionTime += GetNextChangeDirectionTime();
        movementDirection = GetRandomDirection();
    }

    movement.Move(movementDirection);
  }

  bool ShouldChangeDirection()
  {
    return Time.time > nextChangeDirectionTime;
  }

  float GetNextChangeDirectionTime()
  {
      return Random.Range(1.0f, 2.0f);
  }

  Vector2 GetRandomDirection()
  {
      return new Vector2(
          Mathf.Round(Random.Range(-1.0f, 1.0f)),
          Mathf.Round(Random.Range(-1.0f, 1.0f))
      );
  }
}
