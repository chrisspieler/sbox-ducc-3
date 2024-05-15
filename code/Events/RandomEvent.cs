using System;

namespace Ducc;

public class RandomEvent : Component
{
	/// <summary>
	/// Invoked when an event is successfully rolled. The argument is this object.
	/// </summary>
	[Property] public Action<RandomEvent> OnEventStart { get; set; }
	/// <summary>
	/// At each interval of RollRate, this delegate will be invoked. If true or null,
	/// RollEvent will be called.
	/// </summary>
	[Property] public Func<bool> EventStartPredicate { get; set; }
	/// <summary>
	/// When rolling to determine if the event will run, Rarity will be the max value of the 
	/// integer number rolled. If a 0 is rolled, the event will run. For example, if 
	/// Rarity is 1, there is a 50% chance of the event running. If Rarity is 9, there is
	/// a 10% chance.
	/// </summary>
	[Property] public int Rarity { get; set; } = 100;
	/// <summary>
	/// The likelihood of an event occurring will be scaled by the current <see cref="ClockSystem.DayProgress"/>
	/// as evaluated by this curve.
	/// </summary>
	[Property] public Curve TimeCurve { get; set; }
	/// <summary>
	/// The rarity that will be used given the current <see cref="ClockSystem.DayProgress"/> and <see cref="TimeCurve"/>.
	/// </summary>
	[Property]
	public int EffectiveRarity
	{
		get
		{
			if ( !Game.IsPlaying )
			{
				return Rarity;
			}
			_clock ??= Scene.GetSystem<ClockSystem>();
			return (int)(Rarity * TimeCurve.Evaluate( _clock.DayProgress ));
		}
	}
	/// <summary>
	/// The number of seconds that shall elapse between attempts to roll the event.
	/// </summary>
	[Property] public float RollRate { get; set; } = 5f;
	/// <summary>
	/// Determines whether the event may run again after it has already started.
	/// </summary>
	[Property] public bool Recurring { get; set; } = true;

	private RealTimeUntil _untilNextRoll;
	private ClockSystem _clock;

	protected override void OnStart()
	{
		_untilNextRoll = RollRate;
		_clock = Scene.GetSystem<ClockSystem>();
	}

	protected override void OnUpdate()
	{
		if ( _untilNextRoll && ( EventStartPredicate is null || EventStartPredicate() ) )
		{
			RollEvent();
			_untilNextRoll = RollRate;
		}
	}

	[Button("Roll Event")]
	private void RollEvent()
	{
		var rarity = Rarity;
		// The curve is meaningless if it has only one point.
		if ( TimeCurve.Frames.Count > 1 )
		{
			// Use the curve to make the event more or less likely at different times.
			rarity = EffectiveRarity;
		}
		if ( 0 != Game.Random.Int( rarity ) )
			return;

		StartEvent();
	}

	[Button( "Start Event" )]
	private void StartEvent()
	{
		OnEventStart?.Invoke( this );
		if ( !Recurring )
		{
			Enabled = false;
		}
	}
}
