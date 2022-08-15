using System;
using System.Reflection;
using Steamworks;
using UnityEngine;
using Verse;
using Verse.Steam;

namespace SteamWorkshopUploader
{
	public class Settings : ModSettings
	{
		//Use Mod.settings.setting to refer to this setting.
		public ulong steamID = 76561197980628574;

		
		
		
		public void DoWindowContents(Rect wrect)
		{
			var options = new Listing_Standard();
			options.Begin(wrect);
			
			// options.CheckboxLabeled("Sample setting", ref setting);
			options.Gap();

			options.End();
		}
		
		public override void ExposeData()
		{
			// Scribe_Values.Look(ref setting, "setting", true);
		}
	}
}