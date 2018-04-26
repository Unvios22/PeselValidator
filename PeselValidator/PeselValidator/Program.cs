using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeselValidator {
    class Program {
        static void Main(string[] args) {
            PeselValidate validator = new PeselValidate();

            List<Pesel> list = new List<Pesel> {
                new Pesel("ABCABCABCAB"),   //has chars
                new Pesel("9673727"),       //improper length
                new Pesel("44051401458"),   // valid
                new Pesel("98375669371")    // wrong control number
            };

            foreach (Pesel pesel in list) {
                validator.ValidatePesel(pesel);
                Console.WriteLine("Valid:" + pesel.IsValid);
                Console.WriteLine("Pesel Number:" + pesel.PeselString);
                Console.WriteLine("Reason Invalid:" + pesel.ReasonInvalid);
                Console.WriteLine("Sex:" + pesel.Sex);
                Console.WriteLine("Birthdate:" + pesel.BirthDate);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}

