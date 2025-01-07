namespace ChampionDexNameSpace;
using System.Text.Json;

public class LoadCommand : Command
{
    public LoadCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
    {
        if (arguments.Length != 1)
        {
            EstOnBon =  false;
        }
    }

    public override void Execute()
    {
        if (!EstOnBon)
        {
            Console.Error.WriteLine("Il n'y a pas l'argument du fichier Json à charger.");
            return;
        }

        string JsonFilePath = Arguments[0];
        if (!File.Exists(JsonFilePath))
        {
            Console.WriteLine("Le fichier Json à charger n'existe pas.");
            return;
        }

        string jsonContent;
        try
        {
            jsonContent = File.ReadAllText(JsonFilePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erreur lors de la lecture du fichier JSON : {ex.Message}");
            return;
        }

        try
        {
            List<Champion> LesChampionsLus = JsonSerializer.Deserialize<List<Champion>>(jsonContent);
            if (LesChampionsLus == null)
            {
                Console.Error.WriteLine("Le contenu JSON est vide ou ne peut pas être désérialisé en une liste de champions.");
                return;
            }

            bool GroundTruth = false;
            if (JsonFilePath == "Data/ScrappedChampions.json")
            {
                GroundTruth = true;
            }
            
            if (GroundTruth)
            {
                foreach (Champion champion in LesChampionsLus)
                {
                    if (LesChampions.Contains(champion) && !LesChampions.Same(champion))
                    {
                        LesChampions.OverrideChampion(champion);
                        Console.WriteLine($"Champion {champion.Name} existe déjà dans le ChampionDex, donc son ancienne entrée a été réécrite.");
                    }
                    else if (LesChampions.Contains(champion) && LesChampions.Same(champion))
                    {
                        Console.WriteLine($"Champion {champion.Name} existe déjà et la nouvelle entrée reste identique à l'ancienne, donc pas de réécriture.");
                        continue;
                    }
                    else
                    {
                        LesChampions.Add(champion);
                    }
                }
            }
            else
            {
                foreach (Champion champion in LesChampionsLus)
                {
                    if (LesChampions.Contains(champion))
                    {
                        Console.WriteLine($"{champion.Name} existe déjà dans le ChampionDex. Donc il ne sera pas loadé depuis le fichier Json. Si vous voulez réécrire ce champion, vous pouvez utiliser la commander `override`.");
                        Console.WriteLine();
                        continue;
                    }
                    else {LesChampions.Add(champion);}
                }
                return;
            }
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Erreur de format JSON : {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Une erreur inattendue est survenue : {ex.Message}");
        }
    }

}