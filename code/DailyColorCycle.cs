﻿namespace Ducc;

public sealed class DailyColorCycle : Component
{
	[Property] public Component Tintable { get; set; }
	[Property] public Gradient MainGradient { get; set; }
	[Property] public Gradient SecondaryGradient { get; set; }

	protected override void OnUpdate()
	{
		if ( !Tintable.IsValid() )
			return;

		if ( Tintable is Light light )
		{
			light.LightColor = MainGradient.Evaluate( ClockSystem.DayProgress );
		}
		if ( Tintable is DirectionalLight sunLight )
		{
			sunLight.SkyColor = SecondaryGradient.Evaluate( ClockSystem.DayProgress );
		}
		if ( Tintable is SkyBox2D skybox )
		{
			skybox.Tint = MainGradient.Evaluate( ClockSystem.DayProgress );
		}
	}
}