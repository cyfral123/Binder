using System;
using UnityEngine;

public static class BindRemoveCommand
{
    public static void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            ConsoleHelper.AddMessage("Usage: bind remove <key>");
            return;
        }

        try
        {
            KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), args[0], true);
            if (BindManager.RemoveBind(key))
            {
                ConsoleHelper.AddMessage($"Removed bind for key [{key}]");
            }
            else
            {
                ConsoleHelper.AddMessage($"No bind found for key [{key}]");
            }
        }
        catch
        {
            ConsoleHelper.AddMessage($"Invalid key: {args[0]}");
        }
    }
}
