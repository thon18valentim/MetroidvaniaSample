using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
	public float detonationCounter = .5f;
	public GameObject explosion;

	void Update()
	{
		detonationCounter -= Time.deltaTime;
		if (detonationCounter <= 0)
		{
			if (explosion != null)
			{
				Instantiate(explosion, transform.position, transform.rotation);
			}

			Destroy(gameObject);
		}
	}
}
