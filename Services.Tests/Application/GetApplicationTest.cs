using BLL.Models;
using BLL.Tests.Common;
using Services.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services;
using Xunit;

namespace BLL.Tests.Application
{
    public class GetApplicationTest : UnitOfWorkMock
    {
        [Fact]
        public async Task GetApplication_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            var result = await service.Get(ContextMock.ApplicationIdForUpdate);

            // Assert
            result.ShouldBeOfType<ApplicationModel>();
            result.Id.ShouldBe(ContextMock.ApplicationIdForUpdate);
            result.ApplicantId.ShouldBe(ContextMock.UserIdWithApplication);
            result.Name.ShouldBe("Name1");
            result.IsApproved.ShouldBe(false);
            result.VacancyId.ShouldBe(ContextMock.VacancyIdForUpdate);
        }

        [Fact]
        public async Task GetApplication_FailedOnWrongId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Get(Guid.NewGuid().ToString()));
        }
    }
}
