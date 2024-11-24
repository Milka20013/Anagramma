using System.Collections.Generic;
using System.Linq;
public class CharSet
{
    public string alphabet;
    public Dictionary<char, float> charWeights;
    private readonly Dictionary<char, float> initialCharWeights;
    public CharSet(string alphabet, Dictionary<char, float> charWeights)
    {
        this.alphabet = alphabet;
        this.charWeights = charWeights;
        initialCharWeights = new(charWeights);
    }
    public string GetWeightedStr()
    {
        //to-do : balanced weight reduction
        var weights = charWeights.Values.ToArray();
        char c = Utilitity.RandomElementFromWeightedTable(alphabet, weights);
        AdjustWeights(c);
        return c.ToString();
    }
    public void ResetWeights()
    {
        charWeights = new(initialCharWeights);
    }
    public void AdjustWeights(char c)
    {

    }
}
