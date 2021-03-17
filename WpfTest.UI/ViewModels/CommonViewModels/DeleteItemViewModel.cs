using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;

namespace WpfTest.UI.ViewModels.CommonViewModels
{
    public class DeleteItemViewModel : INotifyPropertyChanged
    {
        private StoreService storeService;
        private CustomerService customerService;
        private PersonService personService;

        public event EventHandler RequestClose;
        public ICommand CloseCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private long storeId;
        public long StoreId
        {
            get { return storeId; }
            set { storeId = value; OnPropertyChanged("StoreId"); }
        }

        private long customerId;
        public long CustomerId
        {
            get { return customerId; }
            set { customerId = value; OnPropertyChanged("CustomerId"); }
        }

        private long personId;
        public long PersonId
        {
            get { return personId; }
            set { personId = value; OnPropertyChanged("PersonId"); }
        }

        public DeleteItemViewModel()
        {
            storeService = new StoreService();
            customerService = new CustomerService();
            personService = new PersonService();
            CloseCommand = new DelegateCommand(OnCloseCommand);
            DeleteCommand = new DelegateCommand(OnDeleteCommand);
        }

        private async void OnDeleteCommand(object obj)
        {
            if (StoreId != 0)
            {
                await storeService.DeleteStore(StoreId);
            }
            else if (CustomerId != 0)
            {
                await customerService.DeleteCustomer(CustomerId);
            }
            else
            {
                await personService.DeletePerson(PersonId);
            }
            OnRequestClose();
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
