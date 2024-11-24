using System;
using System.Collections.Generic;
using UnityEngine;

public class TileField : MonoBehaviour
{
    [SerializeField] private GameEventContainer eventContainer;
    private List<Tile> attachedTiles = new();
    private DictionaryManager dictionaryManager;
    private string word = "";

    private void Start()
    {
        dictionaryManager = DictionaryManager.instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScanWord();
        }
    }
    public void OnTileClicked(object tileObj)
    {
        Tile tile = (Tile)tileObj;
        if (attachedTiles.Contains(tile))
        {
            DetachObject(tile);
            return;
        }
        AttachTile(tile);
    }
    public void AttachTile(Tile tile)
    {
        attachedTiles.Add(tile);
        tile.transform.SetParent(transform);
    }
    public void DetachObject(Tile tile)
    {
        attachedTiles.Remove(tile);
        tile.ResetPosition();
    }
    public void ClearField()
    {
        foreach (var tile in attachedTiles)
        {
            tile.ResetPosition();
        }
        attachedTiles = new();
    }
    public void DestroyTiles() // destroy objects attached to the textfield (succesful scan)
    {
        foreach (var tile in attachedTiles)
        {
            tile.DestroyTile();
        }
        ClearField();

    }
    public void ScanWord()
    {
        UpdateWordFromTiles();
        int i = Array.BinarySearch(dictionaryManager.WordsDictionary.words, word);
        if (i < 0)
        {
            WordNotFound();
        }
        else
        {
            WordFound();
        }
    }

    public void UpdateWordFromTiles()
    {
        word = "";
        foreach (var tile in attachedTiles)
        {
            word += tile.Characters;
        }
    }
    private void WordNotFound()
    {
        Debug.Log($"{word} not found");
    }
    private void WordFound()
    {
        Debug.Log($"{word} Found");
        DestroyTiles();
        eventContainer.successfulWordScan.RaiseEvent(word);
    }
}
