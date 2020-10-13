using DataAPI.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestData.Model
{
    public class DataModel : ObservableObject
    {
        private Guid id;
        private string name = String.Empty;
        private string surname = String.Empty;
        private string patronymic = String.Empty;
        private string email = String.Empty;

        public DataModel() { }

        public DataModel(DataRecord record)
        {
            if (record == null)
                return;
            id = record.id;
            Name = record.name;
            Surname = record.surname;
            Patronymic = record.patronymic;
            Email = record.email;
        }

        public Guid Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                Set<string>(() => this.Name, ref name, value);
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                Set<string>(() => this.Surname, ref surname, value);
            }
        }

        public string Patronymic
        {
            get
            {
                return patronymic;
            }
            set
            {
                Set<string>(() => this.Patronymic, ref patronymic, value);
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                Set<string>(() => this.Email, ref email, value);
            }
        }

        public bool EmptyData()
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Surname) && !String.IsNullOrEmpty(Patronymic) && !String.IsNullOrEmpty(Email))
                return false;
            return true;
        }
    }
}
