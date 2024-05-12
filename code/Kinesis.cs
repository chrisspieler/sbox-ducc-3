using Sandbox.Utility;

namespace Sandbox
{
	public class Kinesis : Component
	{
		[Property] public float LowSpeed { get; set; } = 2f;
		[Property] public float HighSpeed { get; set; } = 8f;
		[Property] public float LowRotation { get; set; } = 90f;
		[Property] public float HighRotation { get; set; } = 720f;
		[Property] public float Comfort => _comfort;
		private float _comfort;
		[Property] public float Speed => _speed;
		private float _speed;
		[Property] public float TraceHeight { get; set; } = 0.5f;
		[Property] public float TraceForwardDistance { get; set; } = 1f;
		[Property] public float TraceDownDistance { get; set; } = 0.5f;
		private float _noiseSeed;

		protected override void OnStart()
		{
			_noiseSeed = Game.Random.Float( 1_000_000 );
		}

		protected override void OnFixedUpdate()
		{
			UpdateComfort();
			UpdateSpeed();
			Transform.Rotation *= Rotation.FromAxis( Transform.Rotation.Up, GetRotation() );
			if ( !Transform.Rotation.Forward.IsNaN )
			{
				Transform.Position += Transform.Rotation.Forward * _speed * Time.Delta;
			}
			SnapForward();
			SnapToFloor();
		}

		private void UpdateComfort()
		{
			_comfort = Transform.Position.Length.LerpInverse( 200f, 0f );
			// TODO: Comfort should depend on things like light and humidity.
		}

		private void UpdateSpeed()
		{
			var difference = HighSpeed - LowSpeed;
			difference *= ( 1f - _comfort ) * Noise.Perlin( Time.Now * 10, _noiseSeed );
			_speed = LowSpeed + difference;
		}

		private float GetRotation()
		{
			// Turn faster if less comfortable.
			var degrees = HighRotation.LerpTo( LowRotation, _comfort );
			// Scale from perlin noise so that the rotation direction occasionally reverses.
			var noise = Noise.Perlin( Time.Now * 100, _noiseSeed ) * 2 - 1;
			degrees *= noise;
			return degrees * Time.Delta;
		}

		private void SnapForward()
		{
			var startPos = Transform.Position + Transform.Rotation.Up * TraceHeight;
			var ray = new Ray( startPos, Transform.Rotation.Forward );
			var tr = Scene.Trace
				.Ray( ray, TraceForwardDistance )
				.WithoutTags( "player" )
				.Run();
			if ( !tr.Hit )
				return;

			Transform.Position = tr.EndPosition;
			var crossAxis = Transform.Rotation.Left.Cross( tr.Normal );
			Transform.Rotation = Rotation.LookAt( crossAxis, tr.Normal );
		}

		private void SnapToFloor()
		{
			var startPos = Transform.Position + Transform.Rotation.Up * TraceHeight;
			var ray = new Ray( startPos, Transform.Rotation.Down );
			var tr = Scene.Trace
				.Ray( ray, TraceDownDistance )
				.WithoutTags( "player" )
				.Run();

			if ( !tr.Hit )
			{
				WrapAroundSnap();
				return;
			}

			Transform.Position = tr.EndPosition;
			Transform.Rotation = Rotation.LookAt( Transform.Rotation.Left.Cross( tr.Normal ), tr.Normal );
		}

		private void WrapAroundSnap()
		{
			// Just turn around instead of attempting to wrap around.
			Transform.Rotation *= Rotation.FromYaw( 180f );
			// TODO: Handle wrapping around over the lip of an edge.
		}
	}
}
