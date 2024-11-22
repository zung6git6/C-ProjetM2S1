using System;
using System.Linq;

public class ChampionDex
{
    List<Champion> LesChampions;

    public ChampionDex()
    {
        LesChampions = new List<Champion>();
    }

    public ChampionDex(List<Champion> champions)
    {
        LesChampions = champions;
    }

    public void Add(Champion champion)
    {
        LesChampions.Add(champion);
    }

    public bool Contains(Champion champion)
    {
        bool ContainFlag = false;
        string ChampionNameToLookUp = champion.Name;
        foreach (Champion c in LesChampions)
        {
            if (c.Name == ChampionNameToLookUp)
            {
                ContainFlag = true;
            }        
        }
        return ContainFlag;
    }

    public bool Same(Champion champion)
    {
        bool SameFlag = false;
        foreach (Champion c in LesChampions)
        {
            if (c.Competences.SequenceEqual(champion.Competences) && c.Role == champion.Role && c.Difficulty == champion.Difficulty)
            {
                SameFlag = true;
            }
        }
        return SameFlag;
    }

    public bool ISNull()
    {
        return LesChampions.Count == 0;
    }

    public void OverrideChampion(Champion champion)
    {
        int index = LesChampions.FindIndex(c => c.Name == champion.Name);
        LesChampions[index] = champion;
        return;
    }

    public void SaveJson(string CheminOutput="Data/ScrappedChampions.json")
    {
        if (LesChampions == null)
        {
            Console.Error.WriteLine("Le ChamionDex est vide. Rien est sauvegardé.");
            return;
        }
        string jsonOutput = System.Text.Json.JsonSerializer.Serialize(LesChampions, new System.Text.Json.JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
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

    public override string ToString()
    {
        if (LesChampions == null)
        {
            Console.WriteLine("Le ChampionDex est vide.");
            return  "";
        }

        string output = "";
        foreach (Champion champion in LesChampions)
        {
            output += champion.ToString() + "\n";
        }
        return output;
    }

    public Champion GetByName(string name)
    {
        foreach (Champion c in LesChampions)
        {
            if (c != null && c.Name.ToLower() == name.ToLower())
            {
                return c;
            }
        }
        return null;
    }

    public IEnumerable<Champion> GetByType(Roles r)
    {
        foreach (Champion c in LesChampions)
        {
            if (c != null && (c.Role & r) == r)
            {
                yield return c;
            }
        }
    }

    public IEnumerable<Champion> GetByDifficulty(Difficulties d)
    {
        foreach (Champion c in LesChampions)
        {
            if (c != null && (c.Difficulty & d) == d)
            {
                yield return c;
            }
        }
    }
}