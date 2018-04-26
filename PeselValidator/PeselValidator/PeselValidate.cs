using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeselValidator {
    
    class PeselValidate {

        private Pesel pesel;
        private int[] peselArray = new int[11];
        private string reasonInvalid;
        private int dayOfBirth;
        private int monthOfBirth;
        private int yearOfBirth;

        private byte[] reasonInvalidTab = { 1, 2, 3, 4 };


        public Pesel ValidatePesel(Pesel inputPesel) {
            pesel = inputPesel;
            pesel.IsValid = false;
            if (!ValidateInputFormat(pesel.PeselString)) { return pesel; }
            ParsePeselToIntArray(pesel.PeselString);
            if (!ValidateControlSum()) { return pesel; }
            ExtractBirthdateData();
            if (!ValidateBirthdateData()) { return pesel; }
            InsertBirthdateIntoPeselObj();
            ExtractSex();
            pesel.IsValid = true;
            return pesel;
        }

        private bool ValidateInputFormat(String input) {
            if (!ValidateInputLength(input)) {
                pesel.ReasonInvalid = reasonInvalidTab[0];
                return false;
            } else if (!ValidateInputNumbersOnly(input)) {
                pesel.ReasonInvalid = reasonInvalidTab[1];
                return false;
            }
            return true;
        }

        private bool ValidateInputLength(string input) {
            return (input.Length == 11);
        }

        private bool ValidateInputNumbersOnly(string input) {

            foreach (char ch in input) {
                if (!Char.IsNumber(ch)) { return false; }
            }
            return true;
        }

        private bool ValidateControlSum() {

            int controlSum = peselArray[0] + 3 * peselArray[1] + 7 * peselArray[2]
                        + 9 * peselArray[3] + peselArray[4] + 3 * peselArray[5]
                        + 7 * peselArray[6] + 9 * peselArray[7] + peselArray[8]
                        + 3 * peselArray[9] + peselArray[10];

            int checkControlSum = controlSum % 10;
            if (!(checkControlSum == 0)) {
                pesel.ReasonInvalid = reasonInvalidTab[2];
                return false;
            }
            return true;
        }

        private void ExtractBirthdateData() {
            ExtractDayOfBirth();
            int tempMonthValue = ExtractTempMonthOfBirth(); //extracting two numbers with which the century of birth and actual month of birth are coded
            ExtractYearOfBirth(tempMonthValue);
            ExtractMonthOfBirth(tempMonthValue);
        }

        private void ExtractDayOfBirth() {
            dayOfBirth = 10 * peselArray[4] + peselArray[5];
        }

        private int ExtractTempMonthOfBirth() {
            return (10 * peselArray[2] + peselArray[3]);
        }

        private void ExtractYearOfBirth(int monthValue) {
            yearOfBirth = 10 * peselArray[0] + peselArray[1];

            if (monthValue > 80 && monthValue < 93) {
                yearOfBirth += 1800;

            } else if (monthValue > 0 && monthValue < 13) {
                yearOfBirth += 1900;

            } else if (monthValue > 20 && monthValue < 33) {
                yearOfBirth += 2000;

            } else if (monthValue > 40 && monthValue < 53) {
                yearOfBirth += 2100;

            } else if (monthValue > 60 && monthValue < 73) {
                yearOfBirth += 2200;
            }
        }

        private void ExtractMonthOfBirth(int monthValue) {
            if (monthValue > 80 && monthValue < 93) {
                monthOfBirth = monthValue- 80;

            }else if (monthValue > 0 && monthValue< 13) {
                monthOfBirth = monthValue - 0;

            }else if (monthValue > 20 && monthValue < 33) {
                monthOfBirth = monthValue - 20;

            } else if (monthValue > 40 && monthValue < 53) {
                monthOfBirth = monthValue - 40;

            } else if (monthValue > 60 && monthValue < 73) {
                monthOfBirth = monthValue - 60;
            }
        }

        private bool  ValidateBirthdateData() {
            if (!ValidateYearOfBirth()) { return false; }
            else if (!ValidateMonthOfBirth()) { return false; }
            else if (!ValidateDayOfBirth()) { return false; }
            return true;
        }

        private bool ValidateYearOfBirth() {
            if (!(yearOfBirth >= 1800 && yearOfBirth <= 2299)) { //checking the year range for which the PESEL number was designed
                pesel.ReasonInvalid = reasonInvalidTab[3];
                return false;
            }
            return true;
        }

        private bool ValidateMonthOfBirth() {
            if (!(monthOfBirth >= 1 && monthOfBirth <= 12)) { //checking whether the extracted month makes sense
                pesel.ReasonInvalid = reasonInvalidTab[3];
                return false;
            }
            return true;
        }

        private bool ValidateDayOfBirth() {
            int[] daysInEachMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (IsLeapYear(yearOfBirth)) { daysInEachMonth[1]++; }
            if (!(dayOfBirth > 0 && dayOfBirth <= daysInEachMonth[monthOfBirth - 1])) {  //checking whether the day of birth makes sense in relation to month of birth
                pesel.ReasonInvalid = reasonInvalidTab[3];
                return false;
            }
            return true;
        }

        private void InsertBirthdateIntoPeselObj() {
            DateTime temp = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);
            pesel.BirthDate = temp;
        }

        private void ExtractSex() {
            if (peselArray[9] % 2 == 0) {
                pesel.Sex = 'F';
            } else { pesel.Sex = 'M'; }
        }

        private bool IsLeapYear(int year) {
            if ((year % 4 == 0) && year % 100 != 0) {
                return true;
            } else if (year % 400 == 0) {
                return true;
            } else return false;
        }

        private void ParsePeselToIntArray(string pesel) {

            for (int i = 0 ; i < pesel.Length; i++) {
                peselArray[i] = Int32.Parse(pesel[i].ToString());
            }

        }

    }
}