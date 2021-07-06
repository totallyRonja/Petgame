# Petgame
Discord Pet Game

To compile this you need to add the settings file and do the entity framework migrations.
The settings file should be named `Settings.json` and be at the top of the PetGame *project*, so `/Code/PetGame/Settings.json` and be copied into the compilation directory. It should look something like this
```json
{
  "BotToken": "<Discord Bot Token Here>",
  "DatabasePath": "C:\\Path\\To\\Database\\PetGame.db"
}
```

More on how to do the migrations is here: <https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli>.
