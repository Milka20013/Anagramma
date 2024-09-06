using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [HideInInspector] public int numberOfTiles;
    [SerializeField] private Tile prefab;
    [SerializeField] private Transform outline; //bounds of the grid
    private float scale;
    private float offsetX; //offsets of the elements' positions
    private float offsetY;
    private List<GameObject> tiles = new List<GameObject>();
    public List<string> tileStrings = new List<string>();
    public CharSet charSet;
    public string forcedString = "gomba"; // always have this word in grid
    private void Start()
    {
        numberOfTiles = width * height;
        scale = 1;
        if (outline.localScale.x < width)
        {
            scale = outline.localScale.x / width;
        }
        offsetX = outline.position.x - (width - 1) * scale / 2;
        offsetY = outline.position.y - (height - 1) * scale / 2;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        int[] indices = ForcedStringIndices();
        string forcedChar = " ";
        int j = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var posX = x * scale + offsetX;
                var posY = y * scale + offsetY;
                forcedChar = " ";
                if (indices.Contains(x * height + y))
                {
                    forcedChar = forcedString[j].ToString();
                    j++;
                }
                SpawnTile(new Vector3(posX, posY, 0),forcedChar);    
            }
        }
        ResetWeights();
    }
    public void ClearGrid()
    {
        foreach (var obj in tiles)
        {
            Destroy(obj);
        }
        tiles.Clear();
        tileStrings.Clear();
    }
    public void FillGap(Tile tile, string forcedChar = " ")
    {
        tiles.Remove(tile.gameObject);
        tileStrings.Remove(tile.characters.text);
        Vector3 position = tile.startPosition;
        Destroy(tile.gameObject);
        SpawnTile(position, forcedChar);
    }
    public Tile SpawnTile(Vector3 position, string forcedChar = " ")
    {
        var spawnedTile = Instantiate(prefab, position, Quaternion.identity);
        Tile spawnedTileScr = spawnedTile.GetComponent<Tile>();
        spawnedTile.transform.localScale = new Vector3(scale,scale,1);
        if (forcedChar != " ")
        {
            spawnedTileScr.characters.text = forcedChar;
        }
        else
        {
            spawnedTileScr.characters.text = charSet.GetWeightedChar().ToString();
        }
        spawnedTile.name = "Tile" + position.x + position.y;
        spawnedTileScr.CalculateDamageModifier(charSet.GetPositionBasedOnWeight(spawnedTileScr.characters.text[0]),charSet.charSet.Length);
        tiles.Add(spawnedTile.gameObject);
        tileStrings.Add(spawnedTile.GetComponent<Tile>().characters.text);
        return spawnedTile;
    }
    private int[] ForcedStringIndices()
    {
        int[] indices = new int[forcedString.Length];
        int j = 0;
        int num;
        if (forcedString.Length < numberOfTiles)
        {
            while (j < indices.Length)
            {
                num = Random.Range(0, numberOfTiles);
                if (!indices.Contains(num))
                {
                    indices[j] = num;
                    j++;
                }
            }
        }
        else
        {
            return new int[0];
        }
        return indices;
    }
    public void AdjustWeights()
    {
        float[] factors = new float[charSet.charSet.Length];
        foreach (var obj in tileStrings)
        {
            factors[charSet.GetIndex(obj[0])]++;
        }
        charSet.AdjustWeights(factors);
    }
    public void ResetWeights()
    {
        charSet.ResetWeights();
    }
}
