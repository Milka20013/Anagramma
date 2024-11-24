
//This is a generated script. You should not touch it.

using UnityEngine;
using UnityEditor;
using System.Linq;
[CreateAssetMenu(menuName = "Container/GameEvent",fileName = "GameEventContainer")]
public partial class GameEventContainer : GeneratedContainer
{
public GameEvent successfulWordScan;
public GameEvent tileClicked;
public GameEvent tileDestroyed;
public GameEvent tileSpawned;
public GameEvent wordScanned;
#if UNITY_EDITOR
public override void FindReferences()
{GameEvent[] objects = Resources.LoadAll<GameEvent>("SO/Events");
successfulWordScan = objects.Where(x=>x.name == "SuccessfulWordScan").First();
tileClicked = objects.Where(x=>x.name == "TileClicked").First();
tileDestroyed = objects.Where(x=>x.name == "TileDestroyed").First();
tileSpawned = objects.Where(x=>x.name == "TileSpawned").First();
wordScanned = objects.Where(x=>x.name == "WordScanned").First();
EditorUtility.SetDirty(this);
}
#endif
}