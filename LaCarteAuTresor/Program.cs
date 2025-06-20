using System;

namespace LaCarteAuTresor;
public class Program
{
    static void Main(string[] args)
    {
        string default_message = @"{start the adventure} -> LaCarteAuTresor --play filePathInput filePathOutput
                {launch unit tests} -> LaCarteAuTresor --test
                {informations} -> LaCarteAuTresor --help";
        if (args.Length == 0)
        {
            Console.WriteLine(default_message);
        }
        else
        {
            var programAction = args[0].ToString();
            switch (programAction)
            {
                case "--help":
                    Console.WriteLine(default_message);
                    break;
                case "--play":
                    if (!string.IsNullOrEmpty(args[1].ToString()) && !string.IsNullOrEmpty(args[2].ToString()))
                    {
                        var inputFilePath = args[1].ToString();
                        var outputFilePath = args[2].ToString();

                        var adventure = new Adventure(inputFilePath, outputFilePath);
                        adventure.Play();
                    }
                    else
                        Console.WriteLine(default_message);
                    break;
                case "--test":
                    break;

            }
            if (args[0].ToString() == "--help")
            {
                Console.WriteLine(default_message);
            }
            else
            {

            }

        }
    }
}
