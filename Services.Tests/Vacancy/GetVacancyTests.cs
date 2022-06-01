﻿using BLL.Models;
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

namespace BLL.Tests.Vacancy
{
    public class GetVacancyTests : UnitOfWorkMock
    {
        [Fact]
        public async Task GetVacancy_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            var result = await service.Get(ContextMock.VacancyIdForUpdate);

            // Assert
            result.ShouldBeOfType<VacancyModel>();
            result.Id.ShouldBe(ContextMock.VacancyIdForUpdate);
            result.Title.ShouldBe("Title");
            result.Description.ShouldBe("Description1");
            result.Location.ShouldBe("Location1");
            result.Sector.ShouldBe("Sector1");
        }

        [Fact]
        public async Task GetVacancy_FailedOnWrongId()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Get(Guid.NewGuid().ToString()));
        }
    }
}