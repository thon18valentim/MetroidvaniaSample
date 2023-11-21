using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int id;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			PlayerProfile.SetCurrentCheckPoint(transform.position);
			PlayerProfile.AdvanceCheckpoint(id);
		}
	}
}
