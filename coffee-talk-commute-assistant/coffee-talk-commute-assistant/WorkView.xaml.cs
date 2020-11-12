using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;

namespace coffee_talk_commute_assistant
{
    public sealed partial class WorkView : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Connection> Connections { get; set; } = new ObservableCollection<Connection>();

        private string home = "Augsburg";
        public string Home
        {
            get
            {
                return home;
            }
            set
            {
                home = value;
                NotifyPropertyChanged();
            }
        }

        private string work = "München Hbf";
        public string Work
        {
            get
            {
                return work;
            }
            set
            {
                work = value;
                NotifyPropertyChanged();
            }
        }

        public WorkView()
        {
            this.DataContext = this;
            this.InitializeComponent();
            InitializeConnections();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void InitializeConnections()
        {
            string locationid = await DBClient.getLocationId(Home);
            
            List<Connection> responseFromDB = await DBClient.getPossibleTrains(Work, locationid, DateTime.Now);
            Connections = new ObservableCollection<Connection>(responseFromDB);
            ListOfConnections.ItemsSource = Connections;
        }

        public async void UpdateConnections(String time)
        {
            Connections.ElementAt(0);
            
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateConnections(DateTime.Now.ToString());
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void ListOfConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ListOfConnections.SelectedItem as Connection;
            ContentDialog trainPromptDialog = new ContentDialog
            {
                Title = "Train selection",
                Content = "You selected the train: "+item.Name + " at: " + item.DateTime + ". Are you sure you want to take this train? ",
                CloseButtonText = "Cancel",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await trainPromptDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var content = new ToastContentBuilder().AddText("Your chosen Train departs soon").AddText(item.DateTime + " from " + item.Origin).AddHeroImage(new Uri("ms-appx:///Assets/train.jpg")).GetToastContent();
                Reminder.Remind(item.DateTime, new ToastNotification(content.GetXml()));
            }
            
        }
    }
}
