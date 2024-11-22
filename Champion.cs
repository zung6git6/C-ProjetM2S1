using System;
using System.Globalization;

public class Champion
{
    public string Name {get; private set;}
    public string[] Competences {get; private set;}
    public Roles Role {get; private set;}
    public Difficulties Difficulty {get; private set;}
    private string Sep = "\n";

    public Champion(string name, string[] competences, Roles role, Difficulties difficulty)
    {
        TextInfo textInfo = new CultureInfo("fr", false).TextInfo;
        Name = textInfo.ToTitleCase(name.ToLower());
        Competences = competences;
        CompetencesToTitleCase();
        Role = role;
        Difficulty = difficulty;
    }

    private void CompetencesToTitleCase()
    {
        TextInfo textInfo = new CultureInfo("fr", false).TextInfo;
        for (int i = 0; i < Competences.Length - 1; i++)
        {
            Competences[i] = textInfo.ToTitleCase(Competences[i].ToLower());
        }
    }

    public override string ToString()
    {
        string JoinCompetences = string.Join(", ", Competences);
        return $"Nom : {Name}{Sep}Compétences : {JoinCompetences}{Sep}Rôles : {Role}{Sep}Niveau Difficulté : {Difficulty}";
    }

}