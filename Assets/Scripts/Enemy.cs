using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
  private Movement movement;
  private Health health;
  private Slider healthSlider;

  private float nextChangeDirectionTime = 0.0f;
  private Vector2 movementDirection;

  void Awake()
  {
    health = GetComponent<Health>();
    healthSlider = GetHealthSlider();

    movement = GetComponent<Movement>();
    movementDirection = GetRandomDirection();
    health.OnDied += () => Destroy(this.gameObject);
    health.OnTakeDamage += OnTakeDamage;
  }

  void Start()
  {
    healthSlider.value = health.maxHP;
    healthSlider.maxValue = health.maxHP;
  }

  void Update()
  {
    Walk();
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.tag == "Projectile") {
      health.TakeDamage(1);
      Destroy(collider.gameObject);
    }
  }

  Slider GetHealthSlider()
  {
    var slider = Instantiate(Resources.Load("FloatingHealthCanvas"), transform) as GameObject;

    return slider.transform.Find("Slider").gameObject.GetComponent<Slider>();
  }

  void OnTakeDamage(float newHealth)
  {
    healthSlider.value = newHealth;
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
