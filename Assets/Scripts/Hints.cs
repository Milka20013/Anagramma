using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    public static Hints instance;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text textInPanel;
    [SerializeField] InputField inputField;
    [HideInInspector] public Tile tileToSwap;
    [HideInInspector] public bool isSwapping = false;
    private void Awake()
    {
        instance = this;
    }
    public void ActivatePanel(bool active, string character)
    {
        textInPanel.text = "Swap your character\n" + character + " to:";
        panel.SetActive(active);
    }
    public void SetIsSwapping()
    {
        if (isSwapping)
        {
            isSwapping = false;
        }
        else
        {
            isSwapping = true;
        }
    }
    public void SetTileToSwap(Tile tile)
    {
        tileToSwap = tile;
    }
    public void SwapTiles(Tile tile,string c)
    {
        if (gridManager.charSet.charSet.Contains(c) && tileToSwap != null)
        {
            gridManager.FillGap(tile, c);
            inputField.text = "";
            panel.SetActive(false);
            isSwapping = false;
        }
        else
        {
            Debug.Log("Not valid character");
            isSwapping = false;
        }
    }
    public void ReadChar()
    {
        if (inputField.text == "")
        {
            return;
        }
        SwapTiles(tileToSwap, inputField.text);
    }
}
