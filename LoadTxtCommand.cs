namespace ChampionDexNameSpace;

public class LoadTxtCommand : Command
{
    public LoadTxtCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
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
            Console.Error.WriteLine("Il n'y a pas l'argument du fichier TXT à charger.");
            return;
        }

        string path = Arguments[0];
        if (!File.Exists(path))
        {
            throw new Exception("Le chemin de fichier donné n'existe pas.");
        }
        StreamReader reader = new StreamReader(path);
        List<string> lines = new List<string>();
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line != null)
            {
                lines.Add(line);
            }
        }

        foreach (string line in lines)
        {
            string[] elements = line.Split(';', StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries);
            if (elements.Length < 4)
            {
                Console.WriteLine($"Cette ligne n'est pas valable : {line}");
                continue;
            }
            string ChampionName = elements[0];
            string[] ChampionCompetences = elements[1].Split(", ", StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries);
            if (!Roles.TryParse(elements[2], out Roles ChampionRole))
            {
                throw new Exception("Le 3e élément de la ligne n'est pas un type de Roles.");
            }
            if (!Difficulties.TryParse(elements[3], out Difficulties ChampionDifficulty))
            {
                throw new Exception("Le 4 élément de la ligne n'est pas un type de Difficulties.");
            }
            Champion champion = new Champion(ChampionName, ChampionCompetences, ChampionRole, ChampionDifficulty);
            if (LesChampions.Contains(champion))
            {
                Console.WriteLine($"{champion.Name} existe déjà dans le ChampionDex. Donc il ne sera pas loadé depuis le fichier Json. Si vous voulez réécrire ce champion, vous pouvez utiliser la commander `override`.");
                Console.WriteLine();
                continue;
            }
            else {LesChampions.Add(champion);}
        }
    }
}