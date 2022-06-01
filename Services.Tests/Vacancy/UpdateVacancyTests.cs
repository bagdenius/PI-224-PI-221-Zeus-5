﻿using BLL.Models;
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
    public class UpdateVacancyTests : UnitOfWorkMock
    {
        [Fact]
        public async Task UpdateVacancy_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);
            var updatedTitle = "Updated Title";
            var updatedDescription = "Updated Description";
            var updatedLocation = "Updated Location";
            var updatedSector = "UpdatedSector";
            var updatedEmployerId = ContextMock.UserIdWithoutApplication;

            // Act
            var vacancy = await service.Get(ContextMock.VacancyIdForUpdate);
            vacancy.Title = updatedTitle;
            vacancy.Description = updatedDescription;
            vacancy.Location = updatedLocation;
            vacancy.Sector = updatedSector;
            await service.Update(vacancy);

            // Assert
            Assert.NotNull(await Context.Vacancies.SingleOrDefaultAsync(vacancy =>
            vacancy.Id == ContextMock.VacancyIdForUpdate
            && vacancy.Title == updatedTitle
            && vacancy.Description == updatedDescription
            && vacancy.Location == updatedLocation
            && vacancy.Sector == updatedSector));
        }

        [Fact]
        public async Task UpdateVacancy_FailedOnWrongEmployerId()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);
            var EmployerId = Guid.NewGuid().ToString();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Update(new VacancyModel { EmployerId = EmployerId }));
        }
    }
}
