using Business;
using Data.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestss
{
    public class SubjectBusinessTests
    {
        private SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "SubjectBusinessTestDB")
                .Options;

            //ensuring the in-memory database is clear every time it is being deployed
            var context = new SchoolRegistryContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void AddSubject_ShouldAddSubjectToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject 
            { 
                Name = "Mathematics" 
            };

            // Act
            subjectBusiness.Add(subject);

            // Assert
            var result = context.Subjects.FirstOrDefault(s => s.Name == "Mathematics");
            Assert.NotNull(result);
            Assert.Equal("Mathematics", result.Name);
        }

        [Fact]
        public void GetAllSubjects_ShouldReturnAllSubjects()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var subjectBusiness = new SubjectBusiness(context);
            context.Subjects.AddRange(
                new Subject { Name = "Mathematics" },
                new Subject { Name = "Physics" }
            );
            context.SaveChanges();

            // Act
            var subjects = subjectBusiness.GetAll();

            // Assert
            Assert.Equal(2, subjects.Count);
            Assert.Contains(subjects, s => s.Name == "Mathematics");
            Assert.Contains(subjects, s => s.Name == "Physics");
        }

        [Fact]
        public void GetSubjectById_ShouldReturnCorrectSubject()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject 
            { 
                Name = "Chemistry" 
            };
            context.Subjects.Add(subject);
            context.SaveChanges();

            // Act
            var result = subjectBusiness.Get(subject.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Chemistry", result.Name);
        }

        [Fact]
        public void DeleteSubject_ShouldRemoveSubjectFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject 
            { 
                Name = "Biology" 
            };
            context.Subjects.Add(subject);
            context.SaveChanges();

            // Act
            subjectBusiness.Delete(subject.Id);

            // Assert
            var result = context.Subjects.FirstOrDefault(s => s.Id == subject.Id);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateSubject_ShouldModifyTeacherInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            using var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject
            {
                Name = "History"
            };
            subjectBusiness.Add(subject);
            context.SaveChanges();

            // Act
            subject.Name = "World History";
            subjectBusiness.Update(subject);

            // Assert
            var result = subjectBusiness.Get(subject.Id);
            Assert.NotNull(result);
            Assert.Equal("World History", result.Name);
        }
    }
}
