using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager instance;

    [SerializeField] private string dedicatedDictionaryPath;
    [SerializeField] private string resourcesSOFolderPath;

    [SerializeField] private WordsDictionary _wordsDictionary;
    public WordsDictionary WordsDictionary
    {
        get
        {
            return _wordsDictionary;
        }
        set
        {
            CharSet = new(value.alphabet, value.CharWeights);
            _wordsDictionary = value;
        }
    }
    private CharSet _charSet;
    public CharSet CharSet
    {
        get
        {
            _charSet ??= new(WordsDictionary.alphabet, WordsDictionary.CharWeights);
            return _charSet;
        }
        private set
        {
            _charSet = value;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            this.SingletonWarning();
        }
        instance = this;
        ScanDedicatedFolderForDictionaries();
    }

    public void ScanDedicatedFolderForDictionaries()
    {
        string[] fileNames = Directory.GetFiles(dedicatedDictionaryPath);
        for (int i = 0; i < fileNames.Length; i++)
        {
            if (Path.GetExtension(fileNames[i]) != ".txt")
            {
                continue;
            }
            string fileName = Path.GetFileNameWithoutExtension(fileNames[i]);
            if (File.Exists(resourcesSOFolderPath + fileName + ".asset"))
            {
                continue;
            }
            WordsDictionary = CreateWordsDictionaryFromFile(fileNames[i], resourcesSOFolderPath);
        }
    }
    public static WordsDictionary CreateWordsDictionaryFromFile(string filePath, string locationPath)
    {
        string[] words = File.ReadAllLines(filePath);
        WordsDictionary wordsDict = ScriptableObject.CreateInstance<WordsDictionary>();

        wordsDict.words = words.OrderBy(x => x).ToArray();
        wordsDict.alphabet = GetOrderedCharsetOfWords(words);
        wordsDict.CharWeights = GetOrderedCharWeightsOfWords(words);

        string fileName = Path.GetFileNameWithoutExtension(filePath);
        wordsDict.language = fileName.Split('_').Last();
        AssetDatabase.CreateAsset(wordsDict, locationPath + fileName + ".asset");
        AssetDatabase.SaveAssets();
        Debug.Log($"{filePath} was created, and put into {locationPath}");
        return wordsDict;
    }

    public static string GetOrderedCharsetOfWords(string[] words)
    {
        StringBuilder builder = new(200);
        HashSet<char> foundChars = new();
        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < words[i].Length; j++)
            {
                char c = words[i][j];
                if ((int)c < 21)
                {
                    continue;
                }
                if (foundChars.Contains(c))
                {
                    continue;
                }
                foundChars.Add(c);
                builder.Append(c);
            }
        }
        return new string(builder.ToString().OrderBy(x => x).ToArray());
    }

    public static Dictionary<char, float> GetOrderedCharWeightsOfWords(string[] words)
    {
        Dictionary<char, float> dict = new();
        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < words[i].Length; j++)
            {
                char c = words[i][j];
                if (dict.ContainsKey(c))
                {
                    dict[c]++;
                    continue;
                }
                dict.Add(c, 1);
            }
        }
        return dict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }
}
