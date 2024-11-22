using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

public class SearchCommand : Command
{
    public SearchCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
    {
        if (arguments.Length < 2)
        {
            EstOnBon = false;
        }
    }

    Champion[] GetByRole(string role)
    {
        if(!Roles.TryParse(role, out Roles convertedRole)) {Console.Error.WriteLine("Le Rôle que vous cherchez n'existe pas."); return null;}
        var ChampionsDeTelRole = LesChampions.GetByType(convertedRole);
        Champion[] championsArray = ChampionsDeTelRole.ToArray();
        return championsArray;
    }

    Champion[] GetByDifficulty(string difficulty)
    {
        if(!Difficulties.TryParse(difficulty, out Difficulties convertedDifficulty)) {Console.Error.WriteLine("Le niveau de difficulté que vous cherchez n'existe pas. Il faut choisir entre (FAIBLE|MODÉRÉE|ÉLEVÉE)"); return null;}
        var ChampionsDeTelleDifficulty = LesChampions.GetByDifficulty(convertedDifficulty);
        Champion[] championsArray = ChampionsDeTelleDifficulty.ToArray();
        return championsArray;
    }

    public override void Execute()
    {
        if (!EstOnBon)
        {
            Console.Error.WriteLine("Il n'y a pas assez d'arguments pour effectuer une recherche de champion.\nCommande valide : (name|role|difficulty) <string>");
            return;
        }
        string TypeDeSearch = Arguments[0].ToLower();
        switch (TypeDeSearch)
        {
            case "name":
                TextInfo textInfo = new CultureInfo("fr", false).TextInfo;
                string motifName = textInfo.ToTitleCase(Arguments[1].ToLower());
                Champion championTrouve = LesChampions.GetByName(motifName);
                if (championTrouve != null)
                {
                    Console.WriteLine(championTrouve);
                }
                else
                {
                    Console.WriteLine("Champion n'est pas présent dans le ChampionDex.");
                }
                return;
            case "role":
                Champion[] championsByRole = GetByRole(Arguments[1].ToUpper());
                if (championsByRole != null)
                {
                    foreach (Champion c in championsByRole)
                    {
                        Console.WriteLine(c);
                        Console.WriteLine();
                    }
                }
                return;
            case "difficulty":
                string TransformedDifficulty = AddCommand.TransformToCorrectAccent(Arguments[1].ToUpper());
                Champion[] championsByDifficulty = GetByDifficulty(TransformedDifficulty);
                if (championsByDifficulty != null)
                {
                    foreach (Champion c in championsByDifficulty)
                    {
                        Console.WriteLine(c);
                        Console.WriteLine();
                    }
                }
                return;
            default:
                Console.Error.WriteLine("Le Type de recherche est invalide. Il faut choisir entre (name|role|difficulty)");
                return;
        }
    }
}