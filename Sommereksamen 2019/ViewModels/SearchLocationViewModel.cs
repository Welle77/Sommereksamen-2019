using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Sommereksamen_2019.Helpers;
using Sommereksamen_2019.Model;

namespace Sommereksamen_2019.ViewModels
{
    public class SearchLocationViewModel : ViewModelBase
    {
        private string _currentFile;
        public string CurrentFile
        {
            get => _currentFile;
            set
            {
                SetProperty(ref _currentFile, value);
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<TreeData> _trees;
        public ObservableCollection<TreeData> Trees
        {
            get => _trees;
            set => SetProperty(ref _trees, value);
        }

        private TreeData _currentTreeData;

        public TreeData CurrentTreeData
        {
            get => _currentTreeData;
            set => SetProperty(ref _currentTreeData, value);
        }


        public SearchLocationViewModel()
        {
            Repository = new Repository();
            Trees = new ObservableCollection<TreeData>();
            Location = "";
            CurrentFile = "";
        }

        private string _location;

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private DelegateCommand _openFileCommand;

        public ICommand OpenFileCommand => _openFileCommand ?? (
                                               _openFileCommand = new DelegateCommand(OnOpenFileCommand));

        private async void OnOpenFileCommand()
        {
            var tempTreeData = await Repository.GetData();

            foreach (var treeData in tempTreeData)
            {
                    Trees.Add(treeData);
            }

            if (Trees.Count == 0)
            {
                MessageBox.Show("Nothing was found in that file");
            }

            CurrentFile = Repository.CurrentFileName;

        }

        private DelegateCommand _changeLocationCommand;

        public ICommand ChangeLocationCommand => _changeLocationCommand ?? (
                                                     _changeLocationCommand = new DelegateCommand(OnChangeLocation, ChangeLocationCanExecute)).ObservesProperty(() => CurrentFile);

        private bool ChangeLocationCanExecute()
        {
            return CurrentFile != "";
        }

        private async void OnChangeLocation()
        {
            Trees.Clear();
            var tempTreeData = await Repository.UpdateData();

            if (CurrentFile == "")
            {
                foreach (var treeData in tempTreeData)
                {
                        Trees.Add(treeData);
                }
            }
            else
            {
                foreach (var treeData in tempTreeData)
                {
                    if (treeData.Name.Contains(Location))
                    {
                        Trees.Add(treeData);
                    }
                }
            }

            if (Trees.Count == 0)
            {
                MessageBox.Show("Nothing was found in that file");
            }
        }
    }
}