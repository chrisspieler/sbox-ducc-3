using Sandbox;

namespace Duccsoft.Terry;

[GameResource( "Hair Color Data", "hrclr", "Provides gradients of hair colors.", Category = "Terry Randomizer", Icon = "face_2" )]
public class HairColorData : GameResource
{
	[Property] public Gradient YoungHairColor { get; set; }
	[Property] public Gradient ElderHairColor { get; set; }
}
