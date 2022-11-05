using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking.Transport;

namespace TheGlitchedCompanion;

public class NetworkBootstrap
{
    private static GameObject CreateGameObject(IEnumerable<Type> components)
    {
        var obj = new GameObject();
        foreach (var component in components)
        {
            obj.AddComponent(component);
        }
        obj.SetActive(true);
        return obj;
    }

    public static GameObject CreateNetworkManager()
    {
        var netMan = CreateGameObject(new[]
        {
            typeof(NetworkManager)
        });
        var manager = netMan.GetComponent<NetworkManager>();
        Plugin.log.LogInfo("Created NetworkManager");
        return netMan;
    }

    public static NetworkObject InjectNetworkObject(ref GameObject target)
    {
        target.AddComponent<NetworkObject>();
        var networkObject = target.GetComponent<NetworkObject>();
        return networkObject;
    }
}