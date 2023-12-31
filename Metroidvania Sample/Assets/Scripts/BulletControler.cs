using UnityEngine;

public class BulletControler : MonoBehaviour
{
	public float bulletSpeed;
	private Rigidbody2D rb;

	public Vector2 moveDir;

	public GameObject impactEffect;

	public int damage;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Move();
	}

	// Move the bullet
	private void Move()
	{
		rb.velocity = moveDir * bulletSpeed;
	}

	// Destroy the bullet obj when it hits something
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			collision.GetComponent<HealthController>().TakeDamage(damage, transform);
		}

		Instantiate(impactEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	// Destroy the bullet obj when it is no longer visible in the player's camera
	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
