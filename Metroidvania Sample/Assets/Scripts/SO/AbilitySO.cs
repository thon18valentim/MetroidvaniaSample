using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/General/Ability")]
public class AbilitySO : ScriptableObject, IAbility
{
	[SerializeField] private string _name;
	[SerializeField] private string _description;
	[SerializeField] private AbilitiyType _type;

	public string Name { get => _name; set => _name = value; }
	public string Description { get => _description; set => _description = value; }
	public AbilitiyType Type { get => _type; set => _type = value; }
}
