using Sandbox;

namespace Duccsoft.Terry;

[GameResource( "Skin Tone", "skin", "A mapping between skin tones and their corresponding \"Clothing\" (actually skin) options.", Category = "Terry Randomizer", Icon = "gradient" )]
public class SkinTone : GameResource
{
	public string Name { get; set; }
	public Clothing YoungSkin { get; set; }
	public Clothing ElderSkin { get; set; }
}
