using System.Xml.Schema;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
	public Transform[] patrolPoints;
	private int currentPatrolPoint;

	public float moveSpeed, jumpForce;

	public float patrolPointCooldown;
	private float patrolPointCounter;

	private Rigidbody2D rb;
	public Animator anim;

	public GameObject wallDetector;
	private bool isTouchingWall;

	public int damage;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		patrolPointCounter = patrolPointCooldown;

		// Separeting patrol points from character obj
		foreach (var pPoint in patrolPoints)
		{
			pPoint.SetParent(null);
		}
	}

	void Update()
	{
		Patrol();
	}

	// Handle character patrol action
	private void Patrol()
	{
		var distance = Mathf.Abs(transform.position.x - patrolPoints[currentPatrolPoint].position.x);
		if (distance > .2f)
		{
			// Go to the next patrol checkpoint
			if (transform.position.x < patrolPoints[currentPatrolPoint].position.x)
			{
				rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
				transform.localScale = new Vector3(1f, 1f, 1f);
			}

			// Jump if finds any wall in the way
			if (isTouchingWall)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
		}
		else
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);

			// wait in this patrol checkpoint
			patrolPointCounter -= Time.deltaTime;
			if (patrolPointCounter <= 0)
			{
				patrolPointCounter = patrolPointCooldown;

				currentPatrolPoint++;
				if (currentPatrolPoint >= patrolPoints.Length)
				{
					currentPatrolPoint = 0;
				}
			}
		}

		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<HealthController>().TakeDamage(damage, "Player");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Ground")
		{
			isTouchingWall = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Ground")
		{
			isTouchingWall = false;
		}
	}
}
