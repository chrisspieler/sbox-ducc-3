namespace Ducc;

public class PropLauncher : Component
{
	[Property] public GameObject PropPrefab { get; set; }
	[Property] public Vector3 VelocityDirection { get; set; }
	[Property] public RangedFloat VelocityScale = 300f;
	[Property] public float? AngleJitter { get; set; }
	[Property] public float? AngularVelocityScale { get; set; }
	[Property] public float? SelfDestructTime { get; set; }

	[Button("Launch")]
	public void Launch()
	{
		if ( !Game.IsPlaying )
			return;

		if ( PropPrefab is null )
			return;

		var propGo = PropPrefab.Clone( Transform.Position );
		if ( !propGo.Components.TryGet<Rigidbody>( out var rb, FindMode.EnabledInSelfAndDescendants ) )
		{
			Log.Error( $"Couldn't find rigidbody in prop {propGo.Name}" );
			return;
		}
		var direction = VelocityDirection;
		if ( AngleJitter is not null )
		{
			var jitter = AngleJitter.Value;
			var yaw = Rotation.FromYaw( Game.Random.Float( -jitter, jitter ) );
			var pitch = Rotation.FromPitch( Game.Random.Float( -jitter, jitter ) );
			direction *= yaw * pitch;
		}
		rb.Velocity = direction * VelocityScale.GetValue();
		if ( AngularVelocityScale is not null )
		{
			rb.AngularVelocity = Vector3.Random * AngularVelocityScale.Value;
		}
		if ( SelfDestructTime is not null )
		{
			var sdComp = propGo.Components.Create<SelfDestruct>();
			sdComp.Delay = SelfDestructTime.Value;
		}
	}

	protected override void DrawGizmos()
	{
		Gizmo.Draw.Color = Color.Red;
		Gizmo.Draw.Arrow( Vector3.Zero, VelocityDirection.Normal * 50f );
	}
}
