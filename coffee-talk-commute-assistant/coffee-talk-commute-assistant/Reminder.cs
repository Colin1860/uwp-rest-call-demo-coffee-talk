using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace coffee_talk_commute_assistant
{
    class Reminder
    {
        private ToastNotification Toast;

        private Timer Timer { get; set; }

        private Reminder(TimeSpan tillRemind, ToastNotification toShow)
        {
            Toast = toShow;
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            Timer.Interval = tillRemind.TotalMilliseconds;
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ToastNotifier toastNotifier = ToastNotificationManager.CreateToastNotifier();
            toastNotifier.Show(Toast);
            Timer.Stop();
        }

        public static void Remind(string time, ToastNotification toast)
        {
            var parsedTime = DateTime.Parse(time);
            var remaining = parsedTime - DateTime.Now;
            TimeSpan whenToRemind;

            if (remaining.TotalMinutes >= 30)
            {
                whenToRemind = parsedTime.AddMinutes(-30) - DateTime.Now;
            }
            else
            {
                whenToRemind = TimeSpan.FromSeconds(1);
            }

            Reminder reminder = new Reminder(whenToRemind, toast);
        }
    }
}
