using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database {
	public class Player {
		[Key] public int Id { get; set; }
		public ulong DiscordUser { get; set; }
		public DateTime Joined { get; set; }

		public virtual List<Pet> Pets { get; set; } = new();
	}
}