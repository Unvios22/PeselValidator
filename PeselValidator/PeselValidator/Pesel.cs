using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeselValidator
{
    class Pesel
    {
        private string peselString;
        private bool isValid;
        private DateTime birthDate;
        private char sex;
        private byte reasonInvalid;

        public Pesel(string input) { 
            PeselString = input;
        }

        public string PeselString {
            get => peselString;
            set => peselString = value;
        }
        public bool IsValid {
            get => isValid;
            set => isValid = value;
        }
        public DateTime BirthDate {
            get => birthDate;
            set => birthDate = value;
        }
        public char Sex {
            get => sex;
            set => sex = value;
        }
        public byte ReasonInvalid {
            get => reasonInvalid;
            set => reasonInvalid = value;
        }
    }
}
