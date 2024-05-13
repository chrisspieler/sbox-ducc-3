namespace Ducc;

public sealed class SausageDirectControl : Component
{
	[Property] public SausageController Controller { get; set; }

	protected override void OnUpdate()
	{
		if ( !Controller.IsValid() || !Scene.Camera.IsValid() )
			return;

		Controller.MovementInput = Input.AnalogMove.Normal * Scene.Camera.Transform.Rotation;
		Controller.ShouldRun = Input.Down( "run" );
		Controller.ShouldDuck = Input.Down( "duck" );
	}
}
