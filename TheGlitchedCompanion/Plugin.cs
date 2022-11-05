using BepInEx;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace TheGlitchedCompanion
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string ModGuid = "com.reddust9.tgc";
        private const string ModName = "The Glitched Companion";
        private const string ModVersion = "1.0.0-beta";
        public static ManualLogSource log;
        public static Plugin instance_;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"{ModName} is loaded!");
            log = Logger;
            var harmony = new Harmony(ModGuid);
            harmony.PatchAll();
            instance_ = this;
        }

        private void ObtainAndProcessPlayer()
        {
            var player = GameObject.FindWithTag("Player");
            NetworkBootstrap.InjectNetworkObject(ref player);
            Data.Player1_P = player;
            log.LogInfo("Player object saved");
        }

        public void InitializeAndStartMultiplayer()
        {
            ObtainAndProcessPlayer();
            InitNetworking();
            log.LogInfo("Starting Multiplayer");
        }
        
        [HarmonyPatch(typeof(Warehouse_SpawnEnnard), "OnTriggerEnter")]
        public static class Get_Player_E
        {
            public static void Postfix(ref Warehouse_SpawnEnnard __instance)
            {
                GameObject ennard = __instance.ennard;
                Destroy(ennard.GetComponent<Ennard_AI>());
                Data.Player2_E = ennard;
                Data.Player2_E_Initialized = true;
                log.LogInfo("Ennard AI nuked & saved");
                
                Plugin.instance_.InitializeAndStartMultiplayer();
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
            /* Log ennard position
            if (Data.Player2_E_Initialized)
            {
                var position = Data.Player2_E.transform.position;
                log.LogInfo($"Ennard pos: {position.x} / {position.y} / {position.z}");
            }*/
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