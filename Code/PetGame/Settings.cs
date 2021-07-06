using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class Settings {
	public static Dictionary<string, string> SettingValues;
	
	public static string Get(string key) {
		return GetValues()[key];
	}

	private static Dictionary<string, string> GetValues() {
		return SettingValues ??= JsonSerializer.Deserialize<Dictionary<string, string>>(
			File.ReadAllText("Settings.json"));
	}
}