using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;
using Sommereksamen_2019.Helpers;
using Sommereksamen_2019.Model;

namespace Sommereksamen_2019.ViewModels
{
    public class AddLocationViewModel : ViewModelBase
    {

        public AddLocationViewModel()
        {
            TreeData = new TreeData();
            MeasureTrees = new ObservableCollection<MeasureTrees>();
            for (int i = 0; i < 4; i++)
            {
                MeasureTrees.Add(new MeasureTrees());
            }
            Repository = new Repository();
        }

        private TreeData _treeData;
        public TreeData TreeData
        {
            get => _treeData;
            set => SetProperty(ref _treeData, value);
        }

        private ObservableCollection<MeasureTrees> _measureTrees;

        public ObservableCollection<MeasureTrees> MeasureTrees
        {
            get => _measureTrees;
            set => SetProperty(ref _measureTrees, value);
        }


        private DelegateCommand _saveAsCommand;

        public ICommand SaveAsCommand => _saveAsCommand ??
                                       (_saveAsCommand = new DelegateCommand(OnSaveAsCommand));

        private void OnSaveAsCommand()
        {
            var tempList = new ObservableCollection<MeasureTrees>();
            foreach (var measureTreese in MeasureTrees)
            {
                if (measureTreese.Amount != 0)
                    tempList.Add(measureTreese);
            }
            TreeData.MeasureTrees = tempList;
            TreeData.Id = TreeData.Trees.Count;
            TreeData.Trees.Add(TreeData);
            Repository.SaveData(TreeData.Trees);
        }

        private DelegateCommand _saveCommand;

        public ICommand SaveCommand => _saveCommand ?? (
                                               _saveCommand = new DelegateCommand(OnSaveFileCommand));

        private void OnSaveFileCommand()
        {
            if (Repository.CurrentFileName == "")
            {
                MessageBox.Show("No file was selected");
                return;
            }
            var tempList = new ObservableCollection<MeasureTrees>();
            foreach (var measureTreese in MeasureTrees)
            {
                if (measureTreese.Amount != 0)
                    tempList.Add(measureTreese);
            }
            TreeData.MeasureTrees = tempList;
            TreeData.Id = TreeData.Trees.Count;
            TreeData.Trees.Add(TreeData);
            Repository.SaveData(TreeData.Trees, Repository.CurrentFileName);
        }

        private DelegateCommand _openFileCommand;

        public ICommand OpenFileCommand => _openFileCommand ?? (
                                               _openFileCommand = new DelegateCommand(OnOpenFileCommand));

        private async void OnOpenFileCommand()
        {
            var tempTreeData = await Repository.GetData();

        }
    }
}