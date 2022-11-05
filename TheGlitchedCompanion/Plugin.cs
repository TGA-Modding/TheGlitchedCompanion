using BepInEx;
using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGlitchedCompanion
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string ModGuid = "com.reddust9.tgc";
        private const string ModName = "The Glitched Companion";
        private const string ModVersion = "1.0.0-beta";
        public static ManualLogSource log;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"{ModName} is loaded!");
            log = Logger;
            var harmony = new Harmony(ModGuid);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Player_Move), "Update")]
        public static class Get_Player_P
        {
            public static void Postfix(ref Player_Move __instance)
            {
                GameObject player = __instance.gameObject;
                NetworkBootstrap.InjectNetworkObject(ref player);
                Data.Player1_P = player;
                log.LogInfo("Player object saved");
            }
        }
        [HarmonyPatch(typeof(Warehouse_SpawnEnnard), "OnTriggerEnter")]
        public static class Get_Player_E
        {
            public static void Postfix(ref Warehouse_SpawnEnnard __instance)
            {
                GameObject ennard = __instance.ennard;
                Destroy(ennard.GetComponent<Ennard_AI>());
                Data.Player2_E = ennard;
                log.LogInfo("Ennard AI nuked & saved");
            }
        }
        
        private void InitNetworking()
        {
            if(Data.NetworkManager_Initialized) return;
            Data.NetworkManager = NetworkBootstrap.CreateNetworkManager();
            Data.NetworkManager_Initialized = true;
        }

        private void Update()
        {
            DebugPatches.Update();
        }

        [HarmonyPatch(typeof(MainMenu_VersionTextManager), "Start")]
        public static class VersionTextPatch
        {
            // ReSharper disable once InconsistentNaming
            public static void Postfix(ref MainMenu_VersionTextManager __instance)
            {
                __instance.txt.text += $"\nThe Glitched Companion v{ModVersion} by reddust9";
            }
        }
    }
}