using UnityEngine;

public class CameraController : MonoBehaviour
{
	private PlayerController playerController;
	public BoxCollider2D boundsBox;

	private float halfHeight, halfWidth;

	private void Awake()
	{
		halfHeight = Camera.main.orthographicSize;
		halfWidth = halfHeight * Camera.main.aspect;
	}

	void Start()
	{
		playerController = Startup.playerController;
	}

	void Update()
	{
		Move();
	}

	// Moves the camera according to the player position and scene bounds
	private void Move()
	{
		if (playerController == null)
		{
			Debug.LogWarning("Player reference NOT FOUND");
			return;
		}

		var playerPosition = playerController.transform.position;
		var maxBounds = boundsBox.bounds.max;
		var minBounds = boundsBox.bounds.min;

		transform.position = new Vector3(
			Mathf.Clamp(playerPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth),
			Mathf.Clamp(playerPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight), 
			transform.position.z);
	}
}
