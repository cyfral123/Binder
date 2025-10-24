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

    public static void EnableCheatsSilently()
    {
        AccessTools.Field(typeof(CommandConsole), "cheatsEnabled")?.SetValue(null, true);
        AccessTools.Field(typeof(CommandConsole), "hasCheated")?.SetValue(null, true);

        var uiManagerType = typeof(CL_UIManager);
        var instanceField = AccessTools.Field(uiManagerType, "instance");
        var instance = instanceField?.GetValue(null);

        if (instance != null)
        {
            var cheatTrackerField = AccessTools.Field(uiManagerType, "cheatTracker");
            var tracker = cheatTrackerField?.GetValue(instance) as GameObject;
            tracker?.SetActive(true);
        }
    }
}