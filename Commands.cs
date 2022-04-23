using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchLib.Api.Helix;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Core;

namespace Core
{
    internal static class Commands
    {
        private static Helix Helix { get; } = new Helix(settings: new ApiSettings() { AccessToken = Core.Config.access_token, ClientId = Core.Config.client_id });
        public static void UpdateConfig()
        {
            try
            {
                File.WriteAllText("config.json", JsonSerializer.Serialize(Core.Config));
            }
            // lol
            catch (IOException)
            {
                UpdateConfig();
            }
        }

        public static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Commands: \n\n");
            Console.WriteLine("------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"edit <channel> <unsub/sub> <live/title/game/offline>");
            Console.WriteLine($"edit <channel> update <live/title/game/offline> <new message>");
            Console.WriteLine("edit token <new token>");
            Console.WriteLine("edit clientid <new client id>\n");

            Console.WriteLine($"add <channel>\n");

            Console.WriteLine($"remove <channel>\n");

            Console.WriteLine($"disconnect");
            Console.WriteLine($"connect \\\\The program automatically connects, only use this when you disconnected manually\n");

            Console.WriteLine($"exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static async Task AddChannel(string channel)
        {
            Channel _channel = new Channel();
            GetUsersResponse user = await Helix.Users.GetUsersAsync(logins: new List<string> { channel });
            _channel.name = channel;
            _channel.avatar_url = user.Users.FirstOrDefault()!.ProfileImageUrl;

            Console.WriteLine("Webhook link for this channel: ");
            string? webhookLink = Console.ReadLine();

            Console.WriteLine("Send notification for going live? (yes/no)");
            string? live = Console.ReadLine();
            bool nLive = live == "yes";
            if (nLive)
            {
                Console.WriteLine("What text should be sent when this happens? e.g: You can make this event ping a specific role (leave empty to ignore)");
                _channel.live_message = Console.ReadLine();
            }

            Console.WriteLine("Send notification for title change? (yes/no)");
            string? title = Console.ReadLine();
            bool nTitle = title== "yes";
            if (nTitle)
            {
                Console.WriteLine("What text should be sent when this happens? e.g: You can make this event ping a specific role (leave empty to ignore)");
                _channel.title_message = Console.ReadLine();
            }

            Console.WriteLine("Send notification for game change? (yes/no)");
            string? game = Console.ReadLine();
            bool nGame = game == "yes";
            if (nGame)
            {
                Console.WriteLine("What text should be sent when this happens? e.g: You can make this event ping a specific role (leave empty to ignore)");
                _channel.game_message = Console.ReadLine();
            }

            Console.WriteLine("Send notification for going offline? (yes/no)");
            string? offline = Console.ReadLine();
            bool nOffline = offline == "yes";
            if (nOffline)
            {
                Console.WriteLine("What text should be sent when this happens? e.g: You can make this event ping a specific role (leave empty to ignore)");
                _channel.offline_message = Console.ReadLine();
            }

            _channel.webhook_link = webhookLink;
            _channel.live_sub = nLive;
            _channel.title_sub = nTitle;
            _channel.game_sub = nGame;
            _channel.offline_sub = nOffline;

            List<Channel> channels = (Core.Config.channels ?? Array.Empty<Channel>()).ToList();
            channels.Add(_channel);
            Core.Config.channels = channels.ToArray();
            UpdateConfig();
        }
    }
}
