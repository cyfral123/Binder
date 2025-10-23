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

        foreach (var bind in binds)
        {
            string joined = string.Join(", ", bind.Value);
            ConsoleHelper.AddMessage($"{bind.Key} -> {joined}");
        }
    }
}
