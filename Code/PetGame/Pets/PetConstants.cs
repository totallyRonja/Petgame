public static class PetConstants {
	public static PetFamily[] ValidPetFamilies = {PetFamily.MoonBear, PetFamily.Slink};
	
	//todo: more colors, actually have them be colors, and think more about (custom!!) textures?
	public static string[] ValidPetColors = {"blue", "springgreen", "cyan", "amber", "maroon", "rebeccapurple", "hotpink"};
	public static int PetNameInterval = 60 * 60 * 1000; //allow people one hour to name their pet (value is in ms)
}