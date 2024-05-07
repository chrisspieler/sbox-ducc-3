namespace Ducc;

public sealed class Quacker : Component
{
	[Property] public SoundPointComponent SoundPoint { get; set; }

	protected override void OnUpdate()
	{
		if ( Input.Pressed( "attack1" ) )
		{
			SoundPoint.StopSound();
			SoundPoint.StartSound();
		}
	}
}
