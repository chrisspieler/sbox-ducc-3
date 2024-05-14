using Sandbox;

namespace Duccsoft.Terry;

public static class ClothingCategories
{
	public static bool IsHair( this Clothing clothing )
	{
		return clothing.Category == Clothing.ClothingCategory.Hair
			|| clothing.SubCategory == "Beards";
	}

	public static bool IsEyebrows( this Clothing clothing )
	{
		return clothing.Category == Clothing.ClothingCategory.Facial
			&& clothing.SubCategory == "eyebrows";
	}

	public static bool IsLips( this Clothing clothing )
	{
		return clothing.Category == Clothing.ClothingCategory.Facial
			&& clothing.SubCategory == "Lips";
	}

	public static bool IsFacialOrnament( this Clothing clothing )
	{
		return clothing.Category == Clothing.ClothingCategory.Facial
			&& !IsEyebrows( clothing )
			&& !IsLips( clothing )
			&& !IsHair( clothing );
	}

	public static bool IsFullOutfit( this Clothing clothing )
	{
		return clothing.SubCategory == "Full Outfit";
	}

	public static bool IsTop( this Clothing clothing )
	{
		return clothing.Category == Clothing.ClothingCategory.Tops
			&& !clothing.HideBody.HasFlag( Clothing.BodyGroups.Legs )
			&& !clothing.SlotsOver.HasFlag( Clothing.Slots.Groin );
	}

	public static bool IsShirt( this Clothing clothing )
	{
		if ( clothing is null )
			return false;

		return IsShirt( clothing.Parent )
			|| IsTop( clothing )
			&& !IsDress( clothing )
			&& clothing.SlotsOver.HasFlag( Clothing.Slots.Chest )
			&& !IsFullOutfit( clothing );
	}

	public static bool IsJacket( this Clothing clothing )
	{
		if ( clothing is null )
			return false;

		return IsJacket( clothing.Parent )
			|| IsTop( clothing )
			&& !IsDress( clothing )
			&& !clothing.SlotsOver.HasFlag( Clothing.Slots.Chest )
			&& !IsFullOutfit( clothing )
			&& clothing.SubCategory != "Vests";
	}

	public static bool IsDress( this Clothing clothing )
	{
		if ( clothing is null )
			return false;

		return IsDress( clothing.Parent )
			|| clothing.SubCategory == "Dresses";
	}

	public static bool IsBottom( this Clothing clothing )
	{
		if ( clothing is null )
			return false;

		return IsBottom( clothing.Parent )
			|| clothing.Category == Clothing.ClothingCategory.Bottoms;
	}

	public static bool IsFootwear( this Clothing clothing )
	{
		if ( clothing is null )
			return false;

		return IsFootwear( clothing.Parent )
			|| clothing.Category == Clothing.ClothingCategory.Footwear;
	}
}
