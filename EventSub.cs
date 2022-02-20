using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace Core
{
    public class EventSub
    {
        public static TwitchAPI TwitchAPI = new();

        public EventSub()
        {
            TwitchAPI.Settings.AccessToken = Bot.Token;
            TwitchAPI.Settings.ClientId = Bot.ClientID;
            StartUpdateTimer();
        }

        private void StartUpdateTimer()
        {
            System.Timers.Timer timer = new();

            timer.Interval = 10000;
            timer.AutoReset = true;
            timer.Enabled = true;

            timer.Elapsed += async (s, e) => { await FetchTagUpdates(); };
        }

        private async Task FetchTagUpdates()
        {
            //
        }
    }
}
