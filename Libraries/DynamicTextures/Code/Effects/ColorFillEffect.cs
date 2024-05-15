using Sandbox;

namespace Duccsoft.Graphics;

[Title( "Color Fill" ), Category( "Graphics" )]
public class ColorFillEffect : Component, ITextureEffect
{
	public enum UpdateMode
	{
		/// <summary>
		/// The texture will be filled with one specific color.
		/// </summary>
		Static,
		/// <summary>
		/// The texture will be filled with a color that cycles over time.
		/// </summary>
		Cycle
	}

	[Property] public UpdateMode Mode { get; set; }
	// Static mode
	[Property, ShowIf( nameof( Mode ), UpdateMode.Static )]
	public Color Color { get; set; } = Color.White;
	// Cycle mode
	[Property, ShowIf( nameof( Mode ), UpdateMode.Cycle )] 
	public Gradient ColorCycle { get; set; }
	[Property, ShowIf( nameof( Mode ), UpdateMode.Cycle )]
	public float CycleSpeed { get; set; } = 1f;

	public void Apply( Texture texture )
	{
		if ( texture.Size.x <= 0 || texture.Size.y <= 0 )
			return;

		var color = Mode switch
		{
			UpdateMode.Cycle	=> ColorCycle.Evaluate( Time.Now * CycleSpeed % 1f ),
			_					=> Color
		};
		texture.Update( color, new Rect( Vector2.Zero, texture.Size ) );
	}
}
