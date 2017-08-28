using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Model.Classes
{
    public static class SnilsValidator
    {
        public static bool Validate(string snils)
        {
            Regex digits = new Regex(@"[^\d]");
            snils = digits.Replace(snils, "");

            if (snils.Length != 11)
                return false;

            int index = 10;
            int sum = 0;
            int control = Convert.ToByte(snils.Substring(9, 2));
            snils = snils.Substring(0, 9);

            digits = new Regex("(.)\\1\\1"); //3 подряд одинаковых символа

            if (digits.IsMatch(snils))
                return false;

            foreach (var d in snils)
            {
                index--;
                sum += Int16.Parse(d.ToString()) * index;
            }

            if (sum > 101)
            {
                sum = sum % 101;
            }

            if ((sum == 100 || sum == 101) && (control == 0))
                return true;
            else if ((sum < 100) && (sum == control))
                return true;
            else
                return false;
        }
    }
}
