using System;
using UnityEngine;
using static CommandConsole;

public static class BindAddCommand
{
    public static void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            ConsoleHelper.AddMessage("Usage: bind add <key> <command>");
            return;
        }

        try
        {
            KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), args[0], true);
            string command = string.Join(" ", Utils.SubArray(args, 1));
            BindManager.AddBind(key, command);
            ConsoleHelper.AddMessage($"Bound key [{key}] to command: {command}");
            ConsoleHelper.AddMessage("* When you use the binds, cheat mode will also be activated (scoring disabled)");
        }
        catch
        {
            ConsoleHelper.AddMessage($"Invalid key: {args[0]}");
        }
    }
}
