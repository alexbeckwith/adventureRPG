using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
  [SerializeField]
  private GameObject projectile;
  [SerializeField]
  private float cooldown = 0.65f;

  private float cooldownEnd = 0.0f;
  
  override public bool Attack(int direction)
  {
    if (Time.time < cooldownEnd) {
      return false;
    }

    Vector3 lookRotation = new Vector3(0, 0, 0);
    Vector2 velocity = new Vector2(0, 0);

    switch (direction) {
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

    cooldownEnd = Time.time + cooldown;

    var projectileInstance = Instantiate(
      projectile, 
      transform.position, 
      Quaternion.Euler(lookRotation)
    );

    projectileInstance.GetComponent<Projectile>().Shoot(velocity);

    return true;
  }
}
