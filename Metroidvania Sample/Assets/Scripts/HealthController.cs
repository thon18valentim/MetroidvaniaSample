using UnityEngine;

public class HealthController : MonoBehaviour
{
	public int totalHealt = 100;
	private int health;

	public GameObject deathEffect;

	private void Start()
	{
		health = totalHealt;
	}

	// Handle damage taken
	public void TakeDamage(int damage, string tag = "Enemy")
	{
		health -= damage;
		if (health <= 0)
		{
			if (deathEffect != null)
			{
				Instantiate(deathEffect, transform.position, transform.rotation);
			}

			if (tag == "Enemy")
			{
				Destroy(gameObject);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}
}
