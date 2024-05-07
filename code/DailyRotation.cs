using System;

namespace Ducc;

public sealed class DailyRotation : Component
{
	[Property] public Vector3 RotationAxis { get; set; }
	[Property] public Angles BaseRotation { get; set; }
	
	protected override void OnUpdate()
	{
		Transform.Rotation = BaseRotation * Rotation.FromAxis( RotationAxis, ClockSystem.DayProgress * 360f + 90f );
	}
}
