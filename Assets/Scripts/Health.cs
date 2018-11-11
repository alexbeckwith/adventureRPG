using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField]
  public float maxHP;

  public float currentHP { get; private set; }
  public event Action OnDied;
  public event Action<float> OnTakeDamage;

  void Awake()
  {
    currentHP = maxHP;
  }

  public void TakeDamage(float damage)
  {
    currentHP -= damage;

    switch (currentHP) {
      case float hp when hp > 0 && OnTakeDamage != null:
        OnTakeDamage(currentHP);
        break;
      case float hp when hp <= 0 && OnDied != null:
        OnDied();
        break;
      default:
        break;
    }
  }
}
