namespace Ducc;

public sealed class AttentionGrabber : Component, Component.ITriggerListener
{
	[Property] public GameObject AttentionTarget { get; set; }

	protected override void OnStart()
	{
		AttentionTarget ??= GameObject;
	}

	public void OnTriggerEnter( Collider other )
	{
		var sausage = other.GameObject.Components.GetInAncestorsOrSelf<SausageController>();
		if ( !sausage.IsValid() )
			return;

		sausage.LookTarget = AttentionTarget;
	}

	public void OnTriggerExit( Collider other )
	{
		var sausage = other.GameObject.Components.GetInAncestorsOrSelf<SausageController>();
		if ( !sausage.IsValid() )
			return;

		if ( sausage.LookTarget == AttentionTarget )
		{
			sausage.LookTarget = null;
		}
	}
}
