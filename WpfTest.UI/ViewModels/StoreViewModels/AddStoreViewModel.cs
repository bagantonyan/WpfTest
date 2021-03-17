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

namespace WpfTest.UI.ViewModels.StoreViewModels
{
    public class AddStoreViewModel : INotifyPropertyChanged
    {
        private StoreService storeService;
        public event EventHandler RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private Store _addStore;
        public Store AddStore
        {
            get { return _addStore; }
            set { _addStore = value; }
        }

        public AddStoreViewModel()
        {
            AddStore = new Store();
            storeService = new StoreService();
            AddCommand = new DelegateCommand(OnAddCommand, CanAdd);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        private bool CanAdd(object arg)
        {
            if (string.IsNullOrEmpty(AddStore.Name) || string.IsNullOrEmpty(AddStore.Address))
            {
                return false;
            }
            return true;
        }

        private async void OnAddCommand(object obj)
        {

            var newStore = new Store
            {
                Name = AddStore.Name,
                Address = AddStore.Address,
                ModifiedDate = DateTime.Now,
            };

            string json = JsonConvert.SerializeObject(newStore);
            await storeService.AddStore(json);
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
