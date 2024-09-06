using UnityEngine;

public class AnagrammScanner : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private string[] words;
    public string searchedWord;
    private int longestWordLength;
    private void Start()
    {
        words = TextField.instance.wordSet;
        longestWordLength = gridManager.numberOfTiles;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SearchAllWords();
        }
    }
    public void SearchLongestWord()
    {
        for (int i = longestWordLength; i >= 3; i--)
        {
            ScanForWord(i);
            if (searchedWord != "NOT FOUND")
            {
                break;
            }
        }
    }
    public void SearchAllWords()
    {
        string[] characters = gridManager.tileStrings.ToArray();
        char[] wordChars;
        CharSet charSet = gridManager.charSet;
        int abcLength = gridManager.charSet.charSet.Length;
        int[] charsInInt = new int[abcLength];
        bool tester = true;
        int[] numberOfWordsByLength = new int[gridManager.numberOfTiles];
        for (int i = 0; i < characters.Length; i++)
        {
            int index = charSet.GetIndex(characters[i][0]);
            charsInInt[index]++;
        }
        for (int i = 0; i < words.Length; i++)
        {
            wordChars = words[i].ToCharArray();
            int[] wordInInt = new int[abcLength];
            for (int l = 0; l < wordChars.Length; l++)
            {
                int index = charSet.GetIndex(wordChars[l]);
                wordInInt[index]++;
            }
            for (int k = 0; k < wordInInt.Length; k++)
            {
                if (wordInInt[k] > charsInInt[k])
                {
                    tester = false;
                    break;
                }
            }
            if (tester)
            {
                numberOfWordsByLength[words[i].Length-1]++;
            }
            tester = true;
        }
        int sum = 0;
        for (int i = 0; i < numberOfWordsByLength.Length; i++)
        {
            sum += numberOfWordsByLength[i];
            Debug.Log(numberOfWordsByLength[i]);
        }
        Debug.Log(sum);
    }
    public void ScanForWord(int input)
    {
        string[] characters = gridManager.tileStrings.ToArray();
        char[] wordChars;
        CharSet charSet = gridManager.charSet;
        int abcLength = gridManager.charSet.charSet.Length;
        int[] charsInInt = new int[abcLength];
        bool tester = true;
        for (int i = 0; i < characters.Length; i++)
        {
            int index = charSet.GetIndex(characters[i][0]);
            charsInInt[index]++;
        }
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length == input)
            {
                wordChars = words[i].ToCharArray();
                int[] wordInInt = new int[abcLength];
                for (int l = 0; l < wordChars.Length; l++)
                {
                    int index = charSet.GetIndex(wordChars[l]);
                    wordInInt[index]++;
                }
                for (int k = 0; k < wordInInt.Length; k++)
                {
                    if (wordInInt[k] > charsInInt[k])
                    {
                        tester = false;
                        break;
                    }
                }
                if (tester)
                {
                    searchedWord = words[i];
                    return;
                }
                tester = true;
            }
        }
        searchedWord = "NOT FOUND";
    }
    public void ScanForWord(ParametersForbuttons input)
    {
        string[] characters = gridManager.tileStrings.ToArray();
        char[] wordChars;
        CharSet charSet = gridManager.charSet;
        int abcLength = gridManager.charSet.charSet.Length;
        int[] charsInInt = new int[abcLength];
        bool tester = true;
        bool includeChar = input.searchedChar != '.';
        for (int i = 0; i < characters.Length; i++)
        {
            int index = charSet.GetIndex(characters[i][0]);
            charsInInt[index]++;
        }
        if (includeChar)
        {
            if (charsInInt[charSet.GetIndex(input.searchedChar)] == 0)
            {
                searchedWord = "NOT FOUND";
                return;
            }
        }
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length == input.length)
            {
                wordChars = words[i].ToCharArray();
                int[] wordInInt = new int[abcLength];
                for (int l = 0; l < wordChars.Length; l++)
                {
                    int index = charSet.GetIndex(wordChars[l]);
                    wordInInt[index]++;
                }
                if (includeChar)
                {
                    if (wordInInt[charSet.GetIndex(input.searchedChar)] == 0)
                    {
                        continue;
                    }
                }
                for (int k = 0; k < wordInInt.Length; k++)
                {
                    if (wordInInt[k] > charsInInt[k])
                    {
                        tester = false;
                        break;
                    }
                }
                if (tester)
                {
                    searchedWord = words[i];
                    return;
                }
                tester = true;
            }
        }
        searchedWord = "NOT FOUND";
    }
}
