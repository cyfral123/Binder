using System;
using UnityEngine;

public static class BindClearCommand
{
    public static void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            ConsoleHelper.AddMessage("Usage: bind clear <key>");
            return;
        }

        try
        {
            KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), args[0], true);
            if (BindManager.ClearBind(key))
            {
                ConsoleHelper.AddMessage($"All binds on the key [{key}] are cleared");
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
