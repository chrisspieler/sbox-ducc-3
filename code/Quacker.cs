namespace Ducc;

public sealed class Quacker : Component
{
	[Property] public SoundPointComponent SoundPoint { get; set; }

	protected override void OnUpdate()
	{
		if ( Input.Pressed( "attack1" ) )
		{
			DoQuack();
		}
	}

	public bool CanQuack()
	{
		return true;
	}

	public void DoQuack()
	{
		if ( !CanQuack() )
			return;

		Sandbox.Services.Stats.Increment( "quacks", 1 );
		SoundPoint.StopSound();
		SoundPoint.StartSound();
	}
}
