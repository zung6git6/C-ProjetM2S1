namespace ChampionDexNameSpace;

public class SaveTxtCommand : Command
{
    public SaveTxtCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
    {
        if (arguments.Length < 1)
        {
            EstOnBon = false;
        }
    }

    public override void Execute()
    {
        if (!EstOnBon)
        {
            Console.Error.WriteLine("Le chemin à sauvegarder en fichier TXT est manquant.");
            return;
        }

        string path = Arguments[0];
        path = AddExtension(path);

        try
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                ChampionDexDto championdexDto = LesChampions.ToDto();
                championdexDto.SaveTxt(writer);
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.Error.WriteLine("Erreur : Le chemin spécifié est introuvable.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erreur inattendue : {ex.Message}");
            return;
        }
    }


    private string AddExtension(string path)
    {
        if (!path.Contains(".txt"))
        {
            path += ".txt";
        }
        return path;
    }

}