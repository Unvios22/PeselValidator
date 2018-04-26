using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeselValidator {
    class ReasonInvalidMessage {
        public static readonly String[] ReasonInvalidMessageTab = {  //indexes correspond with value of "reasonInvalid" field in Pesel Class
        "Pesel is valid",
        "Pesel length is different than 11",
        "Pesel doesn't have only numbers",
        "Pesel control number is incorrect",
        "Date of birth embedded in pesel is incorrect" };
    }

}

