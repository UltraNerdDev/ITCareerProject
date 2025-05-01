using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace XUnitTestss
{
    public class GradeBusinessTests
    {
        private SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "GradeBusinessTestDB")
                .Options;

            return new SchoolRegistryContext(options);
        }

        [Fact]
        public void AddGrade_ShouldAddGradeToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var gradeBusiness = new GradeBusiness(context);
            var grade = new Grade
            {
                Value = 95.5,
                Date = new DateOnly(2025, 5, 1),
                Comment = "Excellent performance",
                StudentId = 1,
                SubjectId = 1,
                TeacherId = 1
            };

            // Act
            gradeBusiness.Add(grade);

            // Assert
            var result = context.Grades.FirstOrDefault(g => g.Comment == "Excellent performance");
            Assert.NotNull(result);
            Assert.Equal(95.5, result.Value);
            Assert.Equal("Excellent performance", result.Comment);
        }

        [Fact]
        public void GetAllGrades_ShouldReturnAllGrades()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var gradeBusiness = new GradeBusiness(context);
            context.Grades.AddRange(
                new Grade { Value = 85.0, Date = new DateOnly(2025, 5, 1), Comment = "Good", StudentId = 1, SubjectId = 1, TeacherId = 1 },
                new Grade { Value = 90.0, Date = new DateOnly(2025, 5, 2), Comment = "Very Good", StudentId = 2, SubjectId = 2, TeacherId = 2 }
            );
            context.SaveChanges();

            // Act
            var grades = gradeBusiness.GetAll();

            // Assert
            Assert.Equal(2, grades.Count);
            Assert.Contains(grades, g => g.Comment == "Good");
            Assert.Contains(grades, g => g.Comment == "Very Good");
        }

        [Fact]
        public void GetGradeById_ShouldReturnCorrectGrade()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var gradeBusiness = new GradeBusiness(context);
            var grade = new Grade { Value = 88.0, Date = new DateOnly(2025, 5, 3), Comment = "Satisfactory", StudentId = 1, SubjectId = 1, TeacherId = 1 };
            context.Grades.Add(grade);
            context.SaveChanges();

            // Act
            var result = gradeBusiness.Get(grade.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(88.0, result.Value);
            Assert.Equal("Satisfactory", result.Comment);
        }

        [Fact]
        public void UpdateGrade_ShouldModifyGradeInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var gradeBusiness = new GradeBusiness(context);
            var grade = new Grade { Value = 70.0, Date = new DateOnly(2025, 5, 4), Comment = "Needs Improvement", StudentId = 1, SubjectId = 1, TeacherId = 1 };
            context.Grades.Add(grade);
            context.SaveChanges();

            // Act
            grade.Value = 75.0;
            grade.Comment = "Improved";
            gradeBusiness.Update(grade);

            // Assert
            var result = context.Grades.FirstOrDefault(g => g.Id == grade.Id);
            Assert.NotNull(result);
            Assert.Equal(75.0, result.Value);
            Assert.Equal("Improved", result.Comment);
        }

        [Fact]
        public void DeleteGrade_ShouldRemoveGradeFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var gradeBusiness = new GradeBusiness(context);
            var grade = new Grade { Value = 60.0, Date = new DateOnly(2025, 5, 5), Comment = "Poor", StudentId = 1, SubjectId = 1, TeacherId = 1 };
            context.Grades.Add(grade);
            context.SaveChanges();

            // Act
            gradeBusiness.Delete(grade.Id);

            // Assert
            var result = context.Grades.FirstOrDefault(g => g.Id == grade.Id);
            Assert.Null(result);
        }
    }
}