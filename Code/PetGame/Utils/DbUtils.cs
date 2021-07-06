using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using DSharpPlus.Entities;

public static class DbUtils {
	public static async Task<Player> GetOrCreatePlayer(Context dbContext, DiscordUser user) {
		var player = await dbContext.Players.FindAsync((int)user.Id);
		if (player != null) return player;
		player = new Player {
			Id = (int)user.Id,
			DiscordUser = user.Id,
			Joined = DateTime.Now,
			Pets = new List<Pet>(),
		};
		dbContext.Players.Add(player);
		await dbContext.SaveChangesAsync();
		return player;
	}
}