using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfTest.Models.Models;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;

namespace WpfTest.UI.ViewModels.StoreViewModels
{
    public class EditStoreViewModel : INotifyPropertyChanged
    {
        private StoreService storeService;
        public event EventHandler RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;
        private Store _editStore;

        public ICommand EditCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public Store EditStore
        {
            get { return _editStore; }
            set { _editStore = value; OnPropertyChanged("Store"); }
        }

        public EditStoreViewModel()
        {
            storeService = new StoreService();
            EditCommand = new DelegateCommand(OnEditCommand, CanEdit);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        public EditStoreViewModel(Store store) : this()
        {
            EditStore = store;
        }

        private bool CanEdit(object arg)
        {
            if (string.IsNullOrEmpty(EditStore.Name) || string.IsNullOrEmpty(EditStore.Address))
            {
                return false;
            }
            return true;
        }

        private async void OnEditCommand(object obj)
        {
            var editedStore = new Store
            {
                StoreId = EditStore.StoreId,
                Name = EditStore.Name,
                Address = EditStore.Address
            };

            string json = JsonConvert.SerializeObject(editedStore);
            await storeService.UpdateStore(json);
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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
