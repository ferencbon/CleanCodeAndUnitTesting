using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week03_homework.Libraries
{
    public interface IImageProcessingLibrary
    {
        Task<string> ProcessImage(string inputPath);
    }
}
