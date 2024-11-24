using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class SOContainerGenerator
{
#if UNITY_EDITOR
    private static string GetFolderPath(bool wholePath = false)
    {
        var g = AssetDatabase.FindAssets($"t:Script {nameof(SOContainerGenerator)}");
        string fullPath = AssetDatabase.GUIDToAssetPath(g[0]);
        if (wholePath)
        {
            return fullPath;
        }
        var parts = fullPath.Split('/');
        StringBuilder path = new(50);
        for (int i = 0; i < parts.Length - 1; i++)
        {
            path.Append(parts[i] + '/');
        }
        path.Append("Generated/");
        return path.ToString();
    }
    public static void CreateScript<T>(string contentPath) where T : Object
    {
        StringBuilder content = new(500);
        string className = typeof(T).Name;
        content.Append("\n//This is a generated script. You should not touch it.\n\n");
        content.Append($"using UnityEngine;\nusing UnityEditor;\nusing System.Linq;\n[CreateAssetMenu(menuName = \"Container/{className}\",fileName = \"{className}Container\")]\npublic partial class {className}Container : GeneratedContainer\n{{\n");
        T[] objects = Resources.LoadAll<T>(contentPath);
        foreach (var obj in objects)
        {
            content.Append($"public {className} {obj.name.ToPascalCase()};\n");
        }
        content.Append("#if UNITY_EDITOR\n");
        content.Append("public override void FindReferences()\n{");
        content.Append($"{className}[] objects = Resources.LoadAll<{className}>(\"{contentPath}\");\n");
        foreach (var obj in objects)
        {
            content.Append($"{obj.name.ToPascalCase()} = objects.Where(x=>x.name == \"{obj.name}\").First();\n");
        }
        content.Append("EditorUtility.SetDirty(this);\n");
        content.Append("}\n");
        content.Append("#endif\n");
        content.Append('}');

        string folderPath = GetFolderPath();
        try
        {
            if (File.Exists($"{folderPath}/{className}Container.cs"))
            {
                File.Delete($"{folderPath}/{className}Container.cs");
            }
            StreamWriter streamWriter = new($"{folderPath}/{className}Container.cs");
            streamWriter.Write(content.ToString());
            streamWriter.Close();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            Debug.LogError("File creation failed");
            Debug.LogError(e.Message);
            Debug.LogError(content.ToString());
        }
    }
#endif
}
