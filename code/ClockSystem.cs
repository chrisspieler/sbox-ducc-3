﻿using System;

namespace Ducc;

public class ClockSystem : GameObjectSystem
{
	[ConVar( "clock_debug" )]
	public static bool Debug { get; set; }
	[ConVar( "clock_timescale")]
	public static float TimeScale { get; set; } = 100f;
	public DateTime CurrentTime { get; private set; }
	public float DayProgress
	{
		get
		{
			var totalSeconds = CurrentTime.Second
				+ CurrentTime.Minute * 60f
				+ CurrentTime.Hour * 60f * 60f;
			return totalSeconds / SECONDS_PER_DAY;
		}
	}

	private const int SECONDS_PER_DAY = 60 * 60 * 24;

	public ClockSystem( Scene scene ) : base( scene )
	{
		CurrentTime = new DateTime( 2024, 05, 07, 10, 00, 00 );
		Listen( Stage.UpdateBones, 0, Tick, "Clock System Tick" );
	}

	private void Tick()
	{
		CurrentTime = CurrentTime.AddSeconds( Time.Delta * TimeScale );
		if ( Debug )
		{
			Gizmo.Draw.ScreenText( $"Time: {CurrentTime}, Progress: {DayProgress}, TimeScale: {TimeScale}", new Vector2( Screen.Width / 2, 0f ), "Consolas", 12, TextFlag.CenterTop );
		}
	}

	[ConCmd("clock_settime")]
	public static void SetTime( float progress )
	{
		var scene = Game.ActiveScene;
		if ( scene is null )
			return;

		var clock = scene.GetSystem<ClockSystem>();
		var currentTime = clock.CurrentTime;
		clock.CurrentTime = new DateTime( currentTime.Year, currentTime.Month, currentTime.Day );
		var addedSeconds = TimeSpan.FromSeconds( (int)(SECONDS_PER_DAY * progress));
		clock.CurrentTime += addedSeconds;
	}
}
