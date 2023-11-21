using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
	private Animator anim;

	public float distanceToOpen;
	private bool playerExiting;

	public Transform exitPoint;
	public float movePlayerSpeed;

	public InGameScene nextLevel;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		playerExiting = false;
	}

	private void Update()
	{
		CalculateDistance();
		ControllPlayerIfExiting();
	}

	private void CalculateDistance()
	{
		var distance = Vector3.Distance(transform.position, Startup.playerController.transform.position);
		if (distance < distanceToOpen)
		{
			anim.SetBool("doorOpen", true);
		}
		else
		{
			anim.SetBool("doorOpen", false);
		}
	}

	private void ControllPlayerIfExiting()
	{
		if (playerExiting)
		{
			var player = Startup.playerController;
			player.transform.position = 
				Vector3.MoveTowards(player.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			if (!playerExiting)
			{
				Startup.playerController.DisablePlayerMovement();
				StartCoroutine(UseDoor());
			}
		}
	}

	private IEnumerator UseDoor()
	{
		playerExiting = true;
		Startup.playerController.anim.enabled = false;

		UIController.instance.StartFadeToBlack();

		yield return new WaitForSeconds(1.5f);

		Startup.playerController.EnablePlayerMovement();
		Startup.playerController.anim.enabled = true;

		UIController.instance.StartFadeFromBlack();

		SceneManager.LoadScene((int)nextLevel);
	}
}
