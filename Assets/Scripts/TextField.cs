using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextField : MonoBehaviour
{
    public static TextField instance;
    [SerializeField] GridManager gridManager;
    [SerializeField] Cannon cannon;
    [SerializeField] Text dmgModifierText;
    [SerializeField] Text forceModifierText;
    private float damageModifier;
    private float forceModifier;
    private List<GameObject> attachedObjects = new List<GameObject>(); // objects of input string
    private List<string> charList = new List<string>(); // input string
    private string word; // input word
    [SerializeField] private float sizeThreshold; // after this many tiles it scales
    [HideInInspector] public string[] wordSet; // dictionary
    private void Awake()
    {
        instance = this;
    }
    /*public void AttachObject(GameObject obj)
    {
        attachedObjects.Add(obj);
        charList.Add(obj.GetComponent<Tile>().characters.text);
        word = Concat(charList);
        UpdateMultipliers();
        AlignElements();
    }
    public void DetachObject(GameObject obj)
    {
        charList.RemoveAt(attachedObjects.IndexOf(obj));
        word = Concat(charList);
        attachedObjects.Remove(obj);
        Tile tile = obj.GetComponent<Tile>();
        obj.transform.localScale = tile.startScale;
        obj.transform.position = tile.startPosition;
        UpdateMultipliers();
        AlignElements();
    }
    public void RemoveObjects() // just clear the textfield (all objects)
    {
        foreach (var obj in attachedObjects)
        {
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.transform.position = obj.GetComponent<Tile>().startPosition;
        }
        ClearTextFieldData();
    }
    public void DestroyCharacters() // destroy objects attached to the textfield (succesful scan)
    {
        gridManager.AdjustWeights();
        for (int i = 0; i < attachedObjects.Count; i++)
        {
            var obj = attachedObjects.ElementAt(i).GetComponent<Tile>();
            gridManager.FillGap(obj);
        }
        ClearTextFieldData();
        gridManager.ResetWeights();
    }
    private void ClearTextFieldData()
    {
        charList.Clear();
        word = "";
        attachedObjects.Clear();
    }
    public void AlignElements()
    {
        if (attachedObjects.Count == 0)
        {
            return;
        }
        float scale = AlignScale();
        Vector3 position = new Vector3(transform.position.x - (attachedObjects.Count - 1) * scale / 2, transform.position.y, -0.1f);
        foreach (var obj in attachedObjects)
        {
            MoveTowards(obj, position);
            position.x += scale;
        }
    }
    public void MoveTowards(GameObject obj, Vector3 position)
    {
        obj.transform.position = position;
    }
    public float AlignScale()
    {
        //float scale = attachedObjects.ElementAt(0).GetComponent<Tile>().startScale.x;
        float scale = 1;
        if (attachedObjects.Count * scale >= sizeThreshold)
        {
            scale = sizeThreshold / attachedObjects.Count;
        }
        foreach (var obj in attachedObjects)
        {
            obj.transform.localScale = new Vector3(scale, scale, 1);
        }
        return scale;
    }
    private string Concat(List<string> list)
    {
        string str = "";
        foreach (var item in list)
        {
            str += item;
        }
        return str;
    }
    public void ScanWord()
    {
        int i = Array.BinarySearch(wordSet, word);
        if (i < 0)
        {
            WordNotFound();
        }
        else
        {
            WordFound();
        }
    }
    private void WordNotFound()
    {
        Debug.Log("Not found");
    }
    private void WordFound()
    {
        Debug.Log("Found");
        //float multiplier = CalculateMultiplier();
        cannon.Shoot(forceModifier, damageModifier);
        GameManager.AddUniqueWord(word);
        DestroyCharacters();
    }*/
}
