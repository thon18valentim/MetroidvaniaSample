using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;

	public float moveSpeeed;
	public float jumpForce;

	public Transform groundCheckPoint;
	private bool isJumping;
	public LayerMask groundLayer;

	public BulletControler bullet;
	public Transform shotCheckPoint;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = transform.GetChild(0).GetComponent<Animator>();
	}

	void Update()
	{
		Move();
		Flip();
		Jump();
		Shot();
	}

	// Move action according to speed
	private void Move()
	{
		rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeeed, rb.velocity.y);
		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
	}

	// Jump action and ground detecting
	private void Jump()
	{
		isJumping = !Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);

		if (Input.GetButtonDown("Jump") && !isJumping)
		{
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

	private void Shot()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var _bullet = Instantiate(bullet, shotCheckPoint.position, shotCheckPoint.rotation);
			_bullet.moveDir = new Vector2(transform.localScale.x, 0f);
		}
	}
}
