using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Lists/AbilitiesList")]
public class AbilitiesListSO : ScriptableObject
{
	public List<AbilitySO> abilities;
}
