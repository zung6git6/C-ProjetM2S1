namespace ChampionDexNameSpace;

public abstract class Command
{
    protected ChampionDex LesChampions;
    protected bool EstOnBon = true;
    protected string[] Arguments;

    public Command(ChampionDex championdex, string[] arguments)
    {
        LesChampions = championdex;
        Arguments = arguments;
    }

    public abstract void Execute();
}