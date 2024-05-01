using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Services;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Models;
using week06_final.Models.Person;

namespace week06_final.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly ILoggerWrapper<PaymentService> _logger;
        private readonly IFinancialApiClient _financialApiClient;

        public PaymentService(ILoggerWrapper<PaymentService> logger, IFinancialApiClient financialApiClient)
        {
            _logger = logger;
            _financialApiClient = financialApiClient;
        }
        public async Task<bool> GetPaymentStatus(Student student, Course course)
        {
            try
            {
                CheckStudentAndCourseParameter(student, course);

                var result = await _financialApiClient.GetPaymentStatus(student, course);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FinancialApiException("Error in GetPaymentStatus. See inner exception for details", ex);
            }
        }

        public async Task<bool> CreatePayment(Student student, Course course)
        {
            try
            {
                CheckStudentAndCourseParameter(student, course);

                var result = await _financialApiClient.CreatePayment(student, course);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
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
