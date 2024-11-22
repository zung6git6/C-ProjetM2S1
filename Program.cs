using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text.Json;

public class ChampionsWildRift
{
    public static void Main(string[] args)
    {
        ChampionDex championdex = new ChampionDex();
        bool flag = true; 
        while (flag)
        {
            Console.Write("$ ");
            string line = Console.ReadLine();
            string[] commandArgs = line.Split("\t", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Console.WriteLine(string.Join(" ", commandArgs));

            if (!line.Contains("\t") && commandArgs[0].ToLower() != "exit")
            {
                Console.Error.WriteLine("Vous avez peut-être oublié que le séparateur doit être la tabulation (Tab) ?");
                continue;
            }
            if (line.Trim().Contains(' ') && commandArgs[0].ToLower() != "add" && commandArgs[0].ToLower() != "load")
            {
                Console.Error.WriteLine("Vous avez peut-être oublié que le séparateur doit être la tabulation (Tab) ?");
                continue;
            }

            CommandInterpreter interpreter = new CommandInterpreter(championdex);
            Command cmd = interpreter.Interpret(commandArgs);
            if (cmd != null) {cmd.Execute();}
            // Console.WriteLine(championdex);
            Console.WriteLine();
        }
    }
}

// add
// scrap
// load
// save
// search
// doc, help
// 