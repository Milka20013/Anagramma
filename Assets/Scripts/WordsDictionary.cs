using System.Collections.Generic;
using UnityEngine;

public class WordsDictionary : ScriptableObject
{
    public string language;
    public string alphabet;
    private Dictionary<char, float> _charWeights;
    public Dictionary<char, float> CharWeights
    {
        get
        {
            _charWeights ??= DictionaryManager.GetOrderedCharWeightsOfWords(words);
            return _charWeights;
        }
        set
        {
            _charWeights = value;
        }
    }
    [HideInInspector] public string[] words;
}
