

using Entity03.Context;
using Entity03.Models;
using Entity03.Enums;
using Microsoft.EntityFrameworkCore;

namespace Entity03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CREATE
            AddInstructor();
            AddCourse();
            AddSeveralStudents();
            EnrollStudent();

            // READ
            FindCourse();
            FilterCourses();
            ShowCategories();
            ShowCourseRoster();

            // UPDATE
            ChangeCoursePrice();
            UpdateProgress();
            PublishCourse();

            // DELETE
            UnenrollStudent();
            AttemptDeleteCategoryWithSubCategories();
            SafeDeleteCategory();
            DeleteCourse();
        }

        // ==========================================
        // CREATE
        // ==========================================

        static void AddInstructor()
        {
            using var context = new LearningPlatformContext();

            string email = "mohamed111@example.com";

            var existingInstructor = context.Instructors
                .FirstOrDefault(i => i.Email == email);

            if (existingInstructor != null)
            {
                Console.WriteLine("Instructor already exists.");
                return;
            }

            var instructor = new Instructor
            {
                FullName = "Mohamed Hassan",
                Email = email,
                Bio = "Software Development Instructor",
                JoinedDate = DateTime.Now
            };

            context.Instructors.Add(instructor);
            context.SaveChanges();

            Console.WriteLine(
                $"Instructor added successfully. ID: {instructor.Id}");
        }

        static void AddCourse()
        {
            using var context = new LearningPlatformContext();

            int instructorId = 1;
            int categoryId = 2;

            var instructor = context.Instructors.Find(instructorId);

            if (instructor == null)
            {
                Console.WriteLine("Instructor not found. Course was not added.");
                return;
            }

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found. Course was not added.");
                return;
            }

            var course = new Course
            {
                Title = "ASP.NET Core Fundamentals",
                Description = "Learn ASP.NET Core from scratch",
                Price = 800,
                Level = CourseLevel.Beginner,
                IsPublished = false,
                PublishedDate = null,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            context.Courses.Add(course);
            context.SaveChanges();

            Console.WriteLine(
                $"Course added successfully. ID: {course.Id}");
        }

        static void AddSeveralStudents()
        {
            using var context = new LearningPlatformContext();

            var students = new List<Student>
            {
                new Student
                {
                    FullName = "Ali Hassan",
                    Email = "ali@example.com",
                    DateOfBirth = new DateTime(2002, 4, 10)
                },
                new Student
                {
                    FullName = "Mona Ahmed",
                    Email = "mona@example.com",
                    DateOfBirth = new DateTime(2001, 7, 15)
                },
                new Student
                {
                    FullName = "Khaled Mohamed",
                    Email = "khaled11@example.com",
                    DateOfBirth = new DateTime(2003, 2, 20)
                }
            };

            var studentsToAdd = students
                .Where(s => !context.Students.Any(x => x.Email == s.Email))
                .ToList();

            if (studentsToAdd.Count == 0)
            {
                Console.WriteLine("All students already exist.");
                return;
            }

            // One AddRange and one SaveChanges
            context.Students.AddRange(studentsToAdd);
            context.SaveChanges();

            foreach (var student in studentsToAdd)
            {
                Console.WriteLine(
                    $"Student added: {student.FullName}, ID: {student.Id}");
            }
        }

        static void EnrollStudent()
        {
            using var context = new LearningPlatformContext();

            int studentId = 3;
            int courseId = 1;

            var student = context.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found. Enrollment refused.");
                return;
            }

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found. Enrollment refused.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine("Course is not published. Enrollment refused.");
                return;
            }

            var existingEnrollment =
                context.Enrollments.Find(studentId, courseId);

            if (existingEnrollment != null)
            {
                Console.WriteLine(
                    "Student is already enrolled in this course.");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                ProgressPercentage = 0,
                Status = EnrollmentStatus.Active,
                FinalGrade = null
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            Console.WriteLine("Student enrolled successfully.");
        }

        // ==========================================
        // READ
        // ==========================================

        // 1. Find Course by ID and Title

        static void FindCourse()
        {
            using var context = new LearningPlatformContext();

            int courseId = 1;

            var courseById = context.Courses.Find(courseId);

            if (courseById == null)
            {
                Console.WriteLine("Course not found by ID.");
            }
            else
            {
                Console.WriteLine(
                    $"Found by ID: {courseById.Title}");
            }

            string courseTitle = "C# Fundamentals";

            var courseByTitle = context.Courses
                .FirstOrDefault(c => c.Title == courseTitle);

            if (courseByTitle == null)
            {
                Console.WriteLine("Course not found by Title.");
            }
            else
            {
                Console.WriteLine(
                    $"Found by Title: {courseByTitle.Title}");
            }
        }

        // 2. Filter Courses by Price

        static void FilterCourses()
        {
            using var context = new LearningPlatformContext();

            decimal minPrice = 400;
            decimal maxPrice = 900;

            var courses = context.Courses
                .Where(c => c.Price >= minPrice &&
                            c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(
                    $"Title: {course.Title}, " +
                    $"Level: {course.Level}, " +
                    $"Price: {course.Price}");
            }
        }

        // 3. Show Direct Subcategories and Top-Level Categories

        static void ShowCategories()
        {
            using var context = new LearningPlatformContext();

            int categoryId = 1;

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.WriteLine($"Subcategories of {category.Name}:");

            var subCategories = context.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToList();

            foreach (var subCategory in subCategories)
            {
                Console.WriteLine(subCategory.Name);
            }

            Console.WriteLine("Top-Level Categories:");

            var topLevelCategories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            foreach (var topCategory in topLevelCategories)
            {
                Console.WriteLine(topCategory.Name);
            }
        }

        // 4. Show Course Roster

        static void ShowCourseRoster()
        {
            using var context = new LearningPlatformContext();

            int courseId = 1;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            // First query: Get student IDs from Enrollments

            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine(
                $"Number of enrolled students: {studentIds.Count}");

            // Second query: Get students using Contains

            var students = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }

        // ==========================================
        // UPDATE
        // ==========================================

        // 1. Change Course Price

        static void ChangeCoursePrice()
        {
            using var context = new LearningPlatformContext();

            int courseId = 4;
            decimal newPrice = 900;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found. Price was not changed.");
                return;
            }

            course.Price = newPrice;

            context.SaveChanges();

            Console.WriteLine(
                $"Course price updated successfully to {course.Price}.");
        }

        // 2. Update Enrollment Progress

        static void UpdateProgress()
        {
            using var context = new LearningPlatformContext();

            int studentId = 3;
            int courseId = 1;
            decimal newProgress = 100;

            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine(
                    "Invalid progress. Progress must be between 0 and 100.");
                return;
            }

            // Find enrollment using the composite key

            var enrollment = context.Enrollments.Find(studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            enrollment.ProgressPercentage = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }

            context.SaveChanges();

            Console.WriteLine(
                $"Progress updated successfully: {enrollment.ProgressPercentage}%");

            Console.WriteLine($"Status: {enrollment.Status}");
        }

        // 3. Publish Course

        static void PublishCourse()
        {
            using var context = new LearningPlatformContext();

            int courseId = 4;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found. Cannot publish.");
                return;
            }

            course.IsPublished = true;

            if (course.PublishedDate == null)
            {
                course.PublishedDate = DateTime.Now;
            }

            context.SaveChanges();

            Console.WriteLine(
                $"Course '{course.Title}' published successfully.");

            Console.WriteLine(
                $"Published Date: {course.PublishedDate}");
        }

        // ==========================================
        // DELETE
        // ==========================================

        // 1. Unenroll Student

        static void UnenrollStudent()
        {
            using var context = new LearningPlatformContext();

            int studentId = 3;
            int courseId = 1;

            var enrollment =
                context.Enrollments.Find(studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            context.Enrollments.Remove(enrollment);
            context.SaveChanges();

            Console.WriteLine("Student unenrolled successfully.");
        }

        // 2. Try to Delete a Category with Subcategories

        static void AttemptDeleteCategoryWithSubCategories()
        {
            using var context = new LearningPlatformContext();

            int categoryId = 1;

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            context.Categories.Remove(category);

            try
            {
                context.SaveChanges();

                Console.WriteLine("Category deleted successfully.");
            }
            catch (DbUpdateException)
            {
                Console.WriteLine(
                    "Cannot delete this category because it has subcategories.");
            }
        }

        // 3. Safe Delete Category

        static void SafeDeleteCategory()
        {
            using var context = new LearningPlatformContext();

            int categoryId = 1;

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            bool hasSubCategories = context.Categories
                .Any(c => c.ParentCategoryId == categoryId);

            if (hasSubCategories)
            {
                Console.WriteLine(
                    "Cannot delete category because it has subcategories.");
                return;
            }

            context.Categories.Remove(category);
            context.SaveChanges();

            Console.WriteLine("Category deleted successfully.");
        }

        // 4. Delete Course and Its Enrollments

        static void DeleteCourse()
        {
            using var context = new LearningPlatformContext();

            int courseId = 1;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found. Cannot delete.");
                return;
            }

            int enrollmentsBefore = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments before deleting course: {enrollmentsBefore}");

            context.Courses.Remove(course);
            context.SaveChanges();

            // Course-Enrollment Cascade delete removes its enrollments.

            int enrollmentsAfter = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine("Course deleted successfully.");

            Console.WriteLine(
                $"Enrollments after deleting course: {enrollmentsAfter}");
        }
    }
}

