using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfTest.Models.Models;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;

namespace WpfTest.UI.ViewModels.PersonViewModels
{
    public class EditPersonViewModel : INotifyPropertyChanged
    {
        private PersonService personService;
        private Person _editPerson;
        public event EventHandler RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand EditCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public Person EditPerson
        {
            get { return _editPerson; }
            set { _editPerson = value; OnPropertyChanged("EditPerson"); }
        }

        public EditPersonViewModel()
        {
            personService = new PersonService();
            EditCommand = new DelegateCommand(OnEditCommand, CanEdit);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        public EditPersonViewModel(Person person) : this()
        {
            EditPerson = person;
        }

        private bool CanEdit(object arg)
        {
            if (string.IsNullOrEmpty(EditPerson.FirstName) || string.IsNullOrEmpty(EditPerson.LastName))
            {
                return false;
            }
            return true;
        }

        private async void OnEditCommand(object obj)
        {
            var editedPerson = new Person
            {
                PersonId = EditPerson.PersonId,
                FirstName = EditPerson.FirstName,
                LastName = EditPerson.LastName
            };

            string json = JsonConvert.SerializeObject(editedPerson);
            await personService.UpdatePerson(json);
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
