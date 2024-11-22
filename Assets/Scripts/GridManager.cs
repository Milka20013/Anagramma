using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    public int NumberOfTiles
    {
        get
        {
            return width * height;
        }
    }
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject tileContainer;

    private List<Tile> tiles = new();
    [SerializeField] private string forcedString = "gomba";
    private void Start()
    {
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        List<(Vector2, string)> tilesToSpawn = new();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string forcedStr = "";
                int flattenedIndex = y * width + x;
                if (flattenedIndex < forcedString.Length)
                {
                    forcedStr += forcedString[flattenedIndex];
                }
                tilesToSpawn.Add(new(new(x, y), forcedStr));
            }
        }
        tilesToSpawn = tilesToSpawn.Shuffle().ToList();
        foreach (var tile in tilesToSpawn)
        {
            SpawnTile(tile.Item1, tile.Item2);
        }
    }

    public void ShuffleTiles()
    {
        Transform[] children = new Transform[tileContainer.transform.childCount];
        int[] indices = new int[children.Length];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = tileContainer.transform.GetChild(i);
            indices[i] = i;
        }
        indices = indices.Shuffle().ToArray();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetSiblingIndex(indices[i]);
        }
    }
    public void ClearGrid()
    {
        foreach (var obj in tiles)
        {
            Destroy(obj);
        }
        tiles.Clear();
    }
    public void FillGap(Tile tile, string forcedChar = " ")
    {
        /*tiles.Remove(tile.gameObject);
        tileStrings.Remove(tile.Characters);
        Vector3 position = tile.startPosition;
        Destroy(tile.gameObject);
        SpawnTile(position, forcedChar);*/
    }
    public void SpawnTile(Vector2 pos, string forcedChar = "")
    {
        var spawnedTile = Instantiate(prefab, tileContainer.transform);
        var spawnedTileScr = spawnedTile.GetComponent<Tile>();
        spawnedTileScr.InitTile(forcedChar);
        spawnedTile.name = $"Tile-{pos.x}-{pos.y}";
        tiles.Add(spawnedTileScr);
    }
}
