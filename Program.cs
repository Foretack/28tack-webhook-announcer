using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using Newtonsoft.Json;

namespace Core
{
    public static class Core
    {
        public static Config Config { get; private set; } = default!;

        private static string[] Commands { get; } = { "connect", "disconnect", "edit", "add", "remove", "help"};
        static async Task Main(string[] args)
        {
            StreamReader reader = new StreamReader("config.json");
            string json = reader.ReadToEnd();
            Config = JsonConvert.DeserializeObject<Config>(json)!;

            reader.Close();
            reader.Dispose();

            while (true)
            {
                string? input = Console.ReadLine();
                if (input is not null && !Commands.Contains(input!.Split(' ')[0]))
                {
                    string command = input!.Split(' ')[0];
                    switch (command)
                    {
                        case "connect":
                            break;
                        case "disconnect":
                            break;
                        case "edit":
                            break;
                        case "add":
                            break;
                        case "remove":
                            break;

                        default:
                            Console.WriteLine("Unrecognized input!");
                            PrintHelp();
                            break;
                    }
                }
                if (input is not null && input.StartsWith("exit"))
                {
                    break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Commands: \n\n");
            Console.WriteLine("------------------------------------------------------");
            Console.ForegroundColor= ConsoleColor.Blue;

            Console.WriteLine($"edit <channel> <unsub/sub> <live/title/game/offline>");
            Console.WriteLine($"edit <channel> update <live/title/game/offline> <new message>\n");

            Console.WriteLine($"add <channel>\n");

            Console.WriteLine($"remove <channel>\n");

            Console.WriteLine($"disconnect");
            Console.WriteLine($"connect \\\\The program automatically connects, only use this when you disconnected manually\n");

            Console.WriteLine($"exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    

    public class Config
    {
        public string? access_token { get; set; }
        public string? client_id { get; set; }
        public Channel[]? channels { get; set; }
    }

    public class Channel
    {
        public string? name { get; set; }
        public string? avatar_url { get; set; }
        public string? webhook_link { get; set; }
        public bool live_sub { get; set; }
        public bool title_sub { get; set; }
        public bool game_sub { get; set; }
        public bool offline_sub { get; set; }
        public string? live_message { get; set; }
        public string? title_message { get; set; }
        public string? game_message { get; set; }
        public string? offline_message { get; set; }
    }
}
