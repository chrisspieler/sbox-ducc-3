namespace Ducc;

public class SelfDestruct : Component
{
	[Property] public float Delay { get; set; }

	private RealTimeUntil _untilDestruction;

	protected override void OnStart()
	{
		_untilDestruction = Delay;
	}

	protected override void OnUpdate()
	{
		if ( _untilDestruction )
		{
			GameObject.Destroy();
		}
	}
}
