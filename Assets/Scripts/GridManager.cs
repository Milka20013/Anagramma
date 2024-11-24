using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tileSlotPrefab;
    [SerializeField] private GameObject tileSlotContainer;
    private DictionaryManager dictionaryManager;

    private List<Tile> tiles = new();
    private List<TileSlot> tileSlots = new();
    [SerializeField] private string forcedString = "gomba";
    private void Start()
    {
        dictionaryManager = DictionaryManager.instance;
        GenerateGrid();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            var strs = tiles.Select(x => x.Characters).ToArray();
            string chars = "";
            foreach (var str in strs)
            {
                chars += str;
            }
            Debug.Log(AnagrammScanner.SearchLongestWord(chars, dictionaryManager.WordsDictionary, 10));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            var strs = tiles.Select(x => x.Characters).ToArray();
            string chars = "";
            foreach (var str in strs)
            {
                chars += str;
            }
            var words = AnagrammScanner.ScanAllWords(chars, dictionaryManager.WordsDictionary);
            Dictionary<int, int> numberOfWordsWithLength = new();
            foreach (var word in words)
            {
                if (!numberOfWordsWithLength.TryAdd(word.Length, 1))
                {
                    numberOfWordsWithLength[word.Length]++;
                }
            }
            foreach (var item in numberOfWordsWithLength)
            {
                Debug.Log(item.Key + " " + item.Value);
            }
            var wordsWithLen = words.Where(x => x.Length == numberOfWordsWithLength.Keys.Max()).ToArray();
            foreach (var item in wordsWithLen)
            {
                Debug.Log(item);
            }
        }
    }
    public void GenerateGrid()
    {
        tileSlotContainer.GetComponent<GridLayoutGroup>().constraintCount = height;
        List<(string, string)> tilesToSpawn = new();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string forcedStr;
                int flattenedIndex = y * width + x;
                if (flattenedIndex < forcedString.Length)
                {
                    forcedStr = forcedString[flattenedIndex].ToString();
                    dictionaryManager.CharSet.RemoveCharWeight(forcedString[flattenedIndex]);
                }
                else
                {
                    forcedStr = dictionaryManager.CharSet.GetWeightedStr().ToString();
                }
                tilesToSpawn.Add(new(x.ToString() + "-" + y.ToString(), forcedStr));
            }
        }
        tilesToSpawn = tilesToSpawn.Shuffle().ToList();

        SpawnSlots();

        foreach (var tile in tilesToSpawn)
        {
            SpawnTile(tile.Item1, tile.Item2);
        }
    }

    private void SpawnSlots()
    {
        for (int i = 0; i < NumberOfTiles; i++)
        {
            tileSlots.Add(Instantiate(tileSlotPrefab, tileSlotContainer.transform).GetComponent<TileSlot>());
        }
    }

    public void ShuffleTiles()
    {
        Transform[] children = new Transform[tileSlotContainer.transform.childCount];
        int[] indices = new int[children.Length];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = tileSlotContainer.transform.GetChild(i);
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
        foreach (var tile in tiles)
        {
            tile.DestroyTile();
        }
        tiles.Clear();
    }
    public void OnSuccessfulWordScan(object wordObj)
    {
        RefillGrid();
    }
    public void RefillGrid()
    {
        int refillAmount = tiles.RemoveAll(x => x == null || x.isDead);
        for (int i = 0; i < refillAmount; i++)
        {
            SpawnTile("Refilled", dictionaryManager.CharSet.GetWeightedStr().ToString());
        }
    }

    public Transform GetFirstEmptySlot()
    {
        foreach (var slot in tileSlots)
        {
            if (slot.IsEmpty())
            {
                return slot.transform;
            }
        }
        return null;
    }
    public void SpawnTile(string tileName, string forcedChar = "")
    {
        var parent = GetFirstEmptySlot();
        var spawnedTile = Instantiate(tilePrefab, parent);
        var spawnedTileScr = spawnedTile.GetComponent<Tile>();
        if (parent.TryGetComponent(out TileSlot slot))
        {
            slot.SetTile(spawnedTileScr);
        }
        spawnedTileScr.InitTile(forcedChar);
        spawnedTile.name = $"Tile-{tileName}";
        tiles.Add(spawnedTileScr);
    }
}
