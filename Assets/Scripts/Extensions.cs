using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static string AsRoundStr(this float value, int decimalPoints = 1)
    {
        return Math.Round(value, decimalPoints).ToString();
    }

    public static string ToFirstLower(this string value)
    {
        char first = char.ToLower(value[0]);
        string str = first + value.Remove(0, 1);
        return str;
    }

    public static string ToPascalCase(this string value)
    {
        value = value.ToFirstLower();
        var parts = value.Split();
        return string.Join("", parts);
    }

    public static void SingletonWarning(this MonoBehaviour scr)
    {
        Debug.LogWarning($"Multiple instances of {scr.GetType().Name} was found.");
    }

    public static IList<T> Shuffle<T>(this IEnumerable<T> sequence)
    {
        return sequence.Shuffle(new System.Random());
    }

    public static IList<T> Shuffle<T>(this IEnumerable<T> sequence, System.Random randomNumberGenerator)
    {
        if (sequence == null)
        {
            throw new ArgumentNullException("sequence");
        }

        if (randomNumberGenerator == null)
        {
            throw new ArgumentNullException("randomNumberGenerator");
        }

        T swapTemp;
        List<T> values = sequence.ToList();
        int currentlySelecting = values.Count;
        while (currentlySelecting > 1)
        {
            int selectedElement = randomNumberGenerator.Next(currentlySelecting);
            --currentlySelecting;
            if (currentlySelecting != selectedElement)
            {
                swapTemp = values[currentlySelecting];
                values[currentlySelecting] = values[selectedElement];
                values[selectedElement] = swapTemp;
            }
        }

        return values;
    }
}
