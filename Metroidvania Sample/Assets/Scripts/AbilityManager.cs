using System;
using System.Collections.Generic;
using System.Linq;

public class AbilityManager
{
	private readonly List<Ability> _abilities;

	public AbilityManager(List<Ability> abilities)
	{
		_abilities = abilities;
	}

	private Ability GetAbility(AbilitiyType abilitiyType)
	{
		var ability = _abilities.FirstOrDefault(ab => ab.Type == abilitiyType);
		return ability ?? throw new Exception("Ability NOT FOUND in <Player Profile>");
	}

	public string GetAbilityName(AbilitiyType abilitiyType)
	{
		var ability = GetAbility(abilitiyType);
		return ability.Name;
	}

	public void UnlockAbility(AbilitiyType abilitiyType)
	{
		var ability = GetAbility(abilitiyType);
		ability.Unlocked = true;
	}

	public bool IsAbilityUnlocked(AbilitiyType abilitiyType)
	{
		var ability = GetAbility(abilitiyType);
		return ability.Unlocked;
	}
}
