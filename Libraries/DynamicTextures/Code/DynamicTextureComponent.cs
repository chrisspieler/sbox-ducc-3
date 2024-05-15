using Sandbox;
using System;

namespace Duccsoft.Graphics;

public abstract class DynamicTextureComponent : Component
{
	[Property, Range( 1, 60, 1 )] public float MaxUpdatesPerSecond { get; set; } = 15f;
	[Property] public bool Debug { get; set; } = false;
	[Property] public Vector2 Size { get; set; } = new Vector2( 100 );

	protected Texture OutputTexture { get; set; }

	private RealTimeSince _lastTextureUpdate;

	protected override void OnUpdate()
	{
		if ( _lastTextureUpdate < 1f / MaxUpdatesPerSecond )
			return;

		_lastTextureUpdate = 0f;
		EnsureTextureCreated();
		ApplyEffects();
		OnAfterUpdate();
	}

	private void EnsureTextureCreated()
	{
		if ( OutputTexture == null || OutputTexture.Size != Size )
		{
			InitializeTexture();
		}
	}

	[Button( "Initialize Texture")]
	public void InitializeTexture()
	{
        OutputTexture = Texture.Create( (int)Size.x, (int)Size.y, ImageFormat.RGBA8888 )
			.WithName( $"{GameObject.Name}_DynamicTexture" )
			.WithDynamicUsage()
			.WithUAVBinding()
			.Finish();
	}

	private void ApplyEffects()
	{
		var effects = GameObject.Components.GetAll<ITextureEffect>( FindMode.EnabledInSelf );
		foreach ( var effect in effects )
		{
			effect.Apply( OutputTexture );
		}
	}

	public virtual void OnAfterUpdate() { }

	protected override void DrawGizmos()
	{
		Gizmo.Draw.IgnoreDepth = true;

		Gizmo.Draw.Sprite( Vector3.Down * 50f, new Vector2( 30 ), OutputTexture, true );
	}
}
