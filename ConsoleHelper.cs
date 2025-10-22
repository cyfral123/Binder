using HarmonyLib;
using System.Reflection;

public static class ConsoleHelper
{
    private static MethodInfo addMessageMethod = AccessTools.Method(typeof(CommandConsole), "AddMessageToHistory");

    public static void AddMessage(string message)
    {
        if (CommandConsole.instance != null && addMessageMethod != null)
        {
            addMessageMethod.Invoke(CommandConsole.instance, new object[] { message });
        }
    }
}