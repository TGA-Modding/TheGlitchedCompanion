using System.Collections.Generic;
using UnityEngine.SceneManagement;
using HarmonyLib;
using UnityEngine;

namespace TheGlitchedCompanion;

public class DebugPatches
{
    [HarmonyPatch(typeof(MainMenu_SetBackgroundButtons), "SetButtons")]
    public class GiveAllBackgrounds
    {
        public static void Postfix(ref MainMenu_SetBackgroundButtons __instance)
        {
            for (int index = 0; index < __instance.onButtons.Length; ++index)
            {
                __instance.onButtons[index].SetActive(true);
                __instance.offButtons[index].SetActive(false);
            }
        }
    }
        
    [HarmonyPatch(typeof(MainMenu_SetReplayLevelButtons), "SetButtons")]
    public class GiveAllReplays
    {
        public static void Postfix(ref MainMenu_SetReplayLevelButtons __instance)
        {
            for (int index = 0; index < __instance.onButtons.Length; ++index)
            {
                __instance.onButtons[index].SetActive(true);
                __instance.offButtons[index].SetActive(false);
            }
        }
    }

    internal static void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (var scene in GetAllScenes())
            {
                Plugin.log.LogInfo("SCENE: " + scene.name);
            }
        }
    }
    private static List<Scene> GetAllScenes()
    {
        var sceneCount = SceneManager.sceneCount;
        List<Scene> scenes = new();
        for (var i = 0; i < sceneCount; i++)
        {
            scenes.Add(SceneManager.GetSceneAt(i));
        }

        return scenes;
    }
}