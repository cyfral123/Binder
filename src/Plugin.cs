using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Binder
{
    [BepInPlugin("com.cyfral.binder", "Console Binder", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            BindManager.LoadBinds();
            var harmony = new Harmony("com.cyfral.binder");
            harmony.PatchAll();
        }
    }
}