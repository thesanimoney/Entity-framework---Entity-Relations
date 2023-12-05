using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace StudentSystem.Data.Seeding
{
    public static class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new StudentSystemContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentSystemContext>>()))
            {
                if (context.Students.Any() || context.Courses.Any())
                {
                    return;
                }

                SeedStudents(context);
                SeedCourses(context);

                context.SaveChanges();
            }
        }

        private static void SeedStudents(StudentSystemContext context)
        {
            var students = new[]
            {
                new Student { Name = "John Doe", PhoneNumber = "1234567890", RegisteredOn = DateTime.Now, Birthday = new DateTime(2000, 1, 1) },
                new Student { Name = "Jane Smith", PhoneNumber = "9876543210", RegisteredOn = DateTime.Now, Birthday = new DateTime(1998, 5, 10) },
                
            };

            context.Students.AddRange(students);
        }

        private static void SeedCourses(StudentSystemContext context)
        {
            var courses = new[]
            {
                new Course { Name = "Introduction to Programming", Description = "Learn the basics of programming", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), Price = 99.99m },
                new Course { Name = "Web Development Fundamentals", Description = "Foundational skills for web development", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(4), Price = 129.99m },
            };

            context.Courses.AddRange(courses);
        }

    }
}
