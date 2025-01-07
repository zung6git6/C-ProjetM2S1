namespace ChampionDexNameSpace;

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

    public ChampionDexDto ToDto()
    {
        Func<Champion, ChampionDto> lambda = PARAM => PARAM.ToDto();
        return new ChampionDexDto
        {
            // On converti tous les pokémons en leur version DTO
            _LesChampions = LesChampions
                .Where(p => p != null)
                .Select(lambda)
                .ToList()
        };
    }
}