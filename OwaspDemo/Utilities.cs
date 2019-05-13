using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OwaspDemo
{
    public static class Utilities
    {
        public static bool IsValidEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress)) { return false; }

            emailAddress = emailAddress.Trim(' ');

            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }

            string regex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,8})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            return Regex.IsMatch(emailAddress, regex);
        }

        public static bool IsValidText(string text)
        {
            string regex = @"[a-zA-Z ]+$";
            return Regex.IsMatch(text, regex);
        }
    }
}
