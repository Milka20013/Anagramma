using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    public static HashSet<string> uniqueWords = new HashSet<string>();
    public string[] wordSet;
    private void Update()
    {
    }
    public static void AddUniqueWord(string word) // add to the list where the distinct words are
    {
        int tmp = uniqueWords.Count;
        uniqueWords.Add(word);
        if (tmp != uniqueWords.Count && uniqueWords.Count > PlayerPrefs.GetInt("uniqueWordCount"))
        {
            PlayerPrefs.SetInt("uniqueWordCount", uniqueWords.Count);
            Debug.Log("ok");
        }
    }
    public void ResetValue()
    {
        PlayerPrefs.SetInt("uniqueWordCount", 0);
        PlayerPrefs.SetInt("playerCurrency_Diamonds", 0);
    }
}
