using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
	public AbilitiesListSO abilitiesListSO;
	public GameObject player;

	public float playerRespawnCooldown;

	public static Startup sceneController;
	public static PlayerController playerController;

	public Transform defaultCheckpoint;

	[SerializeField] private UIController uiController;

	void Awake()
	{
		PlayerProfile.SetAbilities(abilitiesListSO);
		playerController = player.GetComponent<PlayerController>();
		sceneController = this;

		SpawnUIController();
	}

	void Start()
	{
		RespawnPlayer();
	}

	// Define current respawn point
	private void RespawnPlayer()
	{
		if (PlayerProfile.CurrentCheckpointId > 0)
		{
			player.transform.position = PlayerProfile.CurrentCheckpoint;
			return;
		}

		player.transform.position = defaultCheckpoint.position;
		PlayerProfile.SetCurrentCheckPoint(defaultCheckpoint.position);
	}

	// Handle player's respawn
	public void PlayerRespawn()
	{
		StartCoroutine(Respaw(player, PlayerProfile.CurrentCheckpoint, playerRespawnCooldown));
	}

	private IEnumerator Respaw(GameObject respawnObj, Vector3 respawnPoint, float respawnCooldown)
	{
		// Deactivate object
		respawnObj.SetActive(false);

		// Wait to respawn
		yield return new WaitForSeconds(respawnCooldown);

		// Reload scene and respawn object
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//respawnObj.transform.position = respawnPoint;
		respawnObj.SetActive(true);

		// Restart UI
		playerController.healthController.FillHealth();
	}

	private void SpawnUIController()
	{
		var obj = FindObjectOfType<UIController>();
		if (obj == null)
		{
			Instantiate(uiController);
		}
	}
}
