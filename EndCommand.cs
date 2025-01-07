namespace ChampionDexNameSpace;

public class EndCommand : Command
{
    public EndCommand(ChampionDex championdex, string[] arguments):base(championdex, arguments) {}

    public override void Execute()
    {
        Console.WriteLine("Le programme va maintenant se terminer. Merci d'avoir utilisé Wild Rift ChampionDex !");
        Environment.Exit(0);
        return;
    }
}