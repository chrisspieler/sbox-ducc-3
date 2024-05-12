namespace Ducc;

public class RandomLightFlicker : Component
{
	[Property] public LightBulb Light { get; set; }

	private bool _flickerSeriesActive;
	private int _flickerSeriesCount = 0;
	private RealTimeUntil _untilNextFlicker;
	private RealTimeUntil _untilNextSeries;

	protected override void OnStart()
	{
		_untilNextSeries = Game.Random.Float( 1f, 5f );
	}


	protected override void OnUpdate()
	{
		if ( _flickerSeriesActive )
		{
			UpdateFlickerSeries();
		}
		else
		{
			UpdateIdle();
		}

		
	}
	private void UpdateFlickerSeries()
	{
		if ( _untilNextFlicker )
		{
			_flickerSeriesCount--;
			Light.Flicker();
			_untilNextFlicker = Game.Random.Float( 0.025f, 0.4f );
			if ( _flickerSeriesCount <= 0 )
			{
				_flickerSeriesActive = false;
				_untilNextSeries = Game.Random.Float( 1f, 5f );
			}
		}
	}

	private void UpdateIdle()
	{
		if ( _untilNextSeries )
		{
			_flickerSeriesActive = true;
			_flickerSeriesCount = Game.Random.Int( 1, 4 );
		}
	}
}
