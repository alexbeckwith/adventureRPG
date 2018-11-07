using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField]
  private float maxHP;

  private float currentHP;
  public event Action OnDied;
  public event Action<float> OnTakeDamage;

  void Awake()
  {
      currentHP = maxHP;
  }

  public void TakeDamage(float damage)
  {
    currentHP -= damage;

    if (currentHP <= 0) {
      Die();
    } else if (OnTakeDamage != null) {
      OnTakeDamage(currentHP);
    }
  }

  private void Die()
  {
    if (OnDied != null) {
      OnDied();
    }
  }
}
