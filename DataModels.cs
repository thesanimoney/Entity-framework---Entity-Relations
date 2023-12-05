// P01_StudentSystem.Data.Models

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Data.Models
{
	public class Student
	{
		public int StudentId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(10)]
		public string PhoneNumber { get; set; }

		public DateTime RegisteredOn { get; set; }

		public DateTime? Birthday { get; set; }

		public ICollection<StudentCourse> CoursesEnrolled { get; set; } = new List<StudentCourse>();
		public ICollection<Homework> HomeworkSubmissions { get; set; } = new List<Homework>();
	}

	public class Course
	{
		public int CourseId { get; set; }

		[Required]
		[StringLength(80)]
		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public decimal Price { get; set; }

		public ICollection<StudentCourse> StudentsEnrolled { get; set; } = new List<StudentCourse>();
		public ICollection<Resource> Resources { get; set; } = new List<Resource>();
		public ICollection<Homework> HomeworkSubmissions { get; set; } = new List<Homework>();
	}

	public class Resource
	{
		public int ResourceId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public string Url { get; set; }

		public ResourceType ResourceType { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }
	}

	public enum ResourceType
	{
		Video,
		Presentation,
		Document,
		Other
	}

	public class Homework
	{
		public int HomeworkId { get; set; }

		public string Content { get; set; }

		public ContentType ContentType { get; set; }

		public DateTime SubmissionTime { get; set; }

		public int StudentId { get; set; }
		public Student Student { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }
	}

	public enum ContentType
	{
		Application,
		Pdf,
		Zip
	}

	public class StudentCourse
	{
		public int StudentId { get; set; }
		public Student Student { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }
	}

	public class StudentSystemContext : DbContext
	{
		public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
			: base(options)
		{
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Resource> Resources { get; set; }
		public DbSet<Homework> HomeworkSubmissions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StudentCourse>()
				.HasKey(sc => new { sc.StudentId, sc.CourseId });

			modelBuilder.Entity<StudentCourse>()
				.HasOne(sc => sc.Student)
				.WithMany(s => s.CoursesEnrolled)
				.HasForeignKey(sc => sc.StudentId);

			modelBuilder.Entity<StudentCourse>()
				.HasOne(sc => sc.Course)
				.WithMany(c => c.StudentsEnrolled)
				.HasForeignKey(sc => sc.CourseId);
		}
	}
}
