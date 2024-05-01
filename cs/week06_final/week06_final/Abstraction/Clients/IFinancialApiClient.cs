using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Models.Person;
using week06_final.Models;

namespace week06_final.Abstraction.Clients
{

    public interface IFinancialApiClient
    {
        public Task<bool> GetPaymentStatus(Student student, Course course);
        public Task<bool> CreatePayment(Student student, Course course);
    }
}
