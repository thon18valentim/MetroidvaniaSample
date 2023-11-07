using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController instance;

	public Slider playerHealthBar;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		CreateHealthBar();
	}

	public void CreateHealthBar()
	{
		var currentPlayerTotalHealth = PlayerProfile.PlayerMaxLife;

		playerHealthBar.maxValue = currentPlayerTotalHealth;
		playerHealthBar.value = Startup.playerController.GetCurrentHealth();

		var barWidth = currentPlayerTotalHealth * 3;
		if (barWidth > 800)
			barWidth = 800;

		var barReactTransform = playerHealthBar.gameObject.GetComponent<RectTransform>();
		barReactTransform.sizeDelta = new Vector2(barWidth, 70);
	}

	public void UpdateHealthBar(int health)
	{
		playerHealthBar.value = health;
	}
}
