using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week04_homework
{
    public class UserDataProcessor
    {
        public string ProcessUserData( bool isSearchEnabled, string[] userNames, bool isProcessEnabled, string targetUserName, int processCount)        {
            if (isSearchEnabled && isProcessEnabled)
                return SearchUser(userNames, targetUserName); 
           
            if (!isSearchEnabled && isProcessEnabled)
                return Processing(processCount);
         
            return "No action taken.";

        }
        private string SearchUser(string[] userNames, string targetUserName)
        {
            int targetUserIndex = Array.IndexOf(userNames, targetUserName);
            if (targetUserIndex>-1)
                return $"User found: {targetUserName} at index {targetUserIndex}";
          
            return "User not found.";
        }

        private string Processing(int processCount)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < processCount; i++)
            {
                result.Append("Processing... ");
            }
            return result.ToString();
        }
    }
}
