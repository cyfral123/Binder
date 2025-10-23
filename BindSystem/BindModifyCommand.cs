using System;
using UnityEngine;

public static class BindModifyCommand
{
    public static void Execute(string[] args)
    {
        if (args.Length < 3)
        {
            ConsoleHelper.AddMessage("Usage:");
            ConsoleHelper.AddMessage("  bind modify removefrom <key> <position>");
            ConsoleHelper.AddMessage("  bind modify appendto <key> <command>");
            return;
        }

        string action = args[0].ToLower();
        string keyStr = args[1];

        if (!Enum.TryParse(keyStr, true, out KeyCode key))
        {
            ConsoleHelper.AddMessage($"Invalid key: {keyStr}");
            return;
        }

        if (!BindManager.GetAllBinds().TryGetValue(key, out var commands))
        {
            ConsoleHelper.AddMessage($"No binds found for key {key}");
            return;
        }

        switch (action)
        {
            case "removefrom":
                RemoveCommand(key, commands, args);
                break;

            case "appendto":
                AppendCommand(key, commands, args);
                break;

            default:
                ConsoleHelper.AddMessage($"Unknown modify action: {action}");
                break;
        }
    }

    private static void RemoveCommand(KeyCode key, string[] commands, string[] args)
    {
        if (args.Length < 3 || !int.TryParse(args[2], out int position))
        {
            ConsoleHelper.AddMessage("Invalid or missing position.");
            return;
        }

        if (position < 1 || position > commands.Length)
        {
            ConsoleHelper.AddMessage("Position out of range.");
            return;
        }

        int index = position - 1;
        string removed = commands[index];

        var newCommands = new string[commands.Length - 1];
        int j = 0;
        for (int i = 0; i < commands.Length; i++)
        {
            if (i != index)
            {
                newCommands[j++] = commands[i];
            }
        }

        BindManager.AddBind(key, newCommands);

        ConsoleHelper.AddMessage($"Removed \"{removed}\" command from key {key}");
    }

    private static void AppendCommand(KeyCode key, string[] commands, string[] args)
    {
        if (args.Length < 3)
        {
            ConsoleHelper.AddMessage("Missing command to append.");
            return;
        }

        string newCommand = string.Join(" ", args, 2, args.Length - 2);

        var newCommands = new string[commands.Length + 1];
        Array.Copy(commands, newCommands, commands.Length);
        newCommands[newCommands.Length - 1] = newCommand;

        BindManager.AddBind(key, newCommands);

        ConsoleHelper.AddMessage($"Appended \"{newCommand}\" command to key {key}");
    }
}