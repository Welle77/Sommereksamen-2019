using Prism.Mvvm;

namespace Sommereksamen_2019.Model
{
    public class MeasureTrees : BindableBase
    {
        private int _amount;
        public int Amount
        {
            get => _amount;
            set =>SetProperty(ref _amount, value);
        }

        private TreeType _treeSort;
        public TreeType TreeSort
        {
            get => _treeSort;
            set => SetProperty(ref _treeSort, value);
        }
    }
}