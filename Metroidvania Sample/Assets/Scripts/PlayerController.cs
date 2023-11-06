using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	public Animator anim;

	public float moveSpeeed;
	public float jumpForce;

	public Transform groundCheckPoint;
	private bool isJumping;
	public LayerMask groundLayer;

	public BulletControler bullet;
	public Transform shotCheckPoint;

	private bool canDoubleJump;

	public float dashSpeed, dashTime;
	public float dashCooldown;
	private float dashCounter;
	private float dashRechargeCounter;

	public SpriteRenderer sr;
	public SpriteRenderer afterEffectImage;
	public float afterEffectLifetime, timeBetweenAfterEffect;
	private float afterEffectCounter;
	public Color afterEffectColor;

	public GameObject standing, ball;
	public float ballCooldown;
	private float ballCounter;
	public Animator ballAnim;

	public Transform bombPosition;
	public GameObject bomb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Dash();
		Move();
		Flip();
		Jump();
		Shot();
		BallMode();
	}

	// Move action according to speed
	private void Move()
	{
		// Cannot move during a dash
		if (dashCounter > 0)
			return;

		rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeeed, rb.velocity.y);
		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
	}

	// Handle dash action
	private void Dash()
	{
		if (dashRechargeCounter > 0)
		{
			dashRechargeCounter -= Time.deltaTime;
		}
		else
		{
			// Can only dash in standing mode
			if (Input.GetButtonDown("Fire2") && standing.activeSelf && PlayerProfile.AbilityManager.IsAbilityUnlocked(AbilitiyType.Dash))
			{
				dashCounter = dashTime;
				ShowAfterEffect();
			}
		}

		if (dashCounter > 0)
		{
			dashCounter -= Time.deltaTime;
			rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

			afterEffectCounter -= Time.deltaTime;
			if (afterEffectCounter <= 0)
			{
				ShowAfterEffect();
				afterEffectCounter = timeBetweenAfterEffect;
			}

			dashRechargeCounter = dashCooldown;
		}
	}

	// Jump action and ground detecting
	private void Jump()
	{
		isJumping = !Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);

		if (Input.GetButtonDown("Jump") && (!isJumping || (canDoubleJump && PlayerProfile.AbilityManager.IsAbilityUnlocked(AbilitiyType.DoubleJump))))
		{
			if (!isJumping)
			{
				canDoubleJump = true;
			}
			else
			{
				canDoubleJump = false;
				anim.SetTrigger("doubleJump");
			}

			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			isJumping = true;
		}

		anim.SetBool("isJumping", isJumping);
	}

	// Flip this object to the right direction, which the player is looking
	private void Flip()
	{
		// Movement in left direction
		if (rb.velocity.x < 0)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		// Movement in right direction
		if (rb.velocity.x > 0)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	// Shots a bullet in the direction which the player is looking
	private void Shot()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			if (standing.activeSelf)
			{
				var _bullet = Instantiate(bullet, shotCheckPoint.position, shotCheckPoint.rotation);
				_bullet.moveDir = new Vector2(transform.localScale.x, 0f);

				anim.SetTrigger("shotFired");
			}
			else
			{
				DroppingBombs();
			}
		}
	}

	// Handle bomb throwing
	private void DroppingBombs()
	{
		if (!ball.activeSelf || !PlayerProfile.AbilityManager.IsAbilityUnlocked(AbilitiyType.DropBomb))
			return;

		Instantiate(bomb, bombPosition.position, bombPosition.rotation);
	}

	// Show After Effect according to settings
	private void ShowAfterEffect()
	{
		var effect = Instantiate(afterEffectImage, transform.position, transform.rotation);
		effect.sprite = sr.sprite;
		effect.transform.localScale = transform.localScale;
		effect.color = afterEffectColor;

		Destroy(effect.gameObject, afterEffectLifetime);
	}

	// Handle ball mode
	private void BallMode()
	{
		if (!ball.activeSelf)
		{
			if (Input.GetAxisRaw("Vertical") < -.9f && PlayerProfile.AbilityManager.IsAbilityUnlocked(AbilitiyType.BecomeBall))
			{
				ballCounter -= Time.deltaTime;
				if (ballCounter <= 0)
				{
					ball.SetActive(true);
					standing.SetActive(false);
				}
			}
			else
			{
				ballCounter = ballCooldown;
			}
		}
		else
		{
			if (Input.GetAxisRaw("Vertical") > .9f)
			{
				ballCounter -= Time.deltaTime;
				if (ballCounter <= 0)
				{
					ball.SetActive(false);
					standing.SetActive(true);
				}
			}
			else
			{
				ballCounter = ballCooldown;
			}

			ballAnim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
		}
	}
}
