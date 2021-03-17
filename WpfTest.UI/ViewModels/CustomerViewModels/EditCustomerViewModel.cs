using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfTest.Models.Models;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;

namespace WpfTest.UI.ViewModels.CustomerViewModels
{
    public class EditCustomerViewModel : INotifyPropertyChanged
    {
        private CustomerService customerService;
        private StoreService storeService;
        private List<Store> stores;
        private Store selectedStore;
        private Customer _editCustomer;
        public event EventHandler RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand EditCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public Customer EditCustomer
        {
            get { return _editCustomer; }
            set { _editCustomer = value; OnPropertyChanged("EditCustomer"); }
        }

        public List<Store> Stores
        {
            get { return stores; }
            set { stores = value; OnPropertyChanged("Stores"); }
        }

        public Store SelectedStore
        {
            get { return selectedStore; }
            set { selectedStore = value; OnPropertyChanged("SelectedStore"); }
        }

        public EditCustomerViewModel()
        {
            customerService = new CustomerService();
            storeService = new StoreService();
            EditCommand = new DelegateCommand(OnEditCommand, CanEdit);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        public EditCustomerViewModel(Customer customer) : this()
        {
            LoadStores();
            EditCustomer = customer;
        }

        private bool CanEdit(object arg)
        {
            return !string.IsNullOrEmpty(EditCustomer.FirstName) && !string.IsNullOrEmpty(EditCustomer.LastName) && EditCustomer.StoreId > 0;
        }

        private async void OnEditCommand(object obj)
        {
            string json = JsonConvert.SerializeObject(EditCustomer);
            await customerService.UpdateCustomer(json);
            OnRequestClose();
        }

        public async void LoadStores()
        {
            Stores = await storeService.GetAllStores();
        }

        private void OnCloseCommand(object obj)
        {
            OnRequestClose();
        }

        protected void OnRequestClose()
        {
            if (RequestClose != null)
                RequestClose(this, EventArgs.Empty);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
