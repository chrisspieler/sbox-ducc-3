public partial class PlayerController
{
	private static PlayerController _instance;

	protected override void OnDestroy()
	{
		if ( _instance == this )
		{
			_instance = null;
		}
	}

	[ActionGraphNode( "Player.CanSeePoint" )]
	public static bool CanSeePoint( Vector3 point, float threshold = 0 )
	{
		var camera = Game.ActiveScene?.Camera;
		if ( !camera.IsValid() || _instance is null )
			return false;

		var dirToPoint = (point - camera.Transform.Position).Normal;
		var dot = camera.Transform.Rotation.Forward.Dot( dirToPoint );
		return dot >= threshold;
	}
}
