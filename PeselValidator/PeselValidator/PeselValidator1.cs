using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeselValidator {

    public class PeselValidator {

        private Pesel pesel;
        private int[] peselArray;
        private string reasonInvalid;
        private int dayOfBirth;
        private int monthOfBirth;
        private int yearOfBirth;

        private byte[] reasonInvalidTab = { 1, 2, 3, 4 };


        public Pesel validatePesel(Pesel inputPesel) {
            pesel = inputPesel;
            pesel.IsValid = false;
            if (!validateInputFormat()) { return pesel; }
            parsePeselToIntArray(pesel.PeselString);
            if (!validateControlSum()) { return pesel; }
            extractBirthdateData();
            if (!validateBirthdateData) { return pesel; }
            insertBirthdateIntoPeselObj();
            extractSex();
            return pesel;
        }

        private bool validateInputFormat(Pesel input) {
            if (!validateInputLength(input)) {
                pesel.ReasonInvalid = reasonInvalidTab[0];
                return false;
            } else if (!validateInputNumbersOnly(input)) {
                pesel.ReasonInvalid = reasonInvalidTab[1];
                return false;
            }
            return true;
        }

        private bool validateInputLength(string input) {
            return (input.Length = 11);
        }

        private bool validateInputNumbersOnly(string input) {

            foreach (char ch in input) {
                if (!Char.IsNumber(ch)) { return false; }
            }
            return true;
        }

        private bool validateControlSum() {

            int controlSum = peselArray[0] + 3 * peselArray[1] + 7 * peselArray[2]
                        + 9 * peselArray[3] + peselArray[4] + 3 * peselArray[5]
                        + 7 * peselArray[6] + 9 * peselArray[7] + peselArray[8]
                        + 3 * peselArray[9] + peselArray[10];

            int checkControlSum = (10 - (controlSum % 10)) % 10;
            if (!checkControlSum == 0) {
                pesel.ReasonInvalid = reasonInvalidTab[2];
                return false;
            }
            return true;
        }

        private void extractBirthdateData() {
            extractDayOfBirth();
            int tempMonthValue = extractTempMonthOfBirth(); //extracting two numbers, with which the century of birth and actual month of birth are coded
            extractYearOfBirth(tempMonthValue);
            extractMonthOfBirth(tempMonthValue);
        }

        private void extractDayOfBirth() {
            dayOfBirth = 10 * peselArray[4] + peselArray[5];
        }

        private int extractTempMonthOfBirth() {
            return (10 * peselArray[2] + peselArray[3]);
        }

        private void extractYearOfBirth(int monthValue) {
            yearOfBirth = 10 * peselArray[0] + peselArray[1];

            if (monthValue > 80 && monthValue < 93) {
                year += 1800;

            } else if (monthValue > 0 && monthValue < 13) {
                year += 1900;

            } else if (monthValue > 20 && monthValue < 33) {
                year += 2000;

            } else if (monthValue > 40 && monthValue < 53) {
                year += 2100;

            } else if (monthValue > 60 && monthValue < 73) {
                year += 2200;
            }
        }

        private extractMonthOfBirth(int monthValue) {
            if (monthValue > 80 && monthValue < 93) {
                monthOfBirth -= 80;

            } else if (monthValue > 20 && monthValue < 33) {
                monthOfBirth -= 20;

            } else if (monthValue > 40 && monthValue < 53) {
                monthOfBirth -= 40;

            } else if (monthValue > 60 && monthValue < 73) {
                monthOfBirth -= 60;
            }
        }

        private validateBirthdateData() {
            if (!validateYearOfBirth) { return false; } else if (!validateMonthOfBirth) { return false; } else if (!validateDayOfBirth) { return false; }
            return true;
        }

        private bool validateYearOfBirth() {
            if (!(yearOfBirth >= 1800 && yearOfBirth <= 2299)) { //checking the year range for which the PESEL number was designed
                pesel.ReasonInvalid = reasonInvalidTab[3];
                return false;
            }
            return true;
        }

        private bool validateMonthOfBirth() {
            if (!(monthOfBirth >= 1 && monthOfBirth <= 12)) { //checking whether the extracted month makes sense
                pesel.ReasonInvalid = reasonInvalidTab[3];
                return false;
            }
            return true;
        }

        private bool validateDayOfBirth() {
            int[] daysInEachMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (isLeapYear(yearOfBirth)) { daysInEachMonth[1]++; }
            if (!(dayOfBirth > 0 && dayOfBirth <= daysInEachMonth[monthOfBirth - 1])) {  //checking whether the day of birth makes sense in relation to month of birth
                pesel.ReasonInvalid = reasonInvalidTab[3];
                return false;
            }
            return true;
        }

        private void insertBirthdateIntoPeselObj() {
            DateTime temp = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);
            pesel.BirthDate = temp;
        }

        private void extractSex() {
            if (peselArray[9] % 2 == 0) {
                pesel.Sex = 'F';
            } else { pesel.Sex = 'M'; }
        }

        private boolean isLeapYear(int year) {
            if ((year % 4 == 0) && year % 100 != 0) {
                return true;
            } else if (year % 400 == 0) {
                return true;
            } else return false;
        }


        private bool parsePeselToIntArray(string pesel) {

            for (int i; i < pesel.Length - 1; i++) {
                peselArray[i] = Int32.Parse(pesel[i]);
            }

        }

    }
}
