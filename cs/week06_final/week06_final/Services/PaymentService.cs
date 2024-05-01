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
        public async Task<bool> GetPaymentStatus(Student student, string courseName)
        {
            try
            {
                CheckStudentAndCourseParameter(student, courseName);

                var result = await _financialApiClient.GetPaymentStatus(student, courseName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FinancialApiException("Error in GetPaymentStatus. See inner exception for details", ex);
            }
        }

        public async Task<bool> CreatePayment(Student student, string courseName)
        {
            try
            {
                CheckStudentAndCourseParameter(student, courseName);

                var result = await _financialApiClient.CreatePayment(student, courseName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new FinancialApiException("Error in CreatePayment. See inner exception for details", ex);
            }

        }

        private void CheckStudentAndCourseParameter(Student student, string courseName)
        {
            ArgumentNullException.ThrowIfNull(student);
            ArgumentException.ThrowIfNullOrWhiteSpace(courseName);
        }
    }
}
