using Sandbox;
using System.Collections.Generic;

namespace Duccsoft.Terry;

[GameResource( "Uniform", "uniform", "A collection of clothing items that are meant to be worn together.", Category = "Terry Randomizer", Icon = "engineering" )]
public class Uniform : GameResource
{
	[Property] public string Name { get; set; }
	/// <summary>
	/// A list of clothing items that would look out of place if worn not as part of this uniform.
	/// For example: a cuirass or cardboard box.
	/// </summary>
	[Property] public List<Clothing> CharacteristicItems { get; set; }
	/// <summary>
	/// A list of clothing items that should be worn as part of this uniform but aren't exclusive to this uniform.
	/// For example: jeans and work boots.
	/// </summary>
	[Property] public List<Clothing> SupplementaryItems { get; set; }

	public ClothingContainer AsClothingContainer()
	{
		var clothing = new ClothingContainer();
		if ( CharacteristicItems is not null )
		{
			foreach ( var item in CharacteristicItems )
			{
				clothing.Add( item, ClothingConflictResolver.RemoveOther );
			}
		}
		if ( SupplementaryItems is not null )
		{
			foreach ( var item in SupplementaryItems )
			{
				clothing.Add( item, ClothingConflictResolver.RemoveOther );
			}
		}
		return clothing;
	}
}
