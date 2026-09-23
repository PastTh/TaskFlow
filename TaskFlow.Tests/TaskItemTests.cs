using TaskFlow.Domain.Entities;
using Xunit;

namespace TaskFlow.Tests
{
    public class TaskItemTests
    {
        [Fact]
        public void Constructor_WithValidTitle_CreatesTask()
        {
            var projectId = Guid.NewGuid();
            var task = new TaskItem("Fix bug", projectId);

            Assert.Equal("Fix bug", task.Title);
            Assert.Equal(projectId, task.ProjectId);
        }

        [Fact]
        public void Constructor_WithEmptyTitle_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new TaskItem("", Guid.NewGuid()));
        }

        [Fact]
        public void Rename_WithEmptyTitle_ThrowsException()
        {
            var task = new TaskItem("Original", Guid.NewGuid());

            Assert.Throws<ArgumentException>(() => task.Rename(""));
        }
    }
}