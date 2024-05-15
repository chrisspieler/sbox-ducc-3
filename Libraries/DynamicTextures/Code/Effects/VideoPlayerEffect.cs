using Sandbox;
using System;

namespace Duccsoft.Graphics;

public class VideoPlayerEffect : Component, ITextureEffect
{
	[Property] public VideoPlayerComponent Player { get; set; }
	[Property] public bool VideoIsReady => _videoIsReady;
	private bool _videoIsReady;
    public void Apply(Texture texture)
    {
		_videoIsReady = Player.IsValid() && Player.VideoTexture is not null;
		if ( !_videoIsReady )
			return;

		var videoSize = Player.VideoTexture.Size;
		var srcRect = (X: 0, Y: 0, Width: (int)videoSize.x, Height: (int)videoSize.y);
		var dstSize = (x: (int)texture.Size.x, y: (int)texture.Size.y);
		var dstData = new Color32[dstSize.x * dstSize.y];
		Player.VideoTexture.GetPixels( srcRect, 0, 0, dstData.AsSpan(), ImageFormat.RGBA8888, dstSize );
		texture.Update( dstData, 0, 0, dstSize.x, dstSize.y );
    }
}
