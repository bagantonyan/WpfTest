using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfTest.Models.Models
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        public long PersonId { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;

                switch (columnName)
                {
                    case "FirstName":
                        {
                            if (string.IsNullOrEmpty(FirstName))
                            {
                                result = "Firstname cannot be empty";
                            }
                            break;
                        }
                    case "LastName":
                        {
                            if (string.IsNullOrEmpty(LastName))
                            {
                                result = "Lastname cannot be empty";
                            }
                            break;
                        }
                }

                if (ErrorCollection.ContainsKey(columnName))
                {
                    ErrorCollection[columnName] = result;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(columnName, result);
                }

                OnPropertyChanged("ErrorCollection");

                return result;
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }

        private DateTime modifiedDate;
        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; OnPropertyChanged("ModifiedDate"); }
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
