
using Sandbox.Audio;

public sealed class PlayerFootsteps : Component
{
	[Property] public SoundEvent FootstepSoundOverride { get; set; }
	[Property] public CharacterController Controller { get; set; }
	[Property] SkinnedModelRenderer Source { get; set; }
	[Property] MixerHandle TargetMixer { get; set; }
	[Property] public float VolumeScale { get; set; } = 1f;

	protected override void OnEnabled()
	{
		if ( Source is null )
			return;

		Source.OnFootstepEvent += OnEvent;
	}

	private bool _wasOnGround = true;
	TimeSince _timeSinceLanding;

	protected override void OnUpdate()
	{
		if ( Controller is null )
		{
			_wasOnGround = true;
			return;
		}

		if ( Controller.IsOnGround && !_wasOnGround )
		{
			_timeSinceLanding = 0f;
		}
		_wasOnGround = Controller.IsOnGround;
	}

	protected override void OnDisabled()
	{
		if ( Source is null )
			return;

		Source.OnFootstepEvent -= OnEvent;
	}

	TimeSince timeSinceStep;

	private void OnEvent( SceneModel.FootstepEvent e )
	{
		// Hearing footsteps when stopped sounds freaky.
		if ( _timeSinceLanding > 0.25f && Input.AnalogMove.Length < 0.1f )
		{
			return;
		}

		if ( timeSinceStep < 0.2f )
			return;

		var tr = Scene.Trace
			.Ray( e.Transform.Position + Vector3.Up * 20, e.Transform.Position + Vector3.Up * -20 )
			.Run();

		if ( !tr.Hit )
			return;

		if ( tr.Surface is null )
			return;

		timeSinceStep = 0;

		var sound = e.FootId == 0 ? tr.Surface.Sounds.FootLeft : tr.Surface.Sounds.FootRight;
		if ( FootstepSoundOverride is not null )
			sound = FootstepSoundOverride.ResourceName;

		if ( sound is null ) return;

		var handle = Sound.Play( sound, tr.HitPosition + tr.Normal * 5 );
		handle.Volume *= e.Volume * VolumeScale;
		handle.TargetMixer = TargetMixer.Get();
		handle.Update();
	}
}
