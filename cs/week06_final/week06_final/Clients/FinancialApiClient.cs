using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Clients;
using week06_final.Exceptions;
using week06_final.Models;
using week06_final.Models.Person;
using week06_final.Abstraction.Wrapper;

namespace week06_final.Clients
{
    public class FinancialApiClient:IFinancialApiClient
    {
        private readonly ILoggerWrapper<FinancialApiClient> _logger;
        public FinancialApiClient(ILoggerWrapper<FinancialApiClient> _logger)
        {
            this._logger = _logger;
        }

        public async Task<bool> GetPaymentStatus(Student student, string courseName)
        {
            try
            {
                CheckStudentAndCourseParameter(student, courseName);

                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw new FinancialApiException("Error in GetPaymentStatusAsync. See inner exception for details", ex);
            }
        }

        public async Task<bool> CreatePayment(Student student, string courseName)
        {
            try
            {
                CheckStudentAndCourseParameter(student, courseName);
                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw new FinancialApiException("Error in CreatePaymentAsync. See inner exception for details", ex);
            }

        }

        private void CheckStudentAndCourseParameter(Student student, string courseName)
        {
            ArgumentNullException.ThrowIfNull(student);
            ArgumentException.ThrowIfNullOrWhiteSpace(courseName);
        }
    }
}
