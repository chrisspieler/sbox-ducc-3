using Sandbox;
using System;
using System.Collections.Generic;

namespace Duccsoft.Terry;

[GameResource("Body Generator Config", "bodcfg", "Defines the parameters of generating the features of an unclothed Terry.", Category = "Terry Randomizer", Icon = "accessibility" )]
public class BodyGeneratorConfig : GameResource
{
	[Property] public HairColorData HairColors { get; set; }
	[Property] public List<SkinTone> SkinTones { get; set; }
	[Property, Range( 0, 100 )] public float ElderPercent { get; set; } = 17f;
	[Property, Range( 0, 100 )] public float CueballPercent { get; set; } = 4.5f;
	[Property, Range( 0, 100 )] public float FacialHairPercent { get; set; } = 15f;
	[Property, Range( 0, 100 )] public float HairDyePercent { get; set; } = 5f;
	[Property, Range( 0, 1 )] public RangedFloat HairDyeSaturation { get; set; } = 0.6f;
	[Property, Range( 0, 100 )] public float FacialOrnamentPercent { get; set; } = 10f;
	[Property, Range( 0, 100 )] public float PlumpLipsPercent { get; set; } = 20f;
	[Property, Range( 0, 100 )] public float EyebrowsPercent { get; set; } = 90f;
}
