// File: Utilities.cs
// Author: Brendan Robinson
// Date Created: 08/04/2018
// Date Last Modified: 01/19/2019
// Description: 

using System;
using System.Collections;
using System.IO;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

// Helper class
public static class Utilities
{
    public static bool Chance(float chance) { return Random.value <= chance; }
    public static bool CoinFlip() { return Random.value <= 0.5f; }

    public static int Mod(int k, int n)
    {
        return (k %= n) < 0 ? k + n : k;
    }

    public static Vector2Int MousePosition() {
        Vector2 mousePos = MousePositionExact();
        return new Vector2Int((int)mousePos.x, (int)mousePos.y);
    }

    public static Vector2 MousePositionExact() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

    public static Vector2 RandomPointInCircle(float radius)
    {
        float t = Mathf.PI * 2 * Random.value;
        float u = Random.value + Random.value;
        float r;
        if (u > 1)
        {
            r = 2 - u;
        }
        else
        {
            r = u;
        }

        return new Vector2(radius * r * Mathf.Cos(t * Mathf.Rad2Deg), radius * r * Mathf.Sin(t * Mathf.Rad2Deg));
    }

    public static float NextGaussianDouble(float mean, float sigma)
    {
        float u, v, s;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            s = u * u + v * v;
        } while (s >= 1.0);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);

        return u * fac * sigma + mean;
    }

    public static float NormalizedRandom(float minValue, float maxValue)
    {
        float mean = (minValue + maxValue) / 2f;
        float sigma = (maxValue - mean) / 3f;
        float result;
        do
        {
            result = NextGaussianDouble(mean, sigma);
        } while (result > maxValue || result < minValue);

        return result;
    }

    #region Extensions

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }

    public static void Invoke(this MonoBehaviour me, Action theDelegate, float time)
    {
        me.StartCoroutine(ExecuteAfterTime(theDelegate, time));
    }

    private static IEnumerator ExecuteAfterTime(Action theDelegate, float delay)
    {
        yield return new WaitForSeconds(delay);
        theDelegate();
    }

    public static void FlipX(this Transform transform)
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }


    public static void PositiveX(this Transform transform)
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    public static void NegativeX(this Transform transform)
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    public static void FlipY(this Transform transform)
    {
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
    public static void FlipZ(this Transform transform)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
    }
    private static Collider2D[] colliders = new Collider2D[64];

    public static Transform GetFirstCollision(this Collider2D collider, string tag)
    {
        int numContacts = collider.GetContacts(colliders);

        for (int i = 0; i < numContacts; i++)
        {
            if (colliders[i].tag.Equals(tag))
            {
                return colliders[i].transform;
            }
        }

        return null;
    }
    #endregion


#if UNITY_EDITOR

    [MenuItem("Utilities/Update Item Data")]
    public static void UpdateItemData()
    {
        string path = "Assets/Resources/Items/ItemID.cs";

        using (FileStream stream = new FileStream(path, FileMode.Truncate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write("public class ItemID {\n");

                ItemData[] itemList = Resources.LoadAll<ItemData>("Items");
                for (int i = 0; i < itemList.Length; i++)
                {
                    writer.Write("public const int " + RemoveWhitespace(itemList[i].name) + " = " +
                                 itemList[i].id + ";\n");

                    itemList[i].Awake();
                    itemList[i].UpdateName();
                }

                writer.Write("}");
                writer.Close();
                AssetDatabase.SaveAssets();
            }
        }
    }

    [MenuItem("Assets/Unload Assets")]
    public static void UnloadAssets()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

#endif
}
