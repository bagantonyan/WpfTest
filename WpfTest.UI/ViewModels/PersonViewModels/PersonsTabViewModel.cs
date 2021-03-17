using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfTest.Models.Models;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;
using WpfTest.UI.ViewModels.CommonViewModels;
using WpfTest.UI.Views.CommonViews;
using WpfTest.UI.Views.PersonViews;

namespace WpfTest.UI.ViewModels.PersonViewModels
{
    public class PersonsTabViewModel : INotifyPropertyChanged
    {
        private bool isTabSelected;
        private PersonService personService;
        private List<Person> persons;
        private Person selectedPerson;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddPersonCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }

        public ICommand DeletePersonCommand { get; set; }
        public ICommand EditPersonCommand { get; set; }

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
        public List<Person> Persons
        {
            get { return persons; }
            set { persons = value; OnPropertyChanged("Persons"); }
        }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value; OnPropertyChanged("SelectedPerson"); }
        }

        public PersonsTabViewModel()
        {
            personService = new PersonService();
            AddPersonCommand = new DelegateCommand(OnAddPersonCommand);
            RefreshDataCommand = new DelegateCommand(OnRefreshDataCommand);

            EditPersonCommand = new DelegateCommand(OnEditPersonCommand, CanEditPerson);
            DeletePersonCommand = new DelegateCommand(OnDeletePersonCommand, CanDeletePerson);
        }

        private void OnRefreshDataCommand(object obj)
        {
            LoadData();
        }

        private void OnAddPersonCommand(object obj)
        {
            var newPersonDialog = new AddPersonView();
            newPersonDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            var dialogResult = newPersonDialog.ShowDialog();
        }

        private bool CanEditPerson(object obj)
        {
            return (SelectedPerson != null);
        }

        private void OnEditPersonCommand(object obj)
        {
            var e = new EditPersonViewModel(SelectedPerson);
            var editPersonDialog = new EditPersonView();
            e.RequestClose += (s, e) => editPersonDialog.Close();

            editPersonDialog.DataContext = e;
            editPersonDialog.Closed += (s, e) =>
            {
                LoadData();
            };
            editPersonDialog.ShowDialog();
        }

        private bool CanDeletePerson(object obj)
        {
            return (SelectedPerson != null);
        }

        private void OnDeletePersonCommand(object obj)
        {
            var viewModel = new DeleteItemViewModel();
            viewModel.PersonId = SelectedPerson.PersonId;
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
            Persons = await personService.GetAllPersons();
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
