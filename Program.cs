using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Core
{
    public class Core
    {
        public static DateTime StartupTime = DateTime.Now;

        static void Main(string[] args)
        {
            Bot bot = new();
            Console.ReadLine();
        }

    }

    public class Bot
    {
        public static readonly string Username = "TWITCH_USERNAME";
        public static readonly string Token = "ACCESS_TOKEN";
        public static readonly string ClientID = "CLIENT_ID";
        public static readonly string InitialChannel = "START_CHANNEL";

        public static TwitchClient Client = new();

        public Bot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(Username, Token);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            Client = new TwitchClient(customClient);
            Client.Initialize(credentials, InitialChannel);

            Client.OnJoinedChannel += (s, e) =>
            {
                Console.WriteLine($"Connected to {e.Channel}");
            };
            Client.OnConnected += (s, e) =>
            {
                Console.WriteLine("connected");
                EventSub eSub = new();
            };

            Client.Connect();
        }
    }
}