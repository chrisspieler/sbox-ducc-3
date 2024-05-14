using Sandbox;
using System.Linq;

namespace Duccsoft.Terry;

public static class SkinnedModelRendererExtensions
{
	public static void DyeHair( this SkinnedModelRenderer citizen, ClothingContainer clothing, Color hairColor )
	{
		if ( !citizen.IsValid() )
			return;

		var hairClothing = clothing.Clothing
			.Select( c => c.Clothing )
			.Where( ClothingCategories.IsHair );
		foreach ( var child in citizen.GameObject.Children )
		{
			if ( hairClothing.Any( c => child.Name.Contains( c.ResourceName ) ) )
			{
				var renderer = child.Components.Get<SkinnedModelRenderer>();
				renderer.Tint = hairColor;
			}
		}
	}
}
