using System;

namespace Ducc;

public sealed class DailyRotation : Component
{
	[Property] public Vector3 RotationAxis { get; set; }
	[Property] public Angles BaseRotation { get; set; }

	private ClockSystem _clock { get; set; }

	protected override void OnStart()
	{
		_clock = Scene.GetSystem<ClockSystem>();
	}

	protected override void OnUpdate()
	{
		Transform.Rotation = BaseRotation * Rotation.FromAxis( RotationAxis, _clock.DayProgress * 360f + 90f );
	}
}
