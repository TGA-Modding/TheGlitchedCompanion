using HarmonyLib;

namespace TheGlitchedCompanion.HelperPatches;

public static class UnlockAll
{
	[HarmonyPatch(typeof(MainMenu_SetBackgroundButtons), "SetButtons")]
	public class UnlockAllBackgrounds
	{
		public static void Postfix(ref MainMenu_SetBackgroundButtons __instance)
		{
			for (int i = 0; i < __instance.onButtons.Length; i++)
			{
				__instance.onButtons[i].SetActive(true);
				__instance.offButtons[i].SetActive(false);
			}
		}
	}
	[HarmonyPatch(typeof(MainMenu_SetReplayLevelButtons), "SetButtons")]
	public class UnlockAllReplays
	{
		public static void Postfix(ref MainMenu_SetReplayLevelButtons __instance)
		{
			for (int i = 0; i < __instance.onButtons.Length; i++)
			{
				__instance.onButtons[i].SetActive(true);
				__instance.offButtons[i].SetActive(false);
			}
		}
	}
}