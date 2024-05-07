namespace Ducc;

public sealed class RandomRotation : Component
{
	protected override void OnStart()
	{
		Transform.Rotation = Rotation.Random;
	}
}
