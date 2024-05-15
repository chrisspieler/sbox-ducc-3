using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace Duccsoft.Terry;

public class OutfitGenerator
{
	public OutfitGenerator( OutfitGeneratorConfig config )
	{
		_config = config;
	}

	private OutfitGeneratorConfig _config;

	public OutfitData GenerateData()
	{
		var data = new OutfitData()
		{
			ShouldWearShirt = _config.ShirtPercent >= Game.Random.Float( 0, 100 ),
			ShouldWearJacket = _config.JacketPercent >= Game.Random.Float( 0, 100 ),
			ShouldWearBottom = _config.BottomPercent >= Game.Random.Float( 0, 100 ),
			ShouldWearFootwear = _config.FootwearPercent >= Game.Random.Float( 0, 100 )
		};
		if ( _config.UniformPercent >= Game.Random.Float( 0, 100 ) )
		{
			data.Uniform = GetRandomUniform();
		}
		data.Resources = GetClothingResources( data );
		return data;
	}

	private ClothingContainer GetClothingResources( OutfitData data )
	{
		if ( data.Uniform is not null )
		{
			return data.Uniform.AsClothingContainer();
		}
		var exclusions = GetExcludedClothing();
		var clothing = new ClothingContainer();
		clothing.Add( GetRandomShirt( data, exclusions ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomJacket( data, exclusions ), ClothingConflictResolver.RemoveSelf );
		clothing.Add( GetRandomBottoms( data, exclusions ), ClothingConflictResolver.DoNothing );
		clothing.Add( GetRandomFootwear( data, exclusions ), ClothingConflictResolver.DoNothing );
		return clothing;
	}

	private IEnumerable<Clothing> GetExcludedClothing()
	{
		var excluded = new HashSet<Clothing>();
		ExcludeConfigExclusions( excluded );
		if ( _config.UniformPreference != OutfitGeneratorConfig.UniformHandling.Mix )
		{
			ExcludeUniformClothing( excluded );
		}
		return excluded;
	}

	private void ExcludeConfigExclusions( HashSet<Clothing> excluded )
	{
		if ( _config.ExcludedClothing is null )
			return;

		foreach ( var exclusion in _config.ExcludedClothing )
		{
			excluded.Add( exclusion );
		}
	}

	private void ExcludeUniformClothing( HashSet<Clothing> excluded )
	{
		foreach ( var uniform in ResourceLibrary.GetAll<Uniform>() )
		{
			if ( uniform.CharacteristicItems is null )
				continue;

			foreach ( var item in uniform.CharacteristicItems )
			{
				excluded.Add( item );
			}
		}
	}

	private Uniform GetRandomUniform()
	{
		var uniforms = ResourceLibrary.GetAll<Uniform>()
			.Except( _config.ExcludedUniforms ?? Enumerable.Empty<Uniform>() )
			.ToList();

		if ( !uniforms.Any() )
			return null;

		return Game.Random.FromList( uniforms );
	}

	private Clothing GetRandomShirt( OutfitData data, IEnumerable<Clothing> excluded = null )
	{
		if ( !data.ShouldWearShirt )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( ClothingCategories.IsShirt )
			.Except( excluded ?? Enumerable.Empty<Clothing>() );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomJacket( OutfitData data, IEnumerable<Clothing> excluded = null )
	{
		// Assumes that someone would not wear a jacket without also wearing a shirt.
		if ( !data.ShouldWearShirt || !data.ShouldWearJacket )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( ClothingCategories.IsJacket )
			.Except( excluded ?? Enumerable.Empty<Clothing>() );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomBottoms( OutfitData data, IEnumerable<Clothing> excluded = null )
	{
		if ( !data.ShouldWearBottom )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( ClothingCategories.IsBottom )
			.Except( excluded ?? Enumerable.Empty<Clothing>() );
		return Game.Random.FromList( options.ToList() );
	}

	private Clothing GetRandomFootwear( OutfitData data, IEnumerable<Clothing> excluded = null )
	{
		if ( !data.ShouldWearFootwear )
			return null;

		var options = ResourceLibrary.GetAll<Clothing>()
			.Where( ClothingCategories.IsFootwear )
			.Except( excluded ?? Enumerable.Empty<Clothing>() );
		return Game.Random.FromList( options.ToList() );
	}
}
