namespace ChampionDexNameSpace;
public class CommandInterpreter
{
    public ChampionDex LesChampions {get; private set;}

    public CommandInterpreter(ChampionDex championdex)
    {
        LesChampions = championdex;
    }
    public Command Interpret(string[] args)
    {
        if (args.Length < 0)
        {
            throw new ArgumentException("Not enough arguments.");
        }
        string TypeCommand = args[0].ToLower();
        switch (TypeCommand)
        {
            case "add":
                AddCommand cmdAdd = new AddCommand(LesChampions, args.Skip(1).ToArray());
                return cmdAdd;
            case "search":
                SearchCommand cmdSearch = new SearchCommand(LesChampions, args.Skip(1).ToArray());
                return cmdSearch;
            case "scrape":
                ScrapeCommand cmdScrape = new ScrapeCommand(LesChampions, args.Skip(1).ToArray());
                return cmdScrape;
            case "save":
                SaveCommand cmdSave = new SaveCommand(LesChampions, args.Skip(1).ToArray());
                return cmdSave;
            case "load":
                LoadCommand cmdLoad = new LoadCommand(LesChampions, args.Skip(1).ToArray());
                return cmdLoad;
            case "override":
                OverrideCommand cmdOverride = new OverrideCommand(LesChampions, args.Skip(1).ToArray());
                return cmdOverride;
            case "exit":
                EndCommand cmdEnd = new EndCommand(LesChampions, args.Skip(1).ToArray());
                return cmdEnd;
            case "savetxt":
                SaveTxtCommand cmdSaveTxt = new SaveTxtCommand(LesChampions, args.Skip(1).ToArray());
                return cmdSaveTxt;
            case "loadtxt":
                LoadTxtCommand cmdLoadTxt = new LoadTxtCommand(LesChampions, args.Skip(1).ToArray());
                return cmdLoadTxt;
            case "-h":
                DocCommand cmdDoc = new DocCommand(LesChampions, args.Skip(1).ToArray());
                return cmdDoc;
            default:
                Console.Error.WriteLine("Command Invalide !");
                return null;
        }
    }
}