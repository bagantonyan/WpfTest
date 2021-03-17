using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfTest.Models.Models;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;

namespace WpfTest.UI.ViewModels.CustomerViewModels
{
    public class AddCustomerViewModel : INotifyPropertyChanged
    {
        private CustomerService customerService;
        private StoreService storeService;
        private PersonService personService;
        private List<Person> persons;
        private List<Store> stores;
        private Store selectedStore;
        private Person selectedPerson;
        public event EventHandler RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public List<Person> Persons
        {
            get { return persons; }
            set { persons = value; OnPropertyChanged("Persons"); }
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

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value; OnPropertyChanged("SelectedPerson"); }
        }

        public AddCustomerViewModel()
        {
            customerService = new CustomerService();
            storeService = new StoreService();
            personService = new PersonService();
            LoadPersons();
            LoadStores();
            AddCommand = new DelegateCommand(OnAddCommand, CanAdd);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        private bool CanAdd(object arg)
        {
            if (SelectedPerson != null && SelectedStore != null)
                return true;
            else
                return false;
        }

        private async void OnAddCommand(object obj)
        {
            var newCustomer = new Customer
            {
                FirstName = SelectedPerson.FirstName,
                LastName = SelectedPerson.LastName,
                PersonId = SelectedPerson.PersonId,
                StoreId = SelectedStore.StoreId,
                ModifiedDate = DateTime.Now,
            };

            string json = JsonConvert.SerializeObject(newCustomer);
            await customerService.AddCustomer(json);
            OnRequestClose();
        }

        private async void LoadPersons()
        {
            Persons = await personService.GetAllPersons();
        }

        private async void LoadStores()
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
