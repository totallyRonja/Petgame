using System.Threading.Tasks;
using Database;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

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
}