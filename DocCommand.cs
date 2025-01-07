namespace ChampionDexNameSpace;

public class DocCommand : Command
{
    public DocCommand(ChampionDex championdex, string[] arguments):base(championdex, arguments) {}

    public override void Execute()
    {
        StreamReader reader = new StreamReader("Readme.md");
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line != null)
            {
                Console.WriteLine(line);
            }
        }
        Console.WriteLine("ANNEXE");
        Console.WriteLine("Liste de Champions existants :");
        StreamReader NameListReader = new StreamReader("Data/championsName.txt");
        while (!NameListReader.EndOfStream)
        {
            string line = NameListReader.ReadLine();
            if (line != null)
            {
                Console.WriteLine($"\t{line}");
            }
        }
        return;
    }
}