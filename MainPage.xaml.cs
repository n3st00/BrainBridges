using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
    /// 
    
    
    public sealed partial class MainPage : Page
    {
        TabViewItem existingNewProjectTab = null;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CreateNewTabTabView_MP(string header)
        {
            var newTab = new TabViewItem { Header = header };
            TabView_MP.TabItems.Add(newTab);
        }
        private void TabView_MP_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            var newTab = new TabViewItem { Header = "Blank Page" };
            newTab.ContentTemplate = (DataTemplate)Resources["NewPageTemplate"];
            TabView_MP.TabItems.Add(newTab);
        }

        private void TabView_MP_TabCloseRequested(Microsoft.UI.Xaml.Controls.TabView sender, Microsoft.UI.Xaml.Controls.TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
            if (args.Tab.Header == "Create New Project")
            {
                existingNewProjectTab = null;
            }
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
            await JsonFileHelper.ReadFromJsonFile(fileName);
            
        }

        private void MenuNewItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabViewItem tabItem in TabView_MP.TabItems)
            {
                if (tabItem.Header == "Create New Project")
                {
                    existingNewProjectTab = tabItem;
                }
            }
            if (existingNewProjectTab == null)
            {
                var newTab = new TabViewItem { Header = "Create New Project" };
                newTab.ContentTemplate = (DataTemplate)Resources["NewFileTemplate"];
                newTab.IsSelected = true;
                TabView_MP.TabItems.Add(newTab);
            } else
            {
                existingNewProjectTab.IsSelected = true;
            }
           
        }

        private async void CreateNewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            bool can_continue = true;
            string nametext = null;
            FrameworkElement button = sender as FrameworkElement;

            if (button != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(button); 
                while(parent != null && !(parent is ScrollViewer))
                {
                    parent = VisualTreeHelper.GetParent(button); break;
                }
                if (parent != null)
                {
                    TextBox nameTextBox = FindChildControl<TextBox>(parent, "NameTextBox");
                    TextBlock charTextBlock = FindChildControl<TextBlock>(parent, "CharactersTextBlock");
                    if (nameTextBox != null)
                    {
                        string text = nameTextBox.Text;
                        nametext = text;
                        if (text == "" || text.Contains(" ") || text.Contains("/") || text.Contains(".") || text.Contains("\\") || text.Contains("'") || text.Contains('"') || text.Length > 30)
                        {
                            nameTextBox.Focus(FocusState.Programmatic);
                            charTextBlock.Visibility = Visibility.Visible;
                            can_continue = false;
                            System.Diagnostics.Debug.WriteLine("name is wrong");
                        }
                        else
                        {
                            
                            System.Diagnostics.Debug.WriteLine("name is right");
                        }
                    }
                    TextBox descrTextBox = FindChildControl<TextBox>(parent, "DescriptionTextBox");
                    TextBlock lengthTextBlock = FindChildControl<TextBlock>(parent, "LengthTextBlock");
                    if (descrTextBox != null)
                    {
                        string text = descrTextBox.Text;
                        if (text.Length > 300)
                        {
                            descrTextBox.Focus(FocusState.Programmatic);
                            lengthTextBlock.Visibility = Visibility.Visible;
                            can_continue = false;
                            System.Diagnostics.Debug.WriteLine("description is wrong");
                        }
                    }
                }
                
            }
            if (can_continue == true)
            {
                await JsonFileHelper.TestCreateNewBlank(nametext);
                await JsonFileHelper.ClearJsonFile(nametext);
                await JsonFileHelper.TestReadFromJson(nametext);
                CreateNewTabTabView_MP(nametext);
            }

            
        }

        public static T FindChildControl<T>(DependencyObject control, string ctrlName) where T : FrameworkElement
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;
                // Not a framework element or is null
                if (fe == null) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    // Found the control so return
                    return (T)child;
                }
                else
                {
                    // Not found it - search children
                    T subItem = FindChildControl<T>(child, ctrlName);
                    if (subItem != null)
                        return subItem;
                }
            }
            return null;
        }
    }
}
