using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Models;
using week06_final.Models.Person;

namespace week06_final.Abstraction.Services
{
    public interface IPaymentService
    {
        public Task<bool> GetPaymentStatus(Student student, string course);
        public Task<bool> CreatePayment(Student student, string course);
    }
}
