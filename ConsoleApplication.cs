using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentSystem.Data.Models;
using System;
using System.Linq;

namespace StudentSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<StudentSystemContext>();
                    context.Database.Migrate();

                    DatabaseSeeder.Seed(services);

                   
                    DisplayStudentAndCourseInfo(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while seeding the database.");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void DisplayStudentAndCourseInfo(StudentSystemContext context)
        {
            Console.WriteLine("Student and Course Information:");

            // Display information about students
            var students = context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"Student ID: {student.StudentId}, Name: {student.Name}");
                Console.WriteLine($"Enrolled Courses:");
                foreach (var courseEnrollment in student.CoursesEnrolled)
                {
                    Console.WriteLine($"  - {courseEnrollment.Course.Name}");
                }
                Console.WriteLine();
            }

            // Display information about courses
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"Course ID: {course.CourseId}, Name: {course.Name}");
                Console.WriteLine($"Enrolled Students:");
                foreach (var studentEnrollment in course.StudentsEnrolled)
                {
                    Console.WriteLine($"  - {studentEnrollment.Student.Name}");
                }
                Console.WriteLine();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<StudentSystemContext>(options =>
                        options.UseSqlServer(
                            hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    // Add other services if needed...

                });
    }
}
