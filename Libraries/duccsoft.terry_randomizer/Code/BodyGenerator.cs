using Sandbox;
using System.Linq;

namespace Duccsoft.Terry;

public class BodyGenerator
{
	public BodyGenerator( BodyGeneratorConfig config )
	{
		_config = config;
	}

	private BodyGeneratorConfig _config;

	public BodyData GenerateData()
	{
		var data = new BodyData
		{
			Skin = Game.Random.FromList( _config.SkinTones ),
			IsElder = _config.ElderPercent >= Game.Random.Float( 0, 100 ),
			IsHairDyed = _config.HairDyePercent >= Game.Random.Float( 0, 100 ),
			HasEyebrows = _config.EyebrowsPercent >= Game.Random.Float( 0, 100 ),
			HasPlumpLips = _config.PlumpLipsPercent >= Game.Random.Float( 0, 100 ),
			IsHeadShaved = _config.CueballPercent >= Game.Random.Float( 0, 100 ),
			HasFacialHair = _config.FacialHairPercent >= Game.Random.Float( 0, 100 ),
			HasFacialOrnament = _config.FacialOrnamentPercent >= Game.Random.Float( 0, 100 )
		};
		data.HairColor = GenerateHairColor( data );
		data.Resources = GetClothingResources( data );
		return data;
	}

	private Color GenerateHairColor( BodyData data )
	{
		return data.IsHairDyed
			? GetRandomHairDyeColor()
			: GetNaturalHairColor( data );
	}

	private Color GetNaturalHairColor( BodyData data )
	{
		var gradientProgress = Game.Random.Float();
		if ( data.IsElder )
		{
			return _config.HairColors.ElderHairColor.Evaluate( gradientProgress );
		}
		else
		{
			return _config.HairColors.YoungHairColor.Evaluate( gradientProgress );
		}
	}

	private Color GetRandomHairDyeColor()
	{
		return new ColorHsv()
			.WithHue( Game.Random.Float( 0, 360 ) )
			.WithSaturation( _config.HairDyeSaturation.GetValue() )
			.WithValue( Game.Random.Float( 1f ) )
			.WithAlpha( 1f );
	}

	public ClothingContainer GetClothingResources( BodyData data )
	{
		var clothing = new ClothingContainer();
		clothing.Add( GetSkin( data ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomHair( data ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomFacialHair( data ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomLips( data ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomEyebrows( data ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomFacialOrnament( data ), ClothingConflictResolver.DoNothing );
		return clothing;
	}

	public Clothing GetSkin( BodyData data )
	{
		return data.IsElder
			? data.Skin.ElderSkin
			: data.Skin.YoungSkin;
	}

	private Clothing GetRandomHair( BodyData data )
	{
		if ( data.IsHeadShaved )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( c => c.Category == Clothing.ClothingCategory.Hair );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomFacialHair( BodyData data )
	{
		if ( !data.HasFacialHair )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( c => c.Category == Clothing.ClothingCategory.Facial && c.SubCategory == "Beards" );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomLips( BodyData data )
	{
		if ( !data.HasPlumpLips )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>().Where( ClothingCategories.IsLips );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomEyebrows( BodyData data )
	{
		if ( !data.HasEyebrows )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>().Where( ClothingCategories.IsEyebrows );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomFacialOrnament( BodyData data )
	{
		// Beards conflict with the facial ornament, so skip this.
		if ( data.HasFacialHair || !data.HasFacialOrnament )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( ClothingCategories.IsFacialOrnament );
		return Game.Random.FromList( options.ToList() );
	}
}
