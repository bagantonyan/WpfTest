using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WpfTest.UI.Services;
using System.Windows.Input;
using WpfTest.UI.Commands;
using WpfTest.Models.Models;
using WpfTest.UI.Views.StoreViews;
using System.Net.Http;
using System.Windows;
using WpfTest.UI.Views.CommonViews;
using WpfTest.UI.ViewModels.CommonViewModels;

namespace WpfTest.UI.ViewModels.StoreViewModels
{
    public class StoresTabViewModel : INotifyPropertyChanged
    {
        private StoreService storeService;
        private List<Store> stores;
        private Store selectedStore;

        public ICommand AddStoreCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }
        public ICommand DeleteStoreCommand { get; set; }
        public ICommand EditStoreCommand { get; set; }
        //public ICommand OpenStoreCommands { get; set; }


        private bool isTabSelected = true;
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

        public StoresTabViewModel()
        {
            storeService = new StoreService();
            AddStoreCommand = new DelegateCommand(OnAddStoreCommand);
            EditStoreCommand = new DelegateCommand(OnEditStoreCommand, CanEditStore);
            RefreshDataCommand = new DelegateCommand(OnRefreshDataCommand);
            DeleteStoreCommand = new DelegateCommand(OnDeleteStoreCommand,CanDeleteStore);
           // OpenStoreCommands = new DelegateCommand(OnOpenStoreCommand);
            LoadData();
        }

        //private void OnOpenStoreCommand(object obj)
        //{

        //}

        private void OnRefreshDataCommand(object obj)
        {
            LoadData();
        }

        

        private bool CanEditStore(object obj)
        {
            return (SelectedStore != null);
        }

        private void OnEditStoreCommand(object obj)
        {
            var e = new EditStoreViewModel(SelectedStore);
            var editStoreDialog = new EditStoreView();
            e.RequestClose += (s, e) => editStoreDialog.Close();

            editStoreDialog.DataContext = e;
            editStoreDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            editStoreDialog.ShowDialog();
        }

        private bool CanDeleteStore(object obj)
        {
            return (SelectedStore != null);
        }

        private void OnAddStoreCommand(object obj)
        {
            var newStoreDialog = new AddStoreView();
            newStoreDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            newStoreDialog.ShowDialog();
        }

        private void OnDeleteStoreCommand(object obj)
        {
            var viewModel = new DeleteItemViewModel();
            viewModel.StoreId = SelectedStore.StoreId;
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
            Stores = await storeService.GetAllStores();
            IsBusy = false;
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
