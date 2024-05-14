using Sandbox;

namespace Duccsoft.Terry;

public class OutfitData
{
	public Uniform Uniform { get; set; }
	public bool ShouldWearShirt { get; set; }
	public bool ShouldWearJacket { get; set; }
	public bool ShouldWearBottom { get; set; }
	public bool ShouldWearFootwear { get; set; }
	public ClothingContainer Resources { get; set; }
}
