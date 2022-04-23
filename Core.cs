using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using System.Text.Json;

namespace Core
{
    public static class Core
    {
        public static Config Config { get; private set; } = default!;

        static async Task Main(string[] args)
        {
            StreamReader reader;
            try { reader = new StreamReader("config.json"); }
            catch (FileNotFoundException) 
            {
                JsonSerializerOptions options = new() { WriteIndented = true };
                await File.WriteAllTextAsync("config.json", JsonSerializer.Serialize(new Config(), options: options));
                reader = new StreamReader("config.json");
            }
            string json = await reader.ReadToEndAsync();
            Config = JsonSerializer.Deserialize<Config>(json)!;

            reader.Close();
            reader.Dispose();

            while (true)
            {
                if (string.IsNullOrEmpty(Config.access_token))
                {
                    Console.WriteLine("Access Token: ");
                    Config.access_token = Console.ReadLine();
                    await Commands.UpdateConfig();
                    continue;
                }
                if (string.IsNullOrEmpty(Config.client_id))
                {
                    Console.WriteLine("Client ID: ");
                    Config.client_id = Console.ReadLine();
                    await Commands.UpdateConfig();
                    continue;
                }
                string? input = Console.ReadLine();
                if (input is not null)
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
                            await Commands.AddChannel(input!.Split(' ')[1]);
                            break;
                        case "remove":
                            await Commands.RemoveChannel(input!.Split(' ')[1]);
                            break;
                        case "help":
                            Commands.PrintHelp();
                            break;
                        case "exit":
                            break;

                        default:
                            Console.WriteLine("Unrecognized command! Type help for a list of commands.");
                            break;
                    }
                }
                if (input is not null && input.StartsWith("exit"))
                {
                    break;
                }
            }
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
