using UnityEngine;

public class Startup : MonoBehaviour
{
	public AbilitiesListSO abilitiesListSO;

	void Awake()
	{
		PlayerProfile.SetAbilities(abilitiesListSO);
	}

	void Start()
	{

	}
}
