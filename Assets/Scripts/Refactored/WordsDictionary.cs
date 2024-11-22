using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Words Dictionary")]
public class WordsDictionary : ScriptableObject
{
    public string language;
    public string charSet;
    public Dictionary<char, float> charWeights;
    [HideInInspector] public string[] words;
}
