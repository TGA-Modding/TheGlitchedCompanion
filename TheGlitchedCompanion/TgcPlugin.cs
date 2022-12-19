using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TheGlitchedCompanion.HelperPatches;
using TheGlitchedCompanion.Networking;
using Unity.Netcode;
using UnityEngine;

namespace TheGlitchedCompanion;

[BepInPlugin(TgcPluginInfo.Guid, TgcPluginInfo.Name, TgcPluginInfo.Version)]
public class TgcPlugin : BaseUnityPlugin
{
	public static ManualLogSource LogSource;
	public static NetworkManager NetworkManager;
	public static bool NetworkRoleStarted;
	private void Awake()
	{
		var harmony = new Harmony(TgcPluginInfo.Guid);
		harmony.PatchAll();
		LogSource = Logger;
		LogSource.LogInfo($"{TgcPluginInfo.Name} v{TgcPluginInfo.Version} has been loaded! Made by {TgcPluginInfo.Author}");
		LogSource.LogInfo($"{TgcPluginInfo.Name}: {TgcPluginInfo.Description}");
		VersionTextManager.AppendToVersionText("Unity v" + Application.unityVersion);
		VersionTextManager.AppendToVersionText($"{TgcPluginInfo.Name} v{TgcPluginInfo.Version}");

		NetworkManager = NetworkObjectFactory.CreateNetworkManager().GetComponent<NetworkManager>();
	}

	private void Update()
	{
		if (!NetworkRoleStarted)
		{
			if (Input.GetKeyDown(KeyCode.T))
			{
				NetworkManager.StartHost();
			} else if (Input.GetKeyDown(KeyCode.Y))
			{
				NetworkManager.StartClient();
			}

			VersionTextManager.Update();}
	}
}