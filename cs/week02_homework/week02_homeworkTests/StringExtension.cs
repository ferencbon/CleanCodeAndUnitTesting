using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week02_homeworkTests
{
    internal static class StringExtension
    {
        internal static string RemoveIllegalCharacters(this string input)
        {
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(invalidChar.ToString(), string.Empty);
            }
            return input;
        }
    }
}
