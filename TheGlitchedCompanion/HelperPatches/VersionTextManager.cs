using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace TheGlitchedCompanion.HelperPatches;

public static class VersionTextManager
{
	private static readonly string m_VersionText = "Ver " + Application.version;
	private static readonly List<string> m_TextAppends = new();
	private static MainMenu_VersionTextManager m_VTM_Instance;

	public static void AppendToVersionText(string text)
	{
		m_TextAppends.Add(text);
	}

	public static void Update()
	{
		if (m_VTM_Instance == null) return;
		m_VTM_Instance.txt.text = m_VersionText;
		foreach (var ta in m_TextAppends)
		{
			m_VTM_Instance.txt.text += "\n";
			m_VTM_Instance.txt.text += ta;
		}
	}
	
	[HarmonyPatch(typeof(MainMenu_VersionTextManager), "Start")]
	public class MM_VTM_GetInstance
	{
		public static void Postfix(ref MainMenu_VersionTextManager __instance)
		{
			m_VTM_Instance = __instance;
		}
	}
}