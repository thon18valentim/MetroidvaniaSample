using System.Collections.Generic;

public static class PlayerProfile
{
	public static AbilityManager AbilityManager { get; private set; }
	
	static PlayerProfile()
	{
		
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
}