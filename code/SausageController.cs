using Sandbox.Citizen;

namespace Ducc;

public sealed class SausageController : Component
{
	[RequireComponent] public CharacterController Character { get; set; }
	[Property] public GameObject Body { get; set; }
	[Property] public GameObject Eyes { get; set; }
	[Property] public CitizenAnimationHelper Animator { get; set; }
	[Property] public Vector3 MovementInput { get; set; }
	[Property] public bool ShouldRun { get; set; } = false;
	[Property] public bool ShouldDuck { get; set; } = false;
	[Property] public float WalkSpeed { get; set; } = 120f;
	[Property] public GameObject LookTarget { get; set; }

	protected override void OnStart()
	{
		GameObject.BreakFromPrefab();
	}

	protected override void OnFixedUpdate()
	{
		var wishVelocity = GetWishVelocity();
		Character.Velocity = wishVelocity;
		Character.Move();
		AnimateBody( wishVelocity );
		if ( !wishVelocity.IsNearlyZero() )
		{
			Transform.Rotation = Rotation.FromYaw( Rotation.LookAt( wishVelocity ).Yaw() );
		}
	}

	private Vector3 GetWishVelocity()
	{
		var wishVelocity = MovementInput * WalkSpeed;
		if ( ShouldDuck )
		{
			wishVelocity *= 0.7f;
		}
		else if ( ShouldRun )
		{
			wishVelocity *= 2.5f;
		}
		else if ( LookTarget.IsValid() )
		{
			// Slow down when looking at something, otherwise the head tilt is not noticeable.
			wishVelocity *= 0.5f;
		}
		return wishVelocity;
	}

	private void AnimateBody( Vector3 wishVelocity )
	{
		if ( !Animator.IsValid() )
			return;

		Animator.DuckLevel = ShouldDuck
			? 1
			: 0;
		Animator.MoveStyle = ShouldRun
			? CitizenAnimationHelper.MoveStyles.Run
			: CitizenAnimationHelper.MoveStyles.Walk;
		Animator.WithVelocity( Character.Velocity );
		Animator.WithWishVelocity( wishVelocity );
		Animator.LookAtEnabled = LookTarget.IsValid();
		if ( Animator.LookAtEnabled )
		{
			Animator.EyeSource = Eyes;
			Animator.LookAt = LookTarget;
			Animator.EyesWeight = 1f;
			Animator.HeadWeight = 0.5f;
			Animator.BodyWeight = 0.25f;
		}
		else
		{
			Animator.WithLook( Transform.Position + Transform.Rotation.Forward * 200f, 1, 0.5f, 0.25f );
		}
	}
}
