using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace Sommereksamen_2019.Model
{
    public class TreeData : BindableBase
    {
        public TreeData()
        {
            MeasureTrees = new ObservableCollection<MeasureTrees>();
        }
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _street;
        public string Street
        {
            get => _street;
            set => SetProperty(ref _street, value);
        }

        private string _streetNumber;
        public string StreetNumber
        {
            get => _streetNumber;
            set => SetProperty(ref _streetNumber, value);
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private ObservableCollection<MeasureTrees> _measureTrees;

        public ObservableCollection<MeasureTrees> MeasureTrees
        {
            get => _measureTrees;
            set => SetProperty(ref _measureTrees, value);
        }
    }
}