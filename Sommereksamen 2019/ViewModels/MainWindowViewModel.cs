using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;
using Sommereksamen_2019.Model;
using Sommereksamen_2019.Views;

namespace Sommereksamen_2019.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region NavCommands

        private DelegateCommand _addLocationCommand;

        public ICommand AddLocationCommand =>
            _addLocationCommand ?? (_addLocationCommand = new DelegateCommand(OnAddLocation));

        private void OnAddLocation()
        {
            var window = new AddLocationView();
            window.ShowDialog();
        }

        private DelegateCommand _searchDelegateCommand;
        public ICommand SearchLocationCommand =>
            _searchDelegateCommand ?? (_searchDelegateCommand = new DelegateCommand(OnSearchLocation));

        private void OnSearchLocation()
        {
            //https://stackoverflow.com/questions/18244196/avoid-opening-same-window-twice-wpf
            var isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (!(w is SearchLocationView)) continue;
                isWindowOpen = true;
                w.Activate();
            }

            if (isWindowOpen) return;
            var newWindow = new SearchLocationView();
            newWindow.Show();

        }

        #endregion
    }
}