using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace TheGlitchedCompanion.Networking;

public static class NetworkObjectFactory
{
	public static GameObject CreateNetworkManager()
	{
		var obj = new GameObject();
		var nm = obj.AddComponent<NetworkManager>();
		var ut = obj.AddComponent<UnityTransport>();
		nm.NetworkConfig.NetworkTransport = ut;
		var inst = Object.Instantiate(obj);
		return inst;
	}

	public static void AddNetworkComponents(GameObject obj)
	{
		var no = obj.AddComponent<NetworkObject>();
	}

	public static void InitializePlayerPrefabWith(GameObject player)
	{
		TgcPlugin.NetworkManager.GetComponent<NetworkManager>().NetworkConfig.PlayerPrefab = player;
		Object.Destroy(player);
	}
}