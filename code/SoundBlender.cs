namespace Ducc;

public sealed class SoundBlender : Component
{
	[Property] public SoundPointComponent Sound1 { get; set; }
	[Property] public Curve Sound1Curve { get; set; }
	[Property] public SoundPointComponent Sound2 { get; set; }
	[Property] public Curve Sound2Curve { get; set; }
	protected override void OnUpdate()
	{
		var time = ClockSystem.DayProgress;
		if ( Sound1.IsValid() )
			Sound1.Volume = Sound1Curve.Evaluate( time );
		if ( Sound2.IsValid() )
			Sound2.Volume = Sound2Curve.Evaluate( time );
	}
}
