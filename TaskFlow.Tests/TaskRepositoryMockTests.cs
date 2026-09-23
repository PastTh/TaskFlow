using Moq;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using Xunit;

namespace TaskFlow.Tests
{
    public class TaskRepositoryMockTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsTask_WhenTaskExists()
        {
            var task = new TaskItem("Test task", Guid.NewGuid());
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var result = await mockRepo.Object.GetByIdAsync(task.Id);

            Assert.NotNull(result);
            Assert.Equal("Test task", result!.Title);
        }
    }
}