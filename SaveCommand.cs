public class SaveCommand :  Command
{
    public SaveCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
    {
        if (Arguments.Length < 1)
        {
            EstOnBon = false;
        }
    }

    private string AddExtension(string path)
    {
        if (!path.Contains(".json"))
        {
            path += ".json";
        }
        return path;
    }

    public override void Execute()
    {
        if (!EstOnBon)
        {
            Console.Error.WriteLine("Il manque le chemin pour sauvegarder le ChampionDex.");
            return;
        }
        string CheminOuput = AddExtension(Arguments[0]);
        if (File.Exists(CheminOuput) && CheminOuput == "Data/ScrappedChampions.json")
        {
            Console.Error.WriteLine("Vous ne pouvez pas réécrire le fichier du ChampionDex Scrappé.");
            return;
        }
        LesChampions.SaveJson(CheminOuput);
    }
}