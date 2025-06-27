using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Customer : INotifyPropertyChanged
    {
        private string _customerID;
        private string _companyName;
        private string _contactName;
        private string _contactTitle;
        private string _address;
        private string _phone;
        private string _password;

        public string CustomerID
        {
            get => _customerID;
            set => SetProperty(ref _customerID, value);
        }

        public string CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }

        public string ContactName
        {
            get => _contactName;
            set => SetProperty(ref _contactName, value);
        }

        public string ContactTitle
        {
            get => _contactTitle;
            set => SetProperty(ref _contactTitle, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
