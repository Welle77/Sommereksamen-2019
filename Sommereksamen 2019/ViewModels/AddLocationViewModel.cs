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
        private ObservableCollection<TreeData> _treeDatas;

        public AddLocationViewModel()
        {
            TreeDatasData = new TreeData();
            Repository = new Repository();
            _treeDatas = new ObservableCollection<TreeData>();
            MeasureTrees = new ObservableCollection<MeasureTrees>();
            for (var i = 0; i < 4; i++)
            {
                MeasureTrees.Add(new MeasureTrees());
            }
        }

        private TreeData _treeData;
        public TreeData TreeDatasData
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
            TreeDatasData.MeasureTrees = tempList;
            TreeDatasData.Id = _treeDatas.Count;
            _treeDatas.Add(TreeDatasData);
            Repository.SaveData(_treeDatas);
        }

        private DelegateCommand _saveCommand;

        public ICommand SaveCommand => _saveCommand ?? (
                                               _saveCommand = new DelegateCommand(OnSaveFileCommand));

        private async void OnSaveFileCommand()
        {
            if (Repository.CurrentFileName == "")
            {
                MessageBox.Show("No file was selected");
                return;
            }

            _treeDatas = await Repository.UpdateData();

            var tempList = new ObservableCollection<MeasureTrees>();
            foreach (var measureTrees in MeasureTrees)
            {
                if (measureTrees.Amount != 0)
                    tempList.Add(measureTrees);
            }
            TreeDatasData.MeasureTrees = tempList;
            TreeDatasData.Id = _treeDatas.Count;
            _treeDatas.Add(TreeDatasData);
            Repository.SaveData(_treeDatas, Repository.CurrentFileName);
        }

        private DelegateCommand _openFileCommand;

        public ICommand OpenFileCommand => _openFileCommand ?? (
                                               _openFileCommand = new DelegateCommand(OnOpenFileCommand));

        private async void OnOpenFileCommand()
        {
             _treeDatas = await Repository.GetData();
        }
    }
}