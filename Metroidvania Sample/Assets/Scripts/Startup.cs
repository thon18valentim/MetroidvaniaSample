using UnityEngine;

public class Startup : MonoBehaviour
{
	public AbilitiesListSO abilitiesListSO;
	public GameObject player;

	public static PlayerController playerController;

	void Awake()
	{
		PlayerProfile.SetAbilities(abilitiesListSO);
		playerController = player.GetComponent<PlayerController>();
	}

	void Start()
	{
		
	}
}
