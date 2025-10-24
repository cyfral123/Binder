using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ConsoleHelper
{
    private static MethodInfo addMessageMethod = AccessTools.Method(typeof(CommandConsole), "AddMessageToHistory");

    private static FieldInfo cheatsEnabledField = AccessTools.Field(typeof(CommandConsole), "cheatsEnabled");
    private static FieldInfo hasCheatedField = AccessTools.Field(typeof(CommandConsole), "hasCheated");

    public static void AddMessage(string message)
    {
        if (CommandConsole.instance != null && addMessageMethod != null)
        {
            addMessageMethod.Invoke(CommandConsole.instance, new object[] { message });
        }
    }

    public static void EnableCheatsSilently()
    {
        if (CommandConsole.instance != null)
        {
            cheatsEnabledField?.SetValue(CommandConsole.instance, true);
            hasCheatedField?.SetValue(CommandConsole.instance, true);
        }

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

    public static bool ExecuteCommandSilenty(string commandLine)
    {
        if (CommandConsole.instance == null)
            return false;

        string[] parts = commandLine.Split(' ');
        if (parts.Length == 0)
            return false;

        string cmdName = parts[0].ToLower();
        string[] args = new string[Mathf.Max(parts.Length - 1, 0)];
        Array.Copy(parts, 1, args, 0, args.Length);

        var cmdDict = Traverse.Create(CommandConsole.instance)
                              .Field("commands")
                              .GetValue<Dictionary<string, CommandConsole.Command>>();

        if (!cmdDict.TryGetValue(cmdName, out var cmd))
            return false;

        try
        {
            SilentLogController.SuppressLogging = true;
            cmd.callback.Invoke(args);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Binder: Failed to execute {cmdName}: {ex}");
        }
        finally
        {
            SilentLogController.SuppressLogging = false;
        }

        return true;
    }
}