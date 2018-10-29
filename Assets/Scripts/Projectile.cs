using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Projectile : MonoBehaviour 
{
	public GameObject projectile;

	protected Rigidbody2D rb2d;

	protected Vector2 position;
	protected Vector3 rotation;

  protected float speed = 7.0f;
  protected float lifetime = 2.0f;
  protected float cooldown = 0.66f;
  protected float lastFireTime = 0.0f; 

	public Projectile(Vector2 _position, Vector3 _rotation)
	{
		position = _position;
		rotation = _rotation;
	}

	public void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	public void Fire()
	{
    var projectileInstance = Instantiate(
			projectile, 
			transform.position, 
			Quaternion.Euler(rotation)
		);
    projectileInstance.GetComponent<Rigidbody2D>().velocity = velocity * speed;

    Destroy(projectileInstance, lifetime);
	}
}
