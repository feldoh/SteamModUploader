using System;
using System.Reflection;
using System.Linq;
using Verse;
using RimWorld;
using UnityEngine;
using HarmonyLib;
using Steamworks;
using Verse.Steam;

namespace SteamWorkshopUploader
{
	public class SteamWorkshopUploader : Verse.Mod
	{
	public static Settings settings;
		public SteamWorkshopUploader(ModContentPack content) : base(content)
		{
			// initialize settings
			settings = GetSettings<Settings>();

#if DEBUG
			Harmony.DEBUG = true;
#endif

			Harmony harmony = new Harmony("Taggerung.rimworld.SteamWorkshopUploader.main");	
			harmony.PatchAll();
			
			SteamManager.InitIfNeeded();
			FieldInfo fieldInfo =
				typeof(SteamUtility).GetField("SteamPersonaName",
					BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic |
					BindingFlags.Static);
			fieldInfo?.SetValue(null, "Taggerung");
			Log.Message(SteamUtility.SteamPersonaName);
			
			Log.Message("Patched Steam Init");
			if (SteamManager.Initialized)
			{
				return;
			}

			if (!Packsize.Test())
			{
				Log.Message(
					"[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.");
			}

			if (!DllCheck.Test())
			{
				Log.Message(
					"[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.");
			}

			try
			{
				if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
				{
					Application.Quit();
					return;
				}
			}
			catch (DllNotFoundException arg)
			{
				Log.Message(
					"[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" +
					arg);
				Application.Quit();
				return;
			}

			var initializedIntField =
				typeof(SteamManager).GetField("initializedInt",
					BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic |
					BindingFlags.Static);
			var init = SteamAPI.Init();

			try {Log.Message("apppppp"); } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message(SteamUtils.GetAppID().ToString() + "App"); } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message(SteamApps.GetAppBuildId().ToString()); } catch(Exception e) {Log.Message(e.Message);}

			try {Log.Message(SteamClient.CreateSteamPipe().ToString());  } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message(SteamClient.CreateLocalUser(out var sp,EAccountType.k_EAccountTypeIndividual ).ToString());  } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message(SteamUser.GetHSteamUser().ToString());  } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message(init.ToString());  } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message($"{SteamAPI.GetHSteamUser()}U");  } catch(Exception e) {Log.Message(e.Message);}
			try {Log.Message($"{SteamAPI.GetHSteamPipe()}p");  } catch(Exception e) {Log.Message(e.Message);}
			initializedIntField?.SetValue(null, init);
			if (!SteamManager.Initialized)
			{
				Log.Message(
					"[Steamworks.NET] SteamAPI.Init() failed. Possible causes: Steam client not running, launched from outside Steam without steam_appid.txt in place, running with different privileges than Steam client (e.g. \"as administrator\")");
				return;
			}

			var workshopInit = typeof(Workshop).GetMethod("Init",
				BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			workshopInit?.Invoke(null, null);
			
		}
		
		public override void DoSettingsWindowContents(Rect inRect)
		{
			base.DoSettingsWindowContents(inRect);
			settings.DoWindowContents(inRect);
		}

		public override string SettingsCategory()
		{
			return "SteamWorkshopUploader";
		}
	}
}