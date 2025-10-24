using HarmonyLib;
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
}