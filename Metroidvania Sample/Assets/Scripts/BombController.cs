using UnityEngine;

public class BombController : MonoBehaviour
{
	public float detonationCounter = .5f;
	public GameObject explosion;

	public float blastRange;
	public LayerMask destructibleMask;

	void Update()
	{
		Detonation();
	}

	// Handle detonation countdown and scenario destruction
	private void Detonation()
	{
		detonationCounter -= Time.deltaTime;
		if (detonationCounter <= 0)
		{
			if (explosion != null)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}

			Destroy(gameObject);

			var destructibleItems = Physics2D.OverlapCircleAll(transform.position, blastRange, destructibleMask);
			if (destructibleItems.Length > 0)
			{
				foreach (var col in destructibleItems)
				{
					Destroy(col.gameObject);
				}
			}
		}
	}
}
