using HarmonyLib;
using System;
using System.Reflection;

[HarmonyPatch(typeof(CommandConsole), "Awake")]
public static class CommandConsolePatch
{
    [HarmonyPostfix]
    public static void Postfix(CommandConsole __instance)
    {
        if (__instance == null) return;

        var registerMethod = AccessTools.Method(typeof(CommandConsole), "RegisterCommand");
        if (registerMethod == null)
        {
            UnityEngine.Debug.LogError("RegisterCommand not found");
            return;
        }

        Action<string[]> bindCallback = args =>
        {
            if (args.Length == 0)
            {
                ConsoleHelper.AddMessage("Usage: add | clear | list | modify");

                return;
            }

            string subcommand = args[0].ToLowerInvariant();
            string[] subArgs = Utils.SubArray(args, 1);

            switch (subcommand)
            {
                case "add":
                    BindAddCommand.Execute(subArgs);
                    break;
                case "clear":
                    BindClearCommand.Execute(subArgs);
                    break;
                case "list":
                    BindListCommand.Execute(subArgs);
                    break;
                case "modify":
                    BindModifyCommand.Execute(subArgs);
                    break;
                default:
                    ConsoleHelper.AddMessage("Unknown bind subcommand. Use: add | clear | list | modify");
                    break;
            }
        };

        registerMethod.Invoke(__instance, new object[] { "bind", bindCallback, false });
    }
}

[HarmonyPatch(typeof(ENT_Player), "LateUpdate")]
public static class ENT_Player_LateUpdate_Patch
{

    [HarmonyPostfix]
    public static void Postfix(ENT_Player __instance)
    {
        if (__instance == null) return;

        if (!__instance.IsInputLocked())
        {
            BindManager.CheckInput();
        }
    }
}