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

namespace BLL.Tests.Vacancy
{
    public class RemoveVacancyTests : UnitOfWorkMock
    {
        [Fact]
        public async Task RemoveVacancy_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            await service.Remove(ContextMock.VacancyIdForDelete);

            // Assert
            Assert.Null(await Context.Vacancies.SingleOrDefaultAsync(vacancy =>
            vacancy.Id == ContextMock.VacancyIdForDelete));
        }

        [Fact]
        public async Task RemoveVacancy_FailedOnWrongId()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Remove(Guid.NewGuid().ToString()));
        }
    }
}
