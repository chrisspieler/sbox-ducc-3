using Sandbox;
using System;
using System.Collections.Generic;

namespace Duccsoft.Terry;

[GameResource( "Clothing Generator Config", "clthcfg", "Defines what clothing items may be returned by a ClothingGenerator.", Category = "Terry Randomizer", Icon = "dry_cleaning")]
public class OutfitGeneratorConfig : GameResource
{
	public enum UniformHandling
	{
		/// <summary>
		/// Clothing belonging to a uniform will not be given any special treatment by <see cref="OutfitGenerator"/>.
		/// </summary>
		Mix,
		/// <summary>
		/// Any clothing belonging to a uniform will not be used by <see cref="OutfitGenerator"/>.
		/// </summary>
		Exclude,
		/// <summary>
		/// Any clothing belonging to a uniform will only be used by <see cref="OutfitGenerator"/> if
		/// the entire uniform should be used, as determined by <see cref="UniformPercent"/>.
		/// </summary>
		Group
	}

	[Property] public UniformHandling UniformPreference { get; set; } = UniformHandling.Group;
	[Property, ShowIf( nameof(UniformPreference), UniformHandling.Group ), Range( 0, 100 )] 
	public float UniformPercent { get; set; } = 5f;
	[Property, ShowIf( nameof( UniformPreference ), UniformHandling.Group )] 
	public List<Uniform> ExcludedUniforms { get; set; }
	[Property] public List<Clothing> ExcludedClothing { get; set; }
	[Property, Range( 0, 100 )] public float ShirtPercent { get; set; } = 95f;
	[Property, Range( 0, 100 )] public float JacketPercent { get; set; } = 20f;
	[Property, Range( 0, 100 )] public float BottomPercent { get; set; } = 100f;
	[Property, Range( 0, 100 )] public float FootwearPercent { get; set; } = 80f;
}
