
public class Ability : IAbility
{
	public string Name { get; set; }
	public string Description { get; set; }
	public AbilitiyType Type { get; set; }
	public bool Unlocked { get; set; }
}

public interface IAbility
{
	public string Name { get; set; }
	public string Description { get; set; }
	public AbilitiyType Type { get; set; }
}

public enum AbilitiyType
{
	DoubleJump,
	Dash,
	BecomeBall,
	DropBomb,
}
