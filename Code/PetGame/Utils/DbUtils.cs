using System;
using System.Linq;
using System.Threading.Tasks;
using Database;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;

public static class DbUtils {
	public static async Task<Player> GetOrCreatePlayer(Context dbContext, DiscordUser user) {
		var player = await dbContext.Players.Where(p => p.DiscordUser == user.Id).FirstOrDefaultAsync();
		if (player != null) return player;
		player = new Player {
			Id = Utils.Random.Next(),
			DiscordUser = user.Id,
			Joined = DateTime.Now,
		};
		dbContext.Players.Add(player);
		await dbContext.SaveChangesAsync();
		return player;
	}
}