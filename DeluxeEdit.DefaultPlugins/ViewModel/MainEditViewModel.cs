﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public class MainEditViewModel: INotifyPropertyChanged
    {
        private MainEdit mainEdit;

        // Declare the event
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainEditViewModel()
        {
            mainEdit = new MainEdit();
            
        }

        //      OnPropertyChange();

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.

        public void Show()

        {
            OnPropertyChanged(); 
        }
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            if (mainEdit != null && mainEdit.MainEditBox != null && mainEdit.MainEditBox.Text != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(mainEdit.MainEditBox.Text));
            }
        }   
    }
}
