using DSharpPlus.Entities;

public static class DiscordUtils {
	public static DiscordInteractionResponseBuilder Response => new();
	public static DiscordMessageBuilder MessageBuilder => new();
	public static DiscordEmbedBuilder EmbedBuilder => new();
	public static DiscordFollowupMessageBuilder FollowupBuilder => new();
}