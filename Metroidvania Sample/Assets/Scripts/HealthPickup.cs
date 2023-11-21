using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	public bool onlyHealPlayer;
	public int healAmount;

	public GameObject healEffect;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		var healthController = collider.gameObject.GetComponentInParent<HealthController>();
		if (healthController != null)
		{
			if (onlyHealPlayer)
			{
				if (collider.gameObject.tag != "Player")
					return;
			}

			healthController.Heal(healAmount, collider.gameObject.tag);

			PlayHealEffect();
			Destroy(gameObject);
		}
	}

	// Play heal effect if it exists
	private void PlayHealEffect()
	{
		if (healEffect != null)
		{
			Instantiate(healEffect, transform.position, Quaternion.identity);
		}
	}
}
