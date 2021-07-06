//Token that represents a new, unnamed pet
//Stored locally until the pet is named and the adoption verified
//Then the pet will go into the database
public struct NewPetToken {
	public string Color;
	public PetFamily Family;
}