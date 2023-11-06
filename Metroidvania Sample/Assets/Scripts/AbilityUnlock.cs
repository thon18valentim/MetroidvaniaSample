using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
	public AbilitiyType abilitiyType;
	public GameObject PickUpEffect;
	public TextMeshProUGUI pickUpMessage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			PlayerProfile.AbilityManager.UnlockAbility(abilitiyType);

			Instantiate(PickUpEffect, transform.position, transform.rotation);

			pickUpMessage.transform.parent.SetParent(null);
			pickUpMessage.transform.parent.position = transform.position;

			var _name = PlayerProfile.AbilityManager.GetAbilityName(abilitiyType);
			pickUpMessage.text = $"{_name} unlocked";

			pickUpMessage.gameObject.SetActive(true);

			Destroy(pickUpMessage.transform.parent.gameObject, 5f);
			Destroy(gameObject);
		}
	}
}
