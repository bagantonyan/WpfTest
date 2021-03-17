using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WpfTest.Models.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string storeName;
        private List<Store> stores;
        private Store currentStore;
        private List<Person> persons;
        private Person currentPerson;
        private DateTime modifiedDate;
        public event PropertyChangedEventHandler PropertyChanged;

        public long CustomerId { get; set; }
        public long PersonId { get; set; }
        public long StoreId { get; set; }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("Lastname"); }
        }

        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; OnPropertyChanged("StoreName"); }
        }

        public List<Store> Stores
        {
            get { return stores; }
            set { stores = value; OnPropertyChanged("Stores"); }
        }

        public Store CurrentStore
        {
            get { return currentStore; }
            set { currentStore = value; OnPropertyChanged("CurrentStore"); }
        }

        public List<Person> Persons
        {
            get { return persons; }
            set { persons = value; OnPropertyChanged("Persons"); }
        }

        public Person CurrentPerson
        {
            get { return currentPerson; }
            set { currentPerson = value; OnPropertyChanged("CurrentPerson"); }
        }

        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; OnPropertyChanged("ModifiedDate"); }
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
