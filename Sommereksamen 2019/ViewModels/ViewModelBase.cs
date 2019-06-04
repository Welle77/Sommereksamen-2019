using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Prism.Mvvm;
using Sommereksamen_2019.Helpers;
using Sommereksamen_2019.Model;

namespace Sommereksamen_2019.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected Repository Repository;
    }
}