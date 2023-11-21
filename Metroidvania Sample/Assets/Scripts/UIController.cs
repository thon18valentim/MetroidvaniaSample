using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController instance;

	public Slider playerHealthBar;

	public Image fadeImg;
	public float fadeSpeed = 2f;
	private bool isFadingToBlack, isFadingFromBlack;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		CreateHealthBar();
	}

	void Update()
	{
		HandleFadeEffect();
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

	private void HandleFadeEffect()
	{
		if (isFadingToBlack)
		{
			fadeImg.color = new Color(
				fadeImg.color.r, 
				fadeImg.color.g, 
				fadeImg.color.b, 
				Mathf.MoveTowards(fadeImg.color.a, 1f, fadeSpeed * Time.deltaTime));

			if (fadeImg.color.a == 1f)
			{
				isFadingToBlack = false;
			}
		}
		else if (isFadingFromBlack)
		{
			fadeImg.color = new Color(
				fadeImg.color.r,
				fadeImg.color.g,
				fadeImg.color.b,
				Mathf.MoveTowards(fadeImg.color.a, 0f, fadeSpeed * Time.deltaTime));

			if (fadeImg.color.a == 0f)
			{
				isFadingFromBlack = false;
			}
		}
	}

	public void StartFadeToBlack()
	{
		isFadingToBlack = true;
		isFadingFromBlack = false;
	}

	public void StartFadeFromBlack()
	{
		isFadingToBlack = false;
		isFadingFromBlack = true;
	}
}
