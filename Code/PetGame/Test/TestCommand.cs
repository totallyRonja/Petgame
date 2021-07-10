using System.IO;
using System.Net;
using System.Threading.Tasks;
using Database;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using static DiscordUtils;

public class TestCommandModule : SlashCommandModule {
	//[SlashCommand("test", "it tests")]
	public async Task TestCommand(InteractionContext context) {
		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
			new DiscordInteractionResponseBuilder()
				.WithContent("Let Sonic Say **FUCK**!")
				.AsEphemeral(false)
				.AddComponents(
					new DiscordButtonComponent(ButtonStyle.Success, "sonic_yes", "Yes!"),
					new DiscordButtonComponent(ButtonStyle.Danger, "sonic_no", "No!")));
	}

	[SlashCommand("embed", "tests a embed with a file url")]
	public async Task EmbedTest(InteractionContext context,
			[Option("url", "the file url to show")] string url = "https://video.twimg.com/tweet_video/E0Dn1M3XEAUpfL-.mp4") {
		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, Response.WithContent(url));
	}

	[SlashCommand("file", "uploads a file")]
	public async Task FileTest(InteractionContext context) {
		await context.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource, Response.AsEphemeral(true));
		var webClient = new WebClient();
		await using var stream = webClient.OpenRead("http://video.twimg.com/tweet_video/E0Dn1M3XEAUpfL-.mp4");
		await context.EditResponseAsync(new DiscordWebhookBuilder().WithContent("wee"));
	}
}