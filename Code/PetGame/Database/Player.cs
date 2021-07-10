using System;
using System.Collections.Generic;

namespace Database {
	public class Player {
		public int Id { get; set; }
		public ulong DiscordUser { get; set; }
		public DateTime Joined { get; set; }
		public virtual ICollection<Pet> Pets { get; set; }
	}
}