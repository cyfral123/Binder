using BepInEx;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BindManager
{
    private static Dictionary<KeyCode, string[]> binds = new Dictionary<KeyCode, string[]>();
    private static readonly string configPath = Path.Combine(Paths.ConfigPath, "binder_binds.json");

    public static void AddBind(KeyCode key, string[] commands)
    {
        binds[key] = commands;
        SaveBinds();
    }

    public static bool RemoveBind(KeyCode key)
    {
        bool result = binds.Remove(key);
        if (result)
            SaveBinds();
        return result;
    }

    public static IReadOnlyDictionary<KeyCode, string[]> GetAllBinds()
    {
        return binds;
    }

    public static void CheckInput()
    {
        foreach (var bind in binds)
        {
            if (Input.GetKeyDown(bind.Key))
            {
                Utils.EnableCheatsSilently();
                foreach (var cmd in bind.Value)
                {
                    CommandConsole.instance.ExecuteCommand(cmd);
                }
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

            JObject root = JObject.Parse(json);
            var bindsToken = root["Binds"];
            if (bindsToken == null)
                return;

            binds.Clear();
            bool convertedOldFormat = false;

            foreach (var pair in bindsToken.Children<JProperty>())
            {
                if (!Enum.TryParse(pair.Name, true, out KeyCode key))
                    continue;

                JToken value = pair.Value;
                if (value.Type == JTokenType.String)
                {
                    binds[key] = new string[] { value.ToString() };
                    convertedOldFormat = true;
                }
                else if (value.Type == JTokenType.Array)
                {
                    binds[key] = value.ToObject<string[]>();
                }
            }

            if (convertedOldFormat)
            {
                Debug.Log("Binder: Converted old bind format to new version.");
                SaveBinds();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Binder: Failed to load binds: {ex}");
        }
    }

    private static void SaveBinds()
    {
        try
        {
            var config = new BindConfig();

            foreach (var bind in binds)
            {
                config.Binds[bind.Key.ToString()] = bind.Value;
            }

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Binder: Failed to save binds: {ex}");
        }
    }
}
