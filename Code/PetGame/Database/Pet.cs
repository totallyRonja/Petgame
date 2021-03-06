using System;

namespace Database {
	public class Pet {
		public int PetId { get; set; }
		public string Name { get; set; }
		public DateTime Birthday { get; set; }
		public string Color { get; set; }
		public PetFamily Family { get; set; }
		public virtual Player Player { get; set; }
	}
}