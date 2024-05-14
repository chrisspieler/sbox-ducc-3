namespace Ducc;

public class SausageSpawner
{
	private const string SAUSAGE_PREFAB_PATH = "prefabs/sausage.prefab";

	[ActionGraphNode("Sausage.Spawn")]
	public static SausageController Spawn( Transform worldTx, Vector3 heading )
	{
		var scene = Game.ActiveScene;
		if ( scene is null )
			return null;

		var prefabScene = SceneUtility.GetPrefabScene( ResourceLibrary.Get<PrefabFile>( SAUSAGE_PREFAB_PATH ) );
		var sausageGo = prefabScene.Clone( worldTx );
		var controller = sausageGo.Components.GetInDescendantsOrSelf<SausageController>();
		controller.MovementInput = heading;
		return controller;
	}
}
