using Sandbox;
using System;
using System.Linq;

namespace Duccsoft.Terry;

public static class ClothingContainerExtensions
{
	public static void Add( this ClothingContainer container, Clothing clothing, ClothingConflictResolver conflictResolver )
	{
		if ( clothing is null )
			return;

		if ( conflictResolver == ClothingConflictResolver.RemoveOther )
		{
			container.Clothing.RemoveAll( x => !x.Clothing.CanBeWornWith( clothing ) );
		}
		else if ( conflictResolver == ClothingConflictResolver.RemoveSelf )
		{
			if ( container.Clothing.Any( c => !c.Clothing.CanBeWornWith( clothing ) ) )
			{
				return;
			}
		}
		container.Clothing.Add( new( clothing ) );
	}

	public static ClothingContainer Merge( this ClothingContainer first, ClothingContainer second, ClothingConflictResolver conflictResolver )
	{
		if ( first is null || second is null )
			throw new ArgumentNullException();

		var newContainer = new ClothingContainer();
		newContainer.Clothing = new( first.Clothing );
		foreach( var clothing in second.Clothing )
		{
			newContainer.Add( clothing.Clothing, conflictResolver );
		}
		return newContainer;
	}
}
