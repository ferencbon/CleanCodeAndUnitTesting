using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week04_homework
{
    public class UserValidator
    {
        public bool ValidateUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            input = input.Trim();

            return IsValidLength(input) && IsValidContent(input);
            
        }

        private bool IsValidLength(string input)
        {
            return input.Length >= 5 && input.Length <= 20;
        }
        private bool IsValidContent(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
        }
    }
}
