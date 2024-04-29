using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace week03_homework.Libraries
{
    public interface IFileStorageLibrary
    {
        Task SaveContentIntoFile(string outputPath, string processedImage);
    }
}
