﻿using Business;
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

            //ensuring the in-memory database is clear every time it is being deployed
            var context = new SchoolRegistryContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void AddGrade_ShouldAddGradeToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var gradeBusiness = new GradeBusiness(context);

            var parent = new Parent { Id = 1, FirstName = "John", LastName = "Doe", Email = "gmail@com" };
            var student = new Student { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "outlook@com" };
            var subject = new Subject { Id = 1, Name = "Math" };
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "outlook@com", Phone = "123456789" };

            context.Subjects.Add(subject);
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.SaveChanges();

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
            var result = gradeBusiness.Get(grade.Id);
            Assert.NotNull(result);
            Assert.Equal(95.5, result.Value);
            Assert.Equal("Excellent performance", result.Comment);
        }

        [Fact]
        public void GetAllGrades_ShouldReturnAllGrades()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var gradeBusiness = new GradeBusiness(context);

            var parent = new Parent { Id = 1, FirstName = "John", LastName = "Doe", Email = "gmail@com" };
            var student = new Student { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "outlook@com" };
            var subject = new Subject { Id = 1, Name = "Math" };
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "outlook@com", Phone = "123456789" };

            context.Subjects.Add(subject);
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.SaveChanges();

            var grade1 = new Grade 
            { 
                Value = 85.0, Date = new DateOnly(2025, 5, 1), 
                Comment = "Good", 
                StudentId = 1, 
                SubjectId = 1, 
                TeacherId = 1 
            };
            var grade2 = new Grade 
            { 
                Value = 90.0, Date = new DateOnly(2025, 5, 2), 
                Comment = "Very Good", 
                StudentId = 1, 
                SubjectId = 1, 
                TeacherId = 1 
            };

            gradeBusiness.Add(grade1);
            gradeBusiness.Add(grade2);
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
            using var gradeBusiness = new GradeBusiness(context);

            var parent = new Parent { Id = 1, FirstName = "John", LastName = "Doe", Email = "gmail@com" };
            var student = new Student { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "outlook@com" };
            var subject = new Subject { Id = 1, Name = "Math" };
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "outlook@com", Phone = "123456789" };

            context.Subjects.Add(subject);
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.SaveChanges();

            var grade = new Grade 
            { 
                Value = 88.0, 
                Date = new DateOnly(2025, 5, 3), 
                Comment = "Satisfactory", 
                StudentId = 1, 
                SubjectId = 1, 
                TeacherId = 1 
            };
            gradeBusiness.Add(grade);
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
            using var gradeBusiness = new GradeBusiness(context);

            var parent = new Parent { Id = 1, FirstName = "John", LastName = "Doe", Email = "gmail@com" };
            var student = new Student { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "outlook@com" };
            var subject = new Subject { Id = 1, Name = "Math" };
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "outlook@com", Phone = "123456789" };

            context.Subjects.Add(subject);
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.SaveChanges();

            var grade = new Grade 
            { 
                Value = 70.0, Date = new DateOnly(2025, 5, 4), 
                Comment = "Needs Improvement", 
                StudentId = 18, SubjectId = 1, 
                TeacherId = 1 
            };
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
            using var gradeBusiness = new GradeBusiness(context);

            var parent = new Parent { Id = 1, FirstName = "John", LastName = "Doe", Email = "gmail@com" };
            var student = new Student { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "outlook@com" };
            var subject = new Subject { Id = 1, Name = "Math" };
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "outlook@com", Phone = "123456789" };

            context.Subjects.Add(subject);
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.SaveChanges();

            var grade = new Grade 
            { 
                Value = 60.0, 
                Date = new DateOnly(2025, 5, 5), 
                Comment = "Poor", 
                StudentId = 1, 
                SubjectId = 1, 
                TeacherId = 1 
            };
            gradeBusiness.Add(grade);
            context.SaveChanges();
            int id = grade.Id;

            // Act
            gradeBusiness.Delete(grade.Id);

            // Assert
            var result = context.Grades.FirstOrDefault(g => g.Id == id);
            Assert.Null(result);
        }
    }
}