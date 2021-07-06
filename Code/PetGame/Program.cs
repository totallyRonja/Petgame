using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

static class Program {
	static async Task Main(string[] args) {
		var discord = new DiscordClient(new DiscordConfiguration {
			Token = Settings.Get("BotToken"),
			TokenType = TokenType.Bot,
			Intents = DiscordIntents.All,
		});

		var commands = discord.UseSlashCommands();
		var interactivity = discord.UseInteractivity(new InteractivityConfiguration {
			ResponseBehavior = InteractionResponseBehavior.Respond,
			ResponseMessage = "Interaction Failed, please contact Ronja", //this doesnt work btw
		});

		discord.ComponentInteractionCreated += async (_, eventArgs) => {
			var response = eventArgs.Id switch {
				"sonic_yes" => "Thank you",
				"sonic_no" => "Wow, why?",
				_ => null,
			}; 
			if(response == null) return;
			await eventArgs.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
				new DiscordInteractionResponseBuilder().WithContent(response).AsEphemeral(false));
			await eventArgs.Message.ModifyAsync(new DiscordMessageBuilder()
				.WithContent("Let Sonic Say **FUCK**!")
				.AddComponents(
					new DiscordButtonComponent(ButtonStyle.Success, "yes", "Yes!", true),
					new DiscordButtonComponent(ButtonStyle.Danger, "no", "No!", true)));
		};

		ulong? testGuildId = 858715624565637130;
		
		commands.RegisterCommands<TestCommandModule>(testGuildId);
		commands.RegisterCommands<PetCommandModule>(testGuildId);

		await discord.ConnectAsync();

		var spamChannel = (await discord.GetGuildAsync(testGuildId.Value)).GetChannel(858748242149638165);

		await Task.Delay(-1);
	}
}
