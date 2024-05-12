namespace Ducc;

public class RandomLightController : Component
{
	[Property] public LightBulb Light { get; set; }
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
			if ( !Light.LightEnabled && 1 >= Game.Random.Int( 30 ) )
			{
				Light.TurnOn();
			}
			else if ( Light.LightEnabled && 1 >= Game.Random.Int( 5 ) )
			{
				Light.TurnOff();
			}
			else if ( Flicker.Enabled && 1 >= Game.Random.Int( 10 ) )
			{
				Flicker.Enabled = !Flicker.Enabled;
			}
			else if ( !Flicker.Enabled && 1 >= Game.Random.Int( 30 ) )
			{
				Flicker.Enabled = !Flicker.Enabled;
			}
			_untilNextEventTick = 5f;
		}
	}

}
