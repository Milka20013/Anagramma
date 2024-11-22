using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GridManager manager = (GridManager)target;
        if (GUILayout.Button("Shuffle"))
        {
            manager.ShuffleTiles();
        }
    }
}
