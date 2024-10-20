﻿using DefaultPlugins.ViewModel;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
namespace DeluxeEdit.DefaultPlugins.Views
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary> 
    public partial class MainEdit: UserControl
    {

        private MainEditViewModel editViewModel;
        private NewFileViewModel newViewModel;
        public MainEdit()
        {
            InitializeComponent();
            editViewModel = new MainEditViewModel(TabFiles, Progress, ProgressText, StatusText);
                
            newViewModel = new NewFileViewModel(TabFiles);

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
           // newViewModel.AddNewTextControlAndSubscr
    
            var builder = new MenuBuilder();
            var menu = builder.BuildMenu();

            builder.ShowMenu(this.MainMenu, menu);
            foreach (MenuItem item in MainMenu.Items)
                item.Click += MenuItem_Click;

                
       }

        private  async void MainEditBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var keyeddata =  await editViewModel.KeyDown();
            
            if (keyeddata == null) e.Handled = false;
            else
            {
                MainEditBox.Text = keyeddata.Content;

             //WPFUtil.AddOrUpddateTab(keyeddata.Header, TabFiles);
                e.Handled = true;
            }


        }


        private async void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.Source is MenuItem)
            {
                var clicked = e.Source as MenuItem;

                 await editViewModel.DoCommand(clicked, MainEditBox.SelectedText);

            }
        }

        private void ShowPlugins_Click(object sender, RoutedEventArgs e)
        {
            Plugins.CreateAndShow();

        }

        private void TabFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    if (TabFiles.Items.Count <= 1) return;
            editViewModel.ChangeTab(TabFiles.SelectedItem as TabItem);


        }

        private void MainEditBox_Scroll_1(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void MainEditBox_Scroll_2(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void MainEditBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
                
             

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
             var data = await editViewModel.LoadFile();
            
            
            
            
            
            
            
            
            
    //        MainEditBox.Text = data.Content;
//

        }
    }

}