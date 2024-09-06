using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextReader : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private TextField textField;
    public string language; //language of the game
    private CharSet charSet;
    [SerializeField] private TextAsset characterInfos; //abc of languages
    [SerializeField] private TextAsset[] textsOfWords; //words of languages
    private void Awake()
    {
        string[] charContent = characterInfos.text.Split('\r');
        int index = 0;
        for (int i = 0; i < charContent.Length; i++)
        {
            if ((int)charContent[i][0] <= 21)
            {
                charContent[i] = charContent[i].Remove(0, 1);
            }
        }
        for (int i = 0; i < charContent.Length; i++)
        {
            if (charContent[i] == language)
            {
                index = i + 1;
                break;
            }
        }
        string[] weightsS = charContent[index + 1].Split(':');
        float[] weightsF = new float[weightsS.Length];
        for (int i = 0; i < weightsF.Length; i++)
        {
            weightsF[i] = float.Parse(weightsS[i]);
        }
        charSet = new CharSet(charContent[index], weightsF);
        gridManager.charSet = charSet;
        for (int i = 0; i < textsOfWords.Length; i++)
        {
            if (textsOfWords[i].name.Substring(0,3) == language)
            {
                index = i;
                break;
            }
        }
        string[] wordContent = textsOfWords[index].text.Split('\r');
        for (int i = 0; i < wordContent.Length; i++)
        {
            if (charSet.GetIndex(wordContent[i][0]) < 0)
            {
                wordContent[i] = wordContent[i].Remove(0, 1);
            }
        }
        textField.wordSet = wordContent;
    }
}
