using System;
using System.Reflection;
using HarmonyLib;
using Steamworks;
using UnityEngine;
using Verse;
using Verse.Steam;

namespace SteamWorkshopUploader
{
	[HarmonyPatch(typeof(SteamUser), nameof(SteamUser.GetSteamID))]
	public class SteamIDHarmonyPatch
	{
		public static bool Prefix(ref CSteamID __result)
		{
			__result = new CSteamID(SteamWorkshopUploader.settings.steamID);
			return false;
		}
	}

	// [StaticConstructorOnStartup]
	// [HarmonyPatch(typeof(SteamManager), nameof(SteamManager.InitIfNeeded))]
	// public class SteamInitHarmonyPatch
	// {
	// 	public static bool Prefix()
	// 	{
			// if (SteamManager.Initialized)
			// {
			// 	return;
			// }
			//
			// if (!Packsize.Test())
			// {
			// 	Log.Message(
			// 		"[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.");
			// }
			//
			// if (!DllCheck.Test())
			// {
			// 	Log.Message(
			// 		"[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.");
			// }
			//
			// try
			// {
			// 	if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
			// 	{
			// 		Application.Quit();
			// 		return;
			// 	}
			// }
			// catch (DllNotFoundException arg)
			// {
			// 	Log.Message(
			// 		"[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" +
			// 		arg);
			// 	Application.Quit();
			// 	return;
			// }
			//
			// var initializedIntField =
			// 	typeof(SteamManager).GetField("initializedInt",
			// 		BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic |
			// 		BindingFlags.Static);
			// var init = SteamAPI.Init();
			// initializedIntField?.SetValue(null, init);
			// if (!SteamManager.Initialized)
			// {
			// 	Log.Message(
			// 		"[Steamworks.NET] SteamAPI.Init() failed. Possible causes: Steam client not running, launched from outside Steam without steam_appid.txt in place, running with different privileges than Steam client (e.g. \"as administrator\")");
			// 	return;
			// }
			//
			// var workshopInit = typeof(Workshop).GetMethod("Init",
			// 	BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			// workshopInit?.Invoke(null, null);	
	// 	}
	// }
	
	[HarmonyPatch(typeof(SteamUtils), nameof(SteamUtils.GetAppID))]
	public class SteamInitHarmonyPatch
	{
		public static bool Prefix(ref AppId_t __result)
		{
			__result = new AppId_t(294100);
			return false;
		}
	}
}