using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WpfTest.UI.ViewModels.CustomerViewModels;
using WpfTest.UI.ViewModels.PersonViewModels;
using WpfTest.UI.ViewModels.StoreViewModels;

namespace WpfTest.UI.ViewModels
{
    public class ShopManagementViewModel : INotifyPropertyChanged
    {
        private StoresTabViewModel storesTabViewModel;

        public StoresTabViewModel StoresTabViewModel
        {
            get { return storesTabViewModel; }
            set { storesTabViewModel = value; OnPropertyChanged("StoresTabViewModel"); }
        }

        private CustomersTabViewModel customersTabViewModel;

        public CustomersTabViewModel CustomersTabViewModel
        {
            get { return customersTabViewModel; }
            set { customersTabViewModel = value; OnPropertyChanged("CustomersTabViewModel"); }
        }

        private PersonsTabViewModel personsTabViewModel;

        public PersonsTabViewModel PersonsTabViewModel
        {
            get { return personsTabViewModel; }
            set { personsTabViewModel = value; OnPropertyChanged("PersonsTabViewModel"); }
        }

        public ShopManagementViewModel()
        {
            storesTabViewModel = new StoresTabViewModel();
            customersTabViewModel = new CustomersTabViewModel();
            personsTabViewModel = new PersonsTabViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
