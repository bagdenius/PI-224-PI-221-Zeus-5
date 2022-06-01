using BLL.Models;
using BLL.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services;
using Xunit;

namespace BLL.Tests.Application
{
    public class UpdateApplicationTests : UnitOfWorkMock
    {
        [Fact]
        public async Task UpdateApplication_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var updatedIsApproved = true;

            // Act
            var application = await service.Get(ContextMock.ApplicationIdForUpdate);
            application.IsApproved = updatedIsApproved;
            service.Update(application);

            // Assert
            Assert.NotNull(await Context.Applications.SingleOrDefaultAsync(application =>
            application.Id == ContextMock.ApplicationIdForUpdate
            && application.IsApproved == updatedIsApproved));
        }

        [Fact]
        public async Task UpdateApplication_FailedOnWrongApplicantId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var ApplicantId = Guid.NewGuid().ToString();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Update(new ApplicationModel { ApplicantId = ApplicantId }));
        }

        [Fact]
        public async Task UpdateApplication_FailedOnWrongVacancyId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var ApplicantId = ContextMock.UserIdWithApplication;
            var VacancyId = Guid.NewGuid().ToString();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Update(new ApplicationModel 
            { 
                ApplicantId = ApplicantId,
                VacancyId = VacancyId 
            }));
        }
    }
}
