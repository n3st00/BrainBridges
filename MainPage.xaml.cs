using Microsoft.UI.Xaml.Controls;
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

namespace BrainBridges
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        
        private void TabView_MP_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            var newTab = new TabViewItem { Header = "New Tab" };
            TabView_MP.TabItems.Add(newTab);
        }

        private void TabView_MP_TabCloseRequested(Microsoft.UI.Xaml.Controls.TabView sender, Microsoft.UI.Xaml.Controls.TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

       

        private async void JsonButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "data.json"; // json file test
            PointStr point = new PointStr
            {
                name = "Johnnn",
                index = "index1",
                description = "description",
                connections = new List<string>{"connection1", "connection2"}
            };
           
            object newData = new { Name = "Janggg", Age = 15, newParam = 34, someother = "dasdasd"}; //new data test
            /*            await JsonFileHelper.WriteToJsonFile(fileName, newData);
             *                        await JsonFileHelper.WriteObjectToJson(fileName, point);
            */
            /*await JsonFileHelper.WriteObjectToJson(fileName, point);
            await JsonFileHelper.ReadFromJsonFile(fileName)*/;
            await JsonFileHelper.ClearJsonFile(fileName);
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
