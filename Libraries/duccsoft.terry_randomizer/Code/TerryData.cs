using Sandbox;

namespace Duccsoft.Terry;

public class TerryData
{
	public BodyData Body { get; set; }
	public OutfitData Outfit { get; set; }
	public ClothingContainer Container => Body.Resources.Merge( Outfit.Resources, ClothingConflictResolver.RemoveOther );

	public static TerryData Generate( BodyGeneratorConfig bodyConfig = null, OutfitGeneratorConfig outfitConfig = null )
	{
		var data = new TerryData();
		if ( bodyConfig is not null )
		{
			data.Body = new BodyGenerator( bodyConfig ).GenerateData();
		}
		if ( outfitConfig is not null )
		{
			data.Outfit = new OutfitGenerator( outfitConfig ).GenerateData();
		}
		return data;
	}
}
