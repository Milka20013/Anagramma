using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneratedContainer), true)]
public class GeneratedContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GeneratedContainer container = (GeneratedContainer)target;
        if (GUILayout.Button("Find references"))
        {
            container.FindReferences();
        }
    }
}
