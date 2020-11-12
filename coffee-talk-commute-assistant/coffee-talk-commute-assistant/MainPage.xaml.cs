using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace coffee_talk_commute_assistant
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Current = this;

            contentFrame.Navigate(typeof(LandingPage));
        }

        /// <summary>
        /// Gets the current MainPage.
        /// </summary>
        public static MainPage Current { get; private set; }

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            
            var menuItems = sender.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().ToList();
            sender.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().ToList().ForEach(x => menuItems.AddRange(x.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()));
            var item = menuItems.First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item);
            
        }

        private void NavView_Navigate(Microsoft.UI.Xaml.Controls.NavigationViewItem item)
        {
            // close all expanded items
            if (mainNavView.MenuItems.Contains(item))
            {
                mainNavView.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                    .ToList()
                    .ForEach(x => x.IsExpanded = false);
            }

            switch (item.Tag)
            {
                case "ConnectionsView":
                    contentFrame.Navigate(typeof(WorkView));
                    break;
                case "LandingView":
                    contentFrame.Navigate(typeof(LandingPage));
                    break;
            }
        }
    }
}
