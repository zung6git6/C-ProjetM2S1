namespace ChampionDexNameSpace;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;


public class ScrapeCommand : Command
{
    public ScrapeCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
    {
        if (Arguments.Length < 1)
        {
            EstOnBon = false;
        }
    }

    public override void Execute()
    {
        if (!EstOnBon)
        {
            Console.Error.WriteLine("Il n'y a pas l'argument du fichier txt qui contient les noms de champions.");
            return;
        }
        string championsNameFilePath = Arguments[0];
        TextInfo textInfo = new CultureInfo("fr", false).TextInfo;
        if (!File.Exists(championsNameFilePath))
        {
            throw new Exception("Le chemin de fichier des noms de champions n'existe pas.");
        }
        StreamReader reader = new StreamReader(championsNameFilePath);
        List<string> lines = new List<string>();
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line != null)
            {
                string lineCleaned = Regex.Replace(line, "[^a-zA-Z']+", "-");
                lineCleaned = Regex.Replace(lineCleaned, "'", "");
                lineCleaned = lineCleaned.ToLower();
                if (lineCleaned == "nunu-willump") {lineCleaned = "nunu-and-willump";}
                lines.Add(lineCleaned);
            }
        }

        ChampionDex Dex = new ChampionDex();

        foreach (string name in lines)
        {
            // the URL of the target page
            string url = $"https://wildrift.leagueoflegends.com/fr-fr/champions/{name}/";
            
            // Console.WriteLine($"Nom du champion : {name}");

            var web = new HtmlWeb();
            // downloading to the target page and parsing its HTML content
            var document = web.Load(url);

            // Selecting the champion's name
            var championsName = document.DocumentNode.SelectSingleNode("/html/body/div[1]/div/main/div/span[2]/section/div[2]/div[1]/div[1]/div[2]/div/div/div");
            string contentName = "";
            if (championsName != null)
            {
                contentName = HtmlEntity.DeEntitize(championsName.InnerText.Trim());
                contentName = textInfo.ToTitleCase(contentName.ToLower());
                // Console.WriteLine(contentName);
            }

            // Selecting all skills
            var skills = document.DocumentNode.SelectNodes("/html/body/div[1]/div/main/div/span[3]/section/div/div[2]/div[1]/div[2]/div/div[1]/ol/li");
            string[] competences = new string[5];
            if (skills != null)
            {
                int i = 0; // Counter for indexing
                foreach (var node in skills)
                {
                    // Extracting the content of the specific sub-node inside each `li`
                    var contentNode = node.SelectSingleNode("div/div[2]");
                    if (contentNode != null)
                    {
                        // Decode HTML entities to normal characters
                        string textContent = HtmlEntity.DeEntitize(contentNode.InnerText.Trim());
                        textContent = textInfo.ToTitleCase(textContent.ToLower());
                        competences[i] = textContent;
                        // Console.WriteLine($"Compétence {i}: {textContent}");
                        i++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Aucune compétence trouvée pour le champion.");
            }

            // Selecting the role or roles
            var role = document.DocumentNode.SelectSingleNode("/html/body/div[1]/div/main/div/span[2]/section/div[2]/div[1]/div[2]/div[1]/div/div[2]/p[2]");
            Roles roles = Roles.None;
            if (role != null)
            {
                string contentRole = HtmlEntity.DeEntitize(role.InnerText.Trim()).ToUpper();
                if (contentRole.Contains("/"))
                {
                    string[] multiRoles = contentRole.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (string oneroleINstring in multiRoles)
                    {
                        if (!Roles.TryParse(oneroleINstring, out Roles onerole)) {Console.WriteLine($"Le rôle {oneroleINstring} est inconnu");}
                        roles |= onerole;
                    }
                    // Joindre tous les éléments avec ", " comme séparateur
                    // string unifiedRoles = string.Join(", ", multiRoles);
                    // Console.WriteLine($"Rôles : {unifiedRoles}");
                }
                else
                {
                    if (!Roles.TryParse(contentRole, out Roles onerole)) {Console.WriteLine($"Le rôle {contentRole} est inconnu");}
                    roles |= onerole;
                    // Console.WriteLine($"Rôle : {contentRole}");
                }
            }
            else
            {
                Console.WriteLine("Aucun rôle trouvé pour le champion.");
            }

            // Select the difficulty level
            var difficulty = document.DocumentNode.SelectSingleNode("/html/body/div[1]/div/main/div/span[2]/section/div[2]/div[1]/div[2]/div[2]/div/div[2]/p[2]");
            Difficulties difficultyEnum = Difficulties.None;
            if (difficulty != null)
            {
                string contentDifficulty = HtmlEntity.DeEntitize(difficulty.InnerText.Trim()).ToUpper();
                if (!Difficulties.TryParse(contentDifficulty, out Difficulties extractedDifficulty)) {Console.WriteLine($"Le rôle {contentDifficulty} est inconnu");}
                difficultyEnum |= extractedDifficulty;
                // Console.WriteLine($"Difficulté : {contentDifficulty}");
            }
            else {Console.WriteLine("L'information du niveau de difficulté manquante pour le champion.");}
            Champion champion = new Champion(contentName, competences, roles, difficultyEnum);
            Dex.Add(champion);
            // Console.WriteLine(champion);
            // Console.WriteLine();
        }
        ChampionDexDto DexDto = Dex.ToDto();
        DexDto.SaveJson();
    }
}