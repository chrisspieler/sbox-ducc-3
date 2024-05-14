namespace Ducc;

public sealed class SausageDestroyer : Component, Component.ITriggerListener
{
	public void OnTriggerEnter(Collider other)
	{
		var sausage = other.GameObject.Components.GetInAncestorsOrSelf<SausageController>();
		if ( !sausage.IsValid() )
			return;

		sausage.GameObject.Destroy();
	}
}
