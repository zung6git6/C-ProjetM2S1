namespace ChampionDexNameSpace;
public class ChampionDexDto
{
    public List<ChampionDto> _LesChampions {get; set;}

    public void SaveJson(string CheminOutput="Data/ScrappedChampions.json")
    {
        if (_LesChampions == null)
        {
            Console.Error.WriteLine("Le ChamionDex est vide. Rien est sauvegardé.");
            return;
        }
        string jsonOutput = System.Text.Json.JsonSerializer.Serialize(_LesChampions, new System.Text.Json.JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
        File.WriteAllText(CheminOutput, jsonOutput);
        if (CheminOutput=="Data/ScrappedChampions.json")
        {
            Console.WriteLine("Les Champions Scrappés sont sauvegardés dans le fichier Data/ScrappedChampions.json");
        }
        else
        {
            Console.WriteLine($"Le ChampionDex est sauvegardé dans le fichier {CheminOutput}");
        }
    }

    public void SaveTxt(StreamWriter file)
    {
        if (_LesChampions == null)
        {
            Console.Error.WriteLine("Le ChamionDex est vide. Rien est sauvegardé.");
            return;
        }
        foreach (ChampionDto championDto in _LesChampions)
        {
            if (championDto != null)
            {
                file.WriteLine(championDto.WritingForm);
            }
        }
    }
}