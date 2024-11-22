using System.Linq;
using UnityEngine;
[System.Serializable]
public class CharSet
{
    public string charSet = "aábcdeéfghiíjklmnoóöõpqrstuúüûvwxyz";
    public float[] weights;
    private float[] startingWeights;
    private float corrector = 1.4f;
    private float hardCorrector = 2f;
    public CharSet(string charSet, float[] weights)
    {
        this.charSet = charSet;
        SetWeights(weights);
    }
    public char GetWeightedChar()
    {
        //to-do : balanced weight reduction
        float num = Random.Range(0, weights.Sum());
        int index = 0;
        for (int i = 0; i < charSet.Length; i++)
        {
            num -= weights[i];
            if (num <= 0)
            {
                weights[i] /= corrector;
                index = i;
                break;
            }
        }
        return charSet[index];
    }
    public void ResetWeights()
    {
        startingWeights.CopyTo(weights, 0);
    }
    public void SetWeights(float[] weights)
    {
        this.weights = weights;
        startingWeights = new float[weights.Length];
        weights.CopyTo(startingWeights, 0);
    }
    public int GetIndex(char c)
    {
        for (int i = 0; i < charSet.Length; i++)
        {
            if (charSet[i] == c)
            {
                return i;
            }
        }
        return -1;
    }
    public void AdjustWeights(float[] factors)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] /= Mathf.Pow(hardCorrector, factors[i]);
        }
    }
    public int GetPositionBasedOnWeight(char c)
    {
        int index = GetIndex(c);
        float weight = startingWeights[index];
        int position = 1;
        for (int i = 0; i < startingWeights.Length; i++)
        {
            if (weight < startingWeights[i])
            {
                position++;
            }
        }
        return position;
    }
}
