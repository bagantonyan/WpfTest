using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WpfTest.UI.Services;
using WpfTest.Models.Models;
using System.Windows.Input;
using WpfTest.UI.Commands;
using WpfTest.UI.Views.CustomerViews;
using System.Net.Http;
using WpfTest.UI.ViewModels.CommonViewModels;
using WpfTest.UI.Views.CommonViews;

namespace WpfTest.UI.ViewModels.CustomerViewModels
{
    public class CustomersTabViewModel : INotifyPropertyChanged
    {
        private bool isTabSelected;
        private CustomerService customerService;
        private List<Customer> customers;
        private Customer selectedCustomer;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddCustomerCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }

        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }

        public bool IsTabSelected
        {
            get { return isTabSelected; }
            set
            {
                isTabSelected = value;
                OnPropertyChanged("IsTabSelected");
                if (value)
                {
                    LoadData();
                }
            }
        }

        private bool isBusy = true;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public List<Customer> Customers
        {
            get { return customers; }
            set { customers = value; OnPropertyChanged("Customers"); }
        }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }

        public CustomersTabViewModel()
        {
            customerService = new CustomerService();
            AddCustomerCommand = new DelegateCommand(OnAddCustomerCommand);
            RefreshDataCommand = new DelegateCommand(OnRefreshDataCommand);

            EditCustomerCommand = new DelegateCommand(OnEditCustomerCommand, CanEditCustomer);
            DeleteCustomerCommand = new DelegateCommand(OnDeleteCustomerCommand, CanDeleteCustomer);
        }

        private void OnRefreshDataCommand(object obj)
        {
            LoadData();
        }

        private void OnAddCustomerCommand(object obj)
        {
            var newCustomerDialog = new AddCustomerView();
            newCustomerDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            newCustomerDialog.ShowDialog();
        }

        private bool CanEditCustomer(object obj)
        {
            return (SelectedCustomer != null);
        }

        private void OnEditCustomerCommand(object obj)
        {
            var e = new EditCustomerViewModel(SelectedCustomer);
            var editCustomerDialog = new EditCustomerView();
            e.RequestClose += (s, e) => editCustomerDialog.Close();

            editCustomerDialog.DataContext = e;
            editCustomerDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            editCustomerDialog.ShowDialog();
        }

        private bool CanDeleteCustomer(object obj)
        {
            return (SelectedCustomer != null);
        }

        private void OnDeleteCustomerCommand(object obj)
        {
            var viewModel = new DeleteItemViewModel();
            viewModel.CustomerId = SelectedCustomer.CustomerId;
            var newDeleteDialog = new DeleteItemView();
            newDeleteDialog.DataContext = viewModel;
            viewModel.RequestClose += (s, e) => newDeleteDialog.Close();
            newDeleteDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            newDeleteDialog.ShowDialog();
        }

        private async void LoadData()
        {
            IsBusy = true;
            Customers = await customerService.GetAllCustomers();
            IsBusy = false;
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
