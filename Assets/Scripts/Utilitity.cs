using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Utilitity
{
    public static System.Random rnd = new();
    public static Transform ClosestToPoint(Vector3 point, Transform[] objects)
    {
        if (objects == null || objects.Length == 0)
        {
            return null;
        }
        return objects[ClosestsIndexToPoint(point, objects)];
    }

    public static GameObject ClosestToPoint(Vector3 point, GameObject[] objects)
    {
        if (objects == null || objects.Length == 0)
        {
            return null;
        }
        return objects[ClosestsIndexToPoint(point, objects.Where(x => x != null).Select(x => x.transform).ToArray())];
    }

    public static Collider2D ClosestToPoint(Vector3 point, Collider2D[] objects)
    {
        if (objects == null || objects.Length == 0)
        {
            return null;
        }
        return objects[ClosestsIndexToPoint(point, objects.Where(x => x != null).Select(x => x.transform).ToArray())];
    }
    public static Collider ClosestToPoint(Vector3 point, Collider[] objects)
    {
        if (objects == null || objects.Length == 0)
        {
            return null;
        }
        return objects[ClosestsIndexToPoint(point, objects.Where(x => x != null).Select(x => x.transform).ToArray())];
    }
    private static int ClosestsIndexToPoint(Vector3 point, Transform[] points)
    {
        float minDist = float.PositiveInfinity;
        int index = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == null)
            {
                continue;
            }
            float dist = Vector3.Distance(points[i].position, point);
            if (dist < minDist)
            {
                minDist = dist;
                index = i;
            }
        }
        return index;
    }

    public static int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    public static T RandomElement<T>(ICollection<T> collection)
    {
        int number = rnd.Next(collection.Count);
        return collection.ElementAt(number);
    }
    public static char RandomElement(string str)
    {
        int number = rnd.Next(str.Length);
        return str[number];
    }

    public static string ShuffleString(string str)
    {
        int[] indices = new int[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            indices[i] = i;
        }
        indices = indices.Shuffle().ToArray();
        StringBuilder builder = new(str.Length);
        foreach (var index in indices)
        {
            builder.Append(str[index]);
        }
        return builder.ToString();
    }

    public static bool RandomTrue(decimal chance, bool percentage = false)
    {
        if (percentage)
        {
            chance /= 100;
        }
        if (chance <= 0m)
        {
            return false;
        }
        if (chance >= 1m)
        {
            return true;
        }
        byte[] bytes = new byte[8];
        rnd.NextBytes(bytes);
        ulong number = System.BitConverter.ToUInt64(bytes, 0);
        ulong shiftedChance = (ulong)(chance * ulong.MaxValue);
        return number <= shiftedChance;
    }
    public static bool RandomTrue(float chance, bool percentage = false)
    {
        return RandomTrue((decimal)chance, percentage);
    }
    public static T RandomElementFromFairTableExcept<T>(IEnumerable<T> table, IEnumerable<T> exceptElements)
    {
        List<T> newTable = new();
        foreach (var item in table)
        {
            if (!exceptElements.Contains(item))
            {
                if (item == null)
                {
                    continue;
                }
                newTable.Add(item);
            }
        }
        return RandomElement(newTable);
    }
    public static int[] CreateSequenceOfRange((int, int) tailHead, int increment = 1)
    {
        if (tailHead.Item1 == tailHead.Item2)
        {
            return new[] { tailHead.Item1, tailHead.Item2 };
        }
        int count = Mathf.CeilToInt((tailHead.Item2 - tailHead.Item1) / increment) + 1;
        int[] array = new int[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = tailHead.Item1 + increment * i;
        }
        return array;
    }

    public static T RandomElementFromWeightedTableNormalized<T>(IEnumerable<T> table, float[] weights)
    {
        float rndValue = Random.value;
        int rndIndex = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            rndValue = rndValue - weights[i];
            if (rndValue <= 0f)
            {
                rndIndex = i;
                break;
            }
        }
        return table.ElementAt(rndIndex);
    }

    public static T RandomElementFromWeightedTable<T>(IEnumerable<T> table, float[] weights)
    {
        double sum = weights.Sum();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = (float)(weights[i] / sum);
        }
        return RandomElementFromWeightedTableNormalized(table, weights);
    }
}
