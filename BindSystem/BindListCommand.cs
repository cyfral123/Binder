using UnityEngine;

public static class BindListCommand
{
    public static void Execute(string[] args)
    {
        var binds = BindManager.GetAllBinds();
        if (binds.Count == 0)
        {
            ConsoleHelper.AddMessage("No keybinds.");
            return;
        }

        foreach (var pair in binds)
        {
            ConsoleHelper.AddMessage($"{pair.Key}: {pair.Value}");
        }
    }
}
