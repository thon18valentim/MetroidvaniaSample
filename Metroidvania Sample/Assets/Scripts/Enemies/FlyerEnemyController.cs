using UnityEngine;

public class FlyerEnemyController : MonoBehaviour
{
	private Rigidbody2D rb;
	public EnemyType enemyType;

	public float rangeToStartChase;
	private bool isChasing;

	public float moveSpeed, turnSpeed;
	public int damage;
	private Transform player;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		player = Startup.playerController.transform;
	}

	void Update()
	{
		Chase();
	}

	// Handle enemy chase action based on player distance
	private void Chase()
	{
		if (!isChasing)
		{
			// Checking if the player is near enough to the enemy start to chasing him
			if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
			{
				isChasing = true;
			}
		}
		else
		{
			if (player.gameObject.activeSelf)
			{
				Vector3 direction = transform.position - player.position;
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

				transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
			}

			transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<HealthController>().TakeDamage(damage, "Player");
		}
	}
}
