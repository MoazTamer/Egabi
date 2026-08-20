using FluentValidation;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Application.Services;

namespace StudentManagementSystem.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddValidatorsFromAssemblyContaining<IStudentService>();

            return services;
        }
    }
}
