using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

public class AddCommand : Command
{
    public AddCommand(ChampionDex championdex, string[] arguments) : base(championdex, arguments)
    {
        if (arguments.Length < 8)
        {
            EstOnBon = false;
        }
    }

    public static string TransformToCorrectAccent(string text)
    {
        text = Regex.Replace(text, @"\b(ELEVEE|ELEVE|elevee|eleve)\b", "ÉLEVÉE", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"\b(MODERE|MODEREE|modere|moderee)\b", "MODÉRÉE", RegexOptions.IgnoreCase);
        return text;
    }

    public override void Execute()
    {
        if (!EstOnBon)
        {
            Console.Error.WriteLine("Il n'y a pas assez d'arguments pour ajouter un Champion. Peut-être que vous avez oublié que le séparateur est la tabulation et pas l'espace.");
            return;
        }
        
        TextInfo textInfo = new CultureInfo("fr", false).TextInfo;
        string championName = textInfo.ToTitleCase(Arguments[0].ToLower());
        string[] competences = new string[] {textInfo.ToTitleCase(Arguments[1].ToLower()), textInfo.ToTitleCase(Arguments[2].ToLower()), textInfo.ToTitleCase(Arguments[3].ToLower()), textInfo.ToTitleCase(Arguments[4].ToLower()), textInfo.ToTitleCase(Arguments[5].ToLower())};

        Roles championRole = Roles.None;
        int RolestartIndex = 6;
        int RolesEndIndex = Arguments.Length - 1;
        string[] ChampionsRolesContent = Arguments.Skip(RolestartIndex).Take(RolesEndIndex - RolestartIndex).ToArray();
        string TransformedDifficulty = TransformToCorrectAccent(Arguments[Arguments.Length-1].ToUpper());
        if(!Difficulties.TryParse(TransformedDifficulty, out Difficulties ChampionDifficulty))
        {
            Console.Error.WriteLine("Le dernier élément entré n'est pas la difficulté de champions, votre chaîne entrée est inparsable.\nLes diffcultés sont : FAIBLE, MODÉRÉE et ÉLEVÉE");
            return;
        }
        foreach (string roleContent in ChampionsRolesContent)
        {
            if(!Roles.TryParse(roleContent.ToUpper(), out Roles OnechampionRole))
            {
                Console.Error.WriteLine($"{roleContent} n'est pas l'un des rôles de champions.");
                continue;
            }
            championRole |= OnechampionRole;
        }

        Champion c = new Champion(championName, competences, championRole, ChampionDifficulty);
        if (LesChampions.Contains(c) && !LesChampions.Same(c))
        {
            Console.Error.WriteLine($"{c.Name} existe déjà dans le ChampionDex, donc la commande `override` est exécutée à la place de `add`.");
            LesChampions.OverrideChampion(c);
            return;
        }
        else if (LesChampions.Contains(c) && LesChampions.Same(c))
        {
            Console.Error.WriteLine($"{c.Name} existe déjà dans le ChampionDex, et son ancienne entrée est identique à la nouvelle, donc pas d'opération.");
            return;
        }
        LesChampions.Add(c);
        Console.WriteLine($"Champion {championName} est ajouté au ChampionDex.");
    }
}