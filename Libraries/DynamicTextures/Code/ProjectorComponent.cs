using Sandbox;

namespace Duccsoft.Graphics;

[Title ("Projector"), Category( "Graphics" )]
public class ProjectorComponent : DynamicTextureComponent
{
	[Property, RequireComponent] public SpotLight Light { get; set; }

	public override void OnAfterUpdate()
	{
		if ( Light.Cookie != OutputTexture )
		{
			Light.Cookie = OutputTexture;
		}
	}
}
