using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfTest.Models.Models;
using WpfTest.UI.Commands;
using WpfTest.UI.Services;

namespace WpfTest.UI.ViewModels.PersonViewModels
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        private PersonService personService;
        public event EventHandler RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        protected void OnRequestClose()
        {
            if (RequestClose != null)
                RequestClose(this, EventArgs.Empty);
        }

        private Person _addPerson;

        public Person AddPerson
        {
            get { return _addPerson; }
            set { _addPerson = value; }
        }


        public AddPersonViewModel()
        {
            AddPerson = new Person();
            personService = new PersonService();
            AddCommand = new DelegateCommand(OnAddCommand, CanAdd);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        private bool CanAdd(object arg)
        {
            if (string.IsNullOrEmpty(AddPerson.FirstName) || string.IsNullOrEmpty(AddPerson.LastName))
            {
                return false;
            }
            return true;
        }

        private void OnAddCommand(object obj)
        {
            var newPerson = new Person
            {
                FirstName = AddPerson.FirstName,
                LastName = AddPerson.LastName,
                ModifiedDate = DateTime.Now,
            };

            string json = JsonConvert.SerializeObject(newPerson);
            personService.AddPerson(json);
            OnRequestClose();
        }

        private void OnCloseCommand(object obj)
        {
            OnRequestClose();
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
