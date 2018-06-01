using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FilmWebshopUtilities
{
    public static class Validator
    {
        public static bool ValidEmail(string email)
        {
            string mailregex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (Regex.IsMatch(email, mailregex))
            {
                return true;
            }
            return false;
        }

        public static bool ValidPassword(string password)
        {
            string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,16}$";
            if (Regex.IsMatch(password, passregex))
            {
                return true;
            }
            return false;
        }
    }
}
