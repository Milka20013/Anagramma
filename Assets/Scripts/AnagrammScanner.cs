using System;
using System.Collections.Generic;
using System.Linq;

public static class AnagrammScanner
{
    public static string SearchLongestWord(string characters, WordsDictionary wordsDict, int maxLen, int minLen = 3)
    {
        string word = "";
        characters = SortStringByWeights(characters, wordsDict.CharWeights);
        for (int i = maxLen; i >= minLen; i--)
        {
            word = ScanForWordWithLength(characters, wordsDict, i);
            if (word != "")
            {
                return word;
            }
        }
        return word;
    }
    public static string ScanForWordWithLength(string characters, WordsDictionary wordsDict, int length)
    {
        Dictionary<char, int> charsDict = new();
        for (int i = 0; i < characters.Length; i++)
        {
            if (!charsDict.TryAdd(characters[i], 1))
            {
                charsDict[characters[i]]++;
            }
        }
        char[] charsArr = charsDict.Keys.ToArray();
        int[] charValues = charsDict.Values.ToArray();
        string[] words = wordsDict.words;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length != length)
            {
                continue;
            }
            if (IsAnagram(charsArr, charValues, words[i]))
            {
                return words[i];
            }
        }
        return "";
    }

    public static List<string> ScanAllWords(string characters, WordsDictionary wordsDict)
    {
        characters = SortStringByWeights(characters, wordsDict.CharWeights);
        Dictionary<char, int> charsDict = new();
        for (int i = 0; i < characters.Length; i++)
        {
            if (!charsDict.TryAdd(characters[i], 1))
            {
                charsDict[characters[i]]++;
            }
        }
        char[] charsArr = charsDict.Keys.ToArray();
        int[] charValues = charsDict.Values.ToArray();
        string[] words = wordsDict.words;
        List<string> acceptedWords = new(500);
        for (int i = 0; i < words.Length; i++)
        {
            if (IsAnagram(charsArr, charValues, words[i]))
            {
                acceptedWords.Add(words[i]);
            }
        }
        return acceptedWords;
    }

    public static bool IsAnagram(char[] characters, int[] charValuesArr, string word)
    {
        if (word.Length > characters.Length)
        {
            return false;
        }
        int[] charValues = new int[charValuesArr.Length];
        charValuesArr.CopyTo(charValues, 0);
        int index;
        for (int i = 0; i < word.Length; i++)
        {
            index = Array.IndexOf(characters, word[i]);
            if (index < 0)
            {
                return false;
            }
            if (charValues[index] == 0)
            {
                return false;
            }
            charValues[index]--;
        }
        return true;
    }
    private static string SortStringByWeights(string str, Dictionary<char, float> weights)
    {
        char[] arr = new char[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            arr[i] = str[i];
        }
        Array.Sort(arr, (x, y) => weights[x].CompareTo(weights[y]));
        return new string(arr);
    }
    private static void FillCharArrayWithString(string chars, char[] arr)
    {
        for (int i = 0; i < chars.Length; i++)
        {
            arr[i] = chars[i];
        }
    }
}
