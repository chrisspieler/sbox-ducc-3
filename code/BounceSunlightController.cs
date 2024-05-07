namespace Ducc;

public sealed class BounceSunlightController : Component
{
	[ConVar( "bounce_sunlight_debug" )]
	public static bool Debug { get; set; }

	[Property] public DirectionalLight SunLight { get; set; }
	[Property] public PointLight BounceLight { get; set; }
	
	private Vector3 TargetPosition { get; set; }

	protected override void OnUpdate()
	{
		if ( !SunLight.IsValid() || !BounceLight.IsValid() )
			return;

		var sunRay = new Ray( Transform.Position, SunLight.Transform.Rotation.Forward );
		var tr = Scene.Trace
			.Ray( sunRay, 3000f )
			.Run();
		var distanceFactor = MathX.Remap( SunLight.Transform.Rotation.Pitch(), -90f, 90f, 0f, 0.75f );
		TargetPosition = sunRay.Project( tr.Distance * distanceFactor );
		BounceLight.Transform.Position = BounceLight.Transform.Position.LerpTo( TargetPosition, Time.Delta );

		if ( Debug )
		{
			Gizmo.Draw.Color = BounceLight.LightColor;
			Gizmo.Draw.LineSphere( BounceLight.Transform.Position, 4f );
			Gizmo.Draw.Color = Color.Green;
			Gizmo.Draw.Line( tr.StartPosition, tr.EndPosition );
			Gizmo.Draw.LineSphere( TargetPosition, 2f );
		}
	}
}
