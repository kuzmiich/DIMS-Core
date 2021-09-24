using DIMS_Core.Tests.Repositories.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DIMS_Core.Tests.Repositories
{
    public class UserTaskRepositoryTests : IDisposable
    {
        private readonly UserTaskRepositoryFixture _fixture;

        public UserTaskRepositoryTests()
        {
            _fixture = new UserTaskRepositoryFixture();
        }

        [Fact]
        // Note: GetAll will work always in our case. It can be thrown into EF Core.
        // But it is implementation details of EF Core so we mustn't test these cases.
        public async Task GetAll_OK()
        {
            // Act
            var result = await _fixture.Repository
                                                 .GetAll()
                                                 .ToArrayAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Length);
        }

        [Fact]
        public async Task GetById_OK()
        {
            // Act
            var result = await _fixture.Repository.GetById(_fixture.UserTaskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_fixture.UserTaskId, result.TaskId);
            Assert.Equal(1, result.StateId);
            Assert.Equal(1, result.TaskId);
            Assert.Equal(1, result.UserId);
        }

        [Fact]
        public async Task GetById_EmptyId_Fail()
        {
            // Arrange
            const int id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task GetById_NotExistTask_Fail()
        {
            // Arrange
            const int id = int.MaxValue;

            // Act, Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _fixture.Repository.GetById(id));
        }

        [Fact]
        public async Task Create_OK()
        {
            // Arrange
            var entity = new DataAccessLayer.Models.UserTask
            {
                TaskId = 2,
                StateId = 2,
                UserId = 2
            };

            // Act
            var result = await _fixture.Repository.Create(entity);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.TaskId);
            Assert.Equal(entity.TaskId, result.TaskId);
            Assert.Equal(entity.StateId, result.StateId);
            Assert.Equal(entity.UserId, result.UserId);
        }

        [Fact]
        public async Task Create_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Create(null));
        }

        [Fact]
        public async Task Update_OK()
        {
            // Arrange
            var entity = new DataAccessLayer.Models.UserTask
            {
                UserTaskId = _fixture.UserTaskId,
                TaskId = 3,
                UserId = 3,
                StateId = 3
            };

            // Act
            var result = _fixture.Repository.Update(entity);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.TaskId);
            Assert.Equal(entity.TaskId, result.TaskId);
            Assert.Equal(entity.StateId, result.StateId);
            Assert.Equal(entity.UserId, result.UserId);
        }

        [Fact]
        public void Update_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            Assert.Throws<ArgumentNullException>(() => _fixture.Repository.Update(null));
        }

        [Fact]
        public async Task Delete_OK()
        {
            // Act
            await _fixture.Repository.Delete(_fixture.UserTaskId);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var deletedEntity = await _fixture.Context.Tasks.FindAsync(_fixture.UserTaskId);
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_EmptyId_Fail()
        {
            // Arrange
            const int id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Repository.Delete(id));
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }
    }
}
