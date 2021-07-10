using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;
using static DiscordUtils;

public class PetCommandModule : SlashCommandModule {

	public static Dictionary<ulong, NewPetToken> UnnamedPets = new();

	[SlashCommandGroup("pet", "Commands regarding your pets")]
	public class PetCommands {
		
		[SlashCommand("get", "Go looking for a pet")]
		public async Task GetPet(InteractionContext context) {
			if (UnnamedPets.ContainsKey(context.User.Id)) {
				await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
					Response.AsEphemeral(true).WithContent("You're already in the process of adopting a pet!")
						.AddEmbed(new DiscordEmbedBuilder().WithDescription("Type `/pet accept <name>` to name and receive your new friend.")));
				return;
			}
			
			var color = PetConstants.ValidPetColors.Random();
			var family = PetConstants.ValidPetFamilies.Random();
			await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
				Response.AsEphemeral(true)
					.WithContent($"You find a {color} {family.GetName()} in need of help!") //TODO: look if the GetName here might be more expensive than necessary
					.AddEmbed(new DiscordEmbedBuilder().WithDescription("Type `/pet accept <name>` to name and receive your new friend.")));
			UnnamedPets.Add(context.User.Id, new NewPetToken {
				Color = color, 
				Family = family,
			});
			await Task.Delay(PetConstants.PetNameInterval);
			if (UnnamedPets.ContainsKey(context.User.Id)) {
				await context.FollowUpAsync(FollowupBuilder.AsEphemeral(true)
					.WithContent($"{context.User.Mention}, you haven't named the pet and it left again."));
				UnnamedPets.Remove(context.User.Id);
			}
		}

		
		[SlashCommand("accept", "Names a new pet and adds is to your sanctuary.")]
		public async Task NamePet(InteractionContext context, 
				[Option("name", "The name of your new pet"), RemainingText] string name) {
			if (!UnnamedPets.Remove(context.User.Id, out var petToken)) {
				await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
					Response.AsEphemeral(true)
						.WithContent("You currently dont have a pet waiting to be named."));
				return;
			}
			await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
				Response
					.AsEphemeral(true)
					.WithContent($"The {petToken.Color} {petToken.Family.GetName()}, now named {name}, joins you ♥"));
			await using (var dbContext = new Context()) {
				var owner = await DbUtils.GetOrCreatePlayer(dbContext, context.User);
				var dbPet = new Pet {
					Birthday = DateTime.Now,
					Color = petToken.Color,
					Name = name,
					Family = petToken.Family,
					PetId = Utils.Random.Next(),
				};
				dbContext.Pets.Add(dbPet);
				owner.Pets.Add(dbPet);
				dbContext.Players.Update(owner);
				await dbContext.SaveChangesAsync();
			}
		}

		[SlashCommand("view", "Views all your pets")]
		public async Task ShowAllPets(InteractionContext context, [Option("public", "Show the pets to everyone?")]bool @public = false) {
			var owner = context.User;
			ICollection<Pet> pets;
			await using (var dbContext = new Context()) {
				var player = await dbContext.Players
					.Include(p => p.Pets)
					.Where(p => p.DiscordUser == owner.Id)
					.FirstOrDefaultAsync();
				pets = player?.Pets;
			}
			if (pets == null || pets.Count == 0) {
				await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
					Response.WithContent("You dont have any pets yet.").AsEphemeral(true));
				return;
			}
				
			var petList = string.Join("\n", pets.Select(pet => $"{pet.Name}, the {pet.Color} {pet.Family.GetName()}"));
			await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
				Response.WithContent("__**Your pets are:**__\n"+petList).AsEphemeral(!@public));
		}
	}
}