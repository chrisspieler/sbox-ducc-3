using Duccsoft.Terry;

namespace Ducc;

public sealed class SausageDresser : Component
{
	[Property] public SkinnedModelRenderer Model { get; set; }
	[Property] public BodyGeneratorConfig BodyConfig { get; set; }
	[Property] public OutfitGeneratorConfig OutfitConfig { get; set; }
	[Property] public bool DressOnStart { get; set; } = true;
	protected override void OnStart()
	{
		if ( DressOnStart )
		{
			GenerateOutfit();
		}
	}

	[Button("Generate Outfit")]
	public void GenerateOutfit()
	{
		var terryData = TerryData.Generate( BodyConfig, OutfitConfig );
		terryData.Container.Apply( Model );
	}
}
