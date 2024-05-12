namespace Ducc;

public class FoxMiteSpawner : Component
{
	[Property] public GameObject Prefab { get; set; }
	[Property] public BBox SpawnBounds { get; set; }
	[Property] public int MaxMites { get; set; } = 50;
	[Property] public int MiteCount => _mites.Count;

	private List<GameObject> _mites = new();

	protected override void OnUpdate()
	{
		foreach( var miteGo in _mites.ToArray() )
		{
			if ( !miteGo.IsValid() )
			{
				_mites.Remove( miteGo );
			}
		}
	}

	[Button("Spawn One")]
	public void SpawnOne()
	{
		if ( MiteCount >= MaxMites )
			return;

		for (int i = 0; i < 25; i++)
		{
			var randomPoint = SpawnBounds.RandomPointInside;
			if ( PlayerController.CanSeePoint( randomPoint ) )
			{
				continue;
			}
			var rotation = Transform.Rotation * Rotation.FromAxis( Transform.Rotation.Up, Game.Random.Float( 0, 360 ) );
			var miteGo = Prefab.Clone( GameObject, randomPoint, rotation, 1f );
			miteGo.BreakFromPrefab();
			_mites.Add( miteGo );
			break;
		}
	}

	protected override void DrawGizmos()
	{
		Gizmo.Draw.Color = Color.Green;
		Gizmo.Draw.LineBBox( SpawnBounds );

		if ( !Gizmo.IsSelected )
			return;

		foreach( var mite in _mites )
		{
			Gizmo.Draw.LineBBox( BBox.FromPositionAndSize( mite.Transform.Position, 1f ) );
		}
	}
}
