using HarmonyLib;
using System;
using UnityEngine;

public class Utils
{
    public static T[] SubArray<T>(T[] data, int index)
    {
        if (index >= data.Length)
            return new T[0];

        int length = data.Length - index;
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
}