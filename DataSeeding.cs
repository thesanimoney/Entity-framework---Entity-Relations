// P02_StudentSystem.Data.Seeding

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
                    // Database has been seeded
                    return;
                }

                // Seed your data here

                context.SaveChanges();
            }
        }
    }
}
