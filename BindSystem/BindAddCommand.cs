using System;
using System.Linq;
using UnityEngine;
using static CommandConsole;

public static class BindAddCommand
{
    public static void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            ConsoleHelper.AddMessage("Usage: bind add <key> <command1>, <command2>, ...");
            return;
        }

        try
        {
            KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), args[0], true);

            string joined = string.Join(" ", Utils.SubArray(args, 1));

            string[] commands = joined
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .ToArray();

            BindManager.AddBind(key, commands);

            ConsoleHelper.AddMessage($"Bound key [{key}] to commands: {string.Join(", ", commands)}");
            ConsoleHelper.AddMessage("<color=orange>* When you use the binds, cheat mode will also be activated (scoring disabled)</color>");
        }
        catch
        {
            ConsoleHelper.AddMessage($"Invalid key: {args[0]}");
        }
    }
}
