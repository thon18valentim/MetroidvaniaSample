using System.Collections.Generic;
using UnityEngine;

public static class PlayerProfile
{
	public static int PlayerMaxLife { get; private set; }
	public static AbilityManager AbilityManager { get; private set; }
	public static Vector3 CurrentCheckpoint { get; private set; }
	public static int CurrentCheckpointId { get; private set; }
	
	static PlayerProfile()
	{
		PlayerMaxLife = 100;
		CurrentCheckpointId = 0;
	}

	public static void SetAbilities(AbilitiesListSO abilitiesListSO)
	{
		var abilities = new List<Ability>();

		abilitiesListSO.abilities.ForEach(ab =>
		{
			var ability = new Ability
			{
				Name = ab.Name,
				Description = ab.Description,
				Type = ab.Type,
				Unlocked = false
			};

			abilities.Add(ability);
		});

		AbilityManager = new AbilityManager(abilities);
	}

	public static void SetCurrentCheckPoint(Vector3 position)
	{
		CurrentCheckpoint = position;
	}

	public static void AdvanceCheckpoint(int checkpointId)
	{
		CurrentCheckpointId = checkpointId;
	}

	public static void ResetCheckpoint()
	{
		CurrentCheckpointId = 0;
		CurrentCheckpoint = Vector3.zero;
	}
}
