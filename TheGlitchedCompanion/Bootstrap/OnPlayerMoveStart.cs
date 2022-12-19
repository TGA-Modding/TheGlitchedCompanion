using HarmonyLib;
using TheGlitchedCompanion.Networking;

namespace TheGlitchedCompanion.Bootstrap;

public class OnPlayerMoveStart
{
	[HarmonyPatch(typeof(Player_Move), "Awake")]
	public static bool Prefix(ref Player_Move __instance)
	{
		NetworkObjectFactory.AddNetworkComponents(__instance.gameObject);
		NetworkObjectFactory.InitializePlayerPrefabWith(__instance.gameObject);
		return false;
	}
}