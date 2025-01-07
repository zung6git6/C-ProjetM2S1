namespace ChampionDexNameSpace;
public class ChampionDto
{
    public string _name {private get; set;}
    public string[] _competences {private get; set;}
    public Roles _role {private get; set;}
    public Difficulties _difficulty {private get; set;}

    private string Sep = ";";

    public string WritingForm 
    {
        get
        {
            string JoinCompetences = string.Join(", ", _competences);
            return $"{_name}{Sep}{JoinCompetences}{Sep}{_role}{Sep}{_difficulty}";
        }
    }
}