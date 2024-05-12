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
	/// The number of seconds that shall elapse between attempts to roll the event.
	/// </summary>
	[Property] public float RollRate { get; set; } = 5f;
	/// <summary>
	/// Determines whether the event may run again after it has already started.
	/// </summary>
	[Property] public bool Recurring { get; set; } = true;

	private RealTimeUntil _untilNextRoll;

	protected override void OnStart()
	{
		_untilNextRoll = RollRate;
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
		if ( 0 != Game.Random.Int( Rarity ) )
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
