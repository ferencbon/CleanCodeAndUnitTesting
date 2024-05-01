using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Repository;
using week06_final.Abstraction.Services;
using week06_final.Abstraction.Wrapper;
using week06_final.Clients;
using week06_final.Models;
using week06_final.Models.Person;
using week06_final.Repository;
using week06_final.Services;
using week06_final.Wrapper;

namespace week06_final
{
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    //ONLY FAST TESTING
    public class Program
    {
        static void Main(string[] args)
        {
            var hostBuilderBuild = CreateHostBuilder(args).Build();
            hostBuilderBuild.Run();
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
              
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IDbClient, DbClient>()
                        .AddSingleton<IFinancialApiClient, FinancialApiClient>()
                        .AddSingleton<INotificationService, NotificationService>()
                        .AddSingleton<INotificationClient, EmailNotificationClient>()
                        .AddSingleton<INotificationClient, PushNotificationClient>()
                        .AddSingleton<ICourseRepository, CourseRepository>()
                        .AddSingleton<ICourseService, CourseService>()
                        .AddSingleton<IPaymentService, PaymentService>()
                        .AddScoped(typeof(ILoggerWrapper<>), typeof(LoggerWrapper<>))
                        .AddLogging(loggingBuilder =>
                        {
                            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                        })
                        .AddHostedService<TestWorker>();
                });
        }
    }
 

    public class TestWorker : IHostedService
    {
        private readonly ICourseService _courseService;

        public TestWorker(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _courseService.AddCourseAsync(new Course("realCourse", DateTime.Now.AddDays(-15), 30, 2, 99));
            await _courseService.AddCourseAsync(new Course("realCourse2", DateTime.Now.AddDays(-5), 30, 2, 99));
            await _courseService.AddCourseAsync(new Course("realCourse3", DateTime.Now.AddDays(5), 30, 2, 99));
            var result=await _courseService.GetCoursesAsync();
            foreach (var course in result)
            {
                try
                {
                    var courseStatistic = await _courseService.GetCourseStatisticsAsync(course.CourseName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var course in result)
            {
                try
                {
                    await _courseService.AddStudentToCourseAsync(new Student("test", "Bela", "emai@&email.hu"), course.CourseName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            var result2 = await _courseService.GetCoursesAsync();

            foreach (var course in result2)
            {
                try
                {
                    
                    Newtonsoft.Json.JsonConvert.SerializeObject(course);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
