using Sandbox;

namespace Duccsoft.Terry;

public class BodyData
{
	public SkinTone Skin { get; set; }
	public bool IsElder { get; set; }
	public bool IsHeadShaved { get; set; }
	public bool HasFacialHair { get; set; }
	public bool IsHairDyed { get; set; }
	public Color HairColor { get; set; }
	public bool HasPlumpLips { get; set; }
	public bool HasEyebrows { get; set; }
	public bool HasFacialOrnament { get; set; }
	public ClothingContainer Resources { get; set; }
}
