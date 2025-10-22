using BepInEx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BindManager
{
    private static Dictionary<KeyCode, string> keyBinds = new Dictionary<KeyCode, string>();

    private static string configPath = Path.Combine(Paths.ConfigPath, "binder_binds.json");

    public static void AddBind(KeyCode key, string command)
    {
        keyBinds[key] = command;
        SaveBinds();
    }

    public static bool RemoveBind(KeyCode key)
    {
        bool result = keyBinds.Remove(key);
        if (result)
            SaveBinds();
        return result;
    }

    public static IReadOnlyDictionary<KeyCode, string> GetAllBinds()
    {
        return keyBinds;
    }

    public static void CheckInput()
    {
        foreach (var bind in keyBinds)
        {
            if (Input.GetKeyDown(bind.Key))
            {
                Utils.EnableCheatsSilently();
                CommandConsole.instance.ExecuteCommand(bind.Value);
            }
        }
    }

    public static void LoadBinds()
    {
        if (!File.Exists(configPath))
            return;

        try
        {
            string json = File.ReadAllText(configPath);
            if (string.IsNullOrEmpty(json))
                return;
            var loaded = JsonConvert.DeserializeObject<BindConfig>(json);
            if (loaded?.Binds == null)
                return;

            keyBinds.Clear();

            foreach (var pair in loaded.Binds)
            {
                if (Enum.TryParse(pair.Key, out KeyCode key))
                {
                    keyBinds[key] = pair.Value;
                }
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Binder: Failed to load binds: {ex}");
        }
    }

    private static void SaveBinds()
    {
        try
        {
            var config = new BindConfig();

            foreach (var pair in keyBinds)
            {
                config.Binds[pair.Key.ToString()] = pair.Value;
            }

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Binder: Failed to save binds: {ex}");
        }
    }
}