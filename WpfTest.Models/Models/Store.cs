using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

namespace WpfTest.Models.Models
{
    public class Store : INotifyPropertyChanged, IDataErrorInfo
    {
        private string name;
        private string address;
        private DateTime modifiedDate;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool HasError
        {
            get
            {
                return this.Error != null;
            }
        }

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;

                switch (columnName)
                {
                    case "Name":
                        {
                            if (string.IsNullOrEmpty(Name))
                            {
                                result = "Name cannot be empty";
                            }
                            break;
                        }

                    case "Address":
                        {
                            if (string.IsNullOrEmpty(Address))
                            {
                                result = "Address cannot be empty";
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

        public long StoreId { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }

        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; OnPropertyChanged("ModifiedDate"); }
        }

        private bool validateDataErrors = false;

        public bool ValidateDataErrors
        {
            get { return validateDataErrors; }
            set { validateDataErrors = value; OnPropertyChanged("ValidateDataErrors"); }
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
