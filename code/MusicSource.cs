using Sandbox.Audio;
using System;

namespace Ducc;

public sealed class MusicSource : Component
{
	[Property] public Action OnFinished { get; set; }
	[Property] public Action OnRepeated { get; set; }

	[Property] public string MusicPath { get; set; }
	[Property] public bool PlayOnStart { get; set; } = true;
	[Property] public float Volume 
	{
		get => _volume;
		set
		{
			_volume = value;
			if ( _player != null )
			{
				_player.Volume = value;
			}
		} 
	}
	private float _volume = 1f;
	[Property] public MixerHandle TargetMixer { get; set; }

	private MusicPlayer _player;

	protected override void OnStart()
	{
		if ( PlayOnStart )
		{
			Play();
		}
	}

	[Button( "Play" )]
	public void Play()
	{
		if ( !Game.IsPlaying )
			return;

		Stop();
		_player = CreatePlayer();
		ConfigurePlayer( _player );
	}

	private MusicPlayer CreatePlayer()
	{
		return MusicPlayer.Play( FileSystem.Mounted, MusicPath );
	}

	private void ConfigurePlayer( MusicPlayer player )
	{
		player.Volume = Volume;
		player.Position = Transform.Position;
		player.OnFinished += OnFinished;
		player.OnRepeated += OnRepeated;
	}

	[Button( "Stop" )]
	public void Stop()
	{
		if ( !Game.IsPlaying )
			return;

		_player?.Stop();
		_player?.Dispose();
		_player = null;
	}
}
