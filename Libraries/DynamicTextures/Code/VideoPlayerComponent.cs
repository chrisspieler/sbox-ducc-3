using Sandbox;

namespace Duccsoft.Graphics;

public class VideoPlayerComponent : Component
{
	[Property] public string VideoPath { get; set; }
	[Property] public bool IsPlaying
	{
		get
		{
			if ( _player is null )
				return false;

			return !_player.IsPaused && Progress < 1f;
		}
	}
	[Property] public float Progress
	{
		get
		{
			if ( _player is null || _player.PlaybackTime == 0f || _player.Duration == 0f )
				return 0f;

			return _player.PlaybackTime / _player.Duration;
		}
	}
	[Property] public Texture VideoTexture => _player?.Texture;

	private VideoPlayer _player;

    protected override void OnUpdate()
    {
		if ( _player is null )
			return;

		_player.Present();
    }

    [Button("Play")]
	public void Play()
	{
		Stop();
		_player = CreateVideoPlayer();
	}

	private VideoPlayer CreateVideoPlayer()
	{
		var player = new VideoPlayer();
		player.Play( FileSystem.Mounted, VideoPath );
		return player;
	}

	[Button("Stop")]
	public void Stop()
	{
		_player?.Stop();
		_player?.Dispose();
		_player = null;
	}

	[Button("Toggle Pause")]
	public void TogglePause()
	{
		if ( _player is null )
			return;

		_player.TogglePause();
	}
}
