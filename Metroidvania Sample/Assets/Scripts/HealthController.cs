using UnityEngine;

public class HealthController : MonoBehaviour
{
	private Rigidbody2D rb;

	[Header("Health")]
	public int totalHealt;
	private int health;
	public bool isPlayer = false;

	[Header("Effects gameObject")]
	public GameObject deathEffect;

	[Header("Invicibility")]
	public float invicibilityLenght;
	private float invicibilityCounter = 0;

	[Header("Damage flash")]
	public float flashLenght;
	private float flashCounter = 0;

	[Header("Character Sprites for flashing effect")]
	public SpriteRenderer[] characterSprites;

	[Header("How far the character is knock back after take damage")]
	public float verticalKnockBackForce;
	public float horizontalKnockBackForce;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		if (isPlayer)
			totalHealt = PlayerProfile.PlayerMaxLife;
		health = totalHealt;
	}

	void Update()
	{
		PlayInvincibleEffect();
	}

	// Handle damage taken
	public void TakeDamage(int _damage, Transform _aggressor, string _tag = "Enemy")
	{
		if (invicibilityCounter > 0)
			return;

		health -= _damage;

		UpdateHealthBar(_tag);

		if (health <= 0)
		{
			PlayDeathEffect();
			Kill(_tag);
		}
		else
		{
			if (_tag == "Player")
			{
				invicibilityCounter = invicibilityLenght;
				KnockBack(_aggressor);
			}
		}
	}

	// Instantly kill the character
	public void TakeFullDamage(string _tag = "Enemy")
	{
		health = 0;

		UpdateHealthBar(_tag);
		PlayDeathEffect();
		Kill(_tag);
	}

	// Play the obj death effect
	private void PlayDeathEffect()
	{
		if (deathEffect != null)
		{
			Instantiate(deathEffect, transform.position, transform.rotation);
		}
	}

	// Play invincible effect
	private void PlayInvincibleEffect()
	{
		if (invicibilityCounter > 0)
		{
			invicibilityCounter -= Time.deltaTime;
			flashCounter -= Time.deltaTime;

			if (flashCounter <= 0)
			{
				foreach (var sprite in characterSprites)
				{
					sprite.enabled = !sprite.enabled;
				}

				flashCounter = flashLenght;
			}

			if (invicibilityCounter <= 0)
			{
				foreach (var sprite in characterSprites)
				{
					sprite.enabled = true;
				}

				flashCounter = 0;

				if (isPlayer)
					Startup.playerController.DisableKnockBack();
			}
		}
	}

	// Destroy or deactivate the obj
	private void Kill(string _tag)
	{
		if (_tag == "Enemy")
		{
			Destroy(gameObject);
		}
		else
		{
			Startup.sceneController.PlayerRespawn();
		} // Player
	}

	// Exports current obj health amount
	public int GetHealth()
	{
		return health;
	}

	// Handle Health bar
	private void UpdateHealthBar(string _tag)
	{
		if (_tag == "Player")
		{
			UIController.instance.UpdateHealthBar(health);
		}
	}

	// Handle character's knock back after take damage
	private void KnockBack(Transform enemy)
	{
		if (isPlayer)
			Startup.playerController.EnableKnockBack();

		rb.AddForce(Vector2.up * verticalKnockBackForce, ForceMode2D.Impulse);

		if (transform.position.x < enemy.position.x)
		{
			rb.AddForce(Vector2.left * horizontalKnockBackForce, ForceMode2D.Impulse);
		}
		else
		{
			rb.AddForce(Vector2.right * horizontalKnockBackForce, ForceMode2D.Impulse);
		}
	}

	// Fill characters' health
	public void FillHealth()
	{
		if (isPlayer)
		{
			totalHealt = PlayerProfile.PlayerMaxLife;
			UIController.instance.UpdateHealthBar(totalHealt);
		}

		health = totalHealt;
	}

	// Handle objects healing
	public void Heal(int healAmount, string _tag = "Player")
	{
		health += healAmount;
		if (health > totalHealt)
			healAmount = totalHealt;

		UpdateHealthBar(_tag);
	}
}
