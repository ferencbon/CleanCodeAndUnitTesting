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

        public async Task<bool> GetPaymentStatus(Student student, Course course)
        {
            try
            {
                CheckStudentAndCourseParameter(student, course);

                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw new FinancialApiException("Error in GetPaymentStatus. See inner exception for details", ex);
            }
        }

        public async Task<bool> CreatePayment(Student student, Course course)
        {
            try
            {
                CheckStudentAndCourseParameter(student, course);
                await Task.Delay(1000);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw new FinancialApiException("Error in CreatePayment. See inner exception for details", ex);
            }

        }

        private void CheckStudentAndCourseParameter(Student student, Course course)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (course == null)
                throw new ArgumentNullException(nameof(course));
        }
    }
}
