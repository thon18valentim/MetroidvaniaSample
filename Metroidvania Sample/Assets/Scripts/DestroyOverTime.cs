using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
	public float lifeTime;

	void Start()
	{
		Destroy(gameObject, lifeTime);
	}
}
