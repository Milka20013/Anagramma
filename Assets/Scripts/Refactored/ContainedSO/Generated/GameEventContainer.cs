
//This is a generated script. You should not touch it.

using UnityEngine;
using UnityEditor;
using System.Linq;
[CreateAssetMenu(menuName = "Container/GameEvent",fileName = "GameEventContainer")]
public partial class GameEventContainer : GeneratedContainer
{
public GameEvent tileSpawned;
#if UNITY_EDITOR
public override void FindReferences()
{GameEvent[] objects = Resources.LoadAll<GameEvent>("SO/Events");
tileSpawned = objects.Where(x=>x.name == "TileSpawned").First();
EditorUtility.SetDirty(this);
}
#endif
}