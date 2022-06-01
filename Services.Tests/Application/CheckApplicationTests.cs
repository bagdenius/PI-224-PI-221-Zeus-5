using BLL.Tests.Common;
using Services.Exceptions;
using System;
using System.Threading.Tasks;
using UI.Services;
using Xunit;

namespace BLL.Tests.Application
{
    public class CheckApplicationTests : UnitOfWorkMock
    {
        [Fact]
        public async Task CheckApplication_True()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var UserId = ContextMock.UserIdWithApplication;
            var VacancyId = ContextMock.VacancyIdForUpdate;

            // Act
            // Assert
            Assert.True(service.CheckApplication(UserId, VacancyId));
        }

        [Fact]
        public async Task CheckApplication_False()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var UserId = ContextMock.UserIdWithoutApplication;
            var VacancyId = ContextMock.VacancyIdForUpdate;

            // Act
            // Assert
            Assert.False(service.CheckApplication(UserId, VacancyId));
        }

        [Fact]
        public async Task CheckApplication_FailedOnWrondUserId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var UserId = Guid.NewGuid().ToString();
            var VacancyId = ContextMock.VacancyIdForUpdate;

            // Act
            // Assert
            Assert.Throws<NotFoundException>(() => service.CheckApplication(UserId, VacancyId));
        }

        [Fact]
        public async Task CheckApplication_FailedOnWrondVacancyId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var UserId = ContextMock.UserIdWithApplication;
            var VacancyId = Guid.NewGuid().ToString();

            // Act
            // Assert
            Assert.Throws<NotFoundException>(() => 
            service.CheckApplication(UserId, VacancyId));
        }
    }
}
