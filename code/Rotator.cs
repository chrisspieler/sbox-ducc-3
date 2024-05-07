namespace Ducc;

public sealed class Rotator : Component
{
	[Property] public Angles DegreesPerSecond { get; set; }
	protected override void OnUpdate()
	{
		Transform.Rotation *= DegreesPerSecond * Time.Delta;
	}
}
