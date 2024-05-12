namespace Ducc;

public class RandomLightController : Component
{
	[Property] public LightBulb Light { get; set; }
	[Property] public int LightToggleStartValue { get; set; } = 500;
	[Property] public int LightToggleEndValue { get; set; } = 10;
	[Property] public int FlickerToggleStartValue { get; set; } = 30;
	[Property] public int FlickerToggleEndValue { get; set; } = 10;
	[Property] public RandomLightFlicker Flicker { get; set; }

	private RealTimeUntil _untilNextEventTick;

	protected override void OnStart()
	{
		_untilNextEventTick = 5f;
	}

	protected override void OnUpdate()
	{
		if ( _untilNextEventTick )
		{
			Log.Info( "Event Tick" );
			RollLightToggle();
			RollFlickerToggle();
			_untilNextEventTick = 5f;
		}
	}

	private void RollLightToggle()
	{
		if ( !Light.LightEnabled && 0 == Game.Random.Int( LightToggleStartValue ) )
		{
			Light.TurnOn();
		}
		else if ( Light.LightEnabled && 0 == Game.Random.Int( LightToggleEndValue ) )
		{
			Light.TurnOff();
		}
	}

	private void RollFlickerToggle()
	{
		if ( Flicker.Enabled && 0 == Game.Random.Int( FlickerToggleEndValue ) )
		{
			Flicker.Enabled = !Flicker.Enabled;
		}
		else if ( !Flicker.Enabled && 0 == Game.Random.Int( FlickerToggleStartValue ) )
		{
			Flicker.Enabled = !Flicker.Enabled;
		}
	}

	[Button("Test RNG")]
	private void PrintRngSample()
	{
		for ( int i = 0; i < 100; i++ )
		{
			Log.Info( Game.Random.Int( 250 ) );
		}
	}
}
