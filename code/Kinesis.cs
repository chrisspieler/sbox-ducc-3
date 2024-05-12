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
			Transform.Position += Transform.Rotation.Forward * _speed * Time.Delta;
			// TODO: Cling to walls, snap to surface.
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
	}
}
