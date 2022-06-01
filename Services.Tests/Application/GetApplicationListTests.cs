﻿using BLL.Models;
using BLL.Tests.Common;
using DAL.Entities;
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
    public class GetApplicationListTests : UnitOfWorkMock
    {
        [Fact]
        public async Task GetApplicationStringsListAsync_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            var result = await service.GetStringsAsync();

            // Assert
            result.Count().ShouldBe(1);
        }

        [Fact]
        public async Task GetApplicationList_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            var result = service.Get();

            // Assert
            result.ShouldBeOfType<List<ApplicationModel>>();
            result.Count().ShouldBe(1);
        }

        [Fact]
        public async Task FindByUserIdApplicationList_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            var result = await service.FindByUserId(ContextMock.UserIdWithApplication);

            // Assert
            result.ShouldBeOfType<List<ApplicationModel>>();
            result.Count().ShouldBe(1);
        }

        [Fact]
        public async Task FindByUserIdApplicationList_FailedOnWrongUserId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => 
            await service.FindByUserId(Guid.NewGuid().ToString()));
        }
    }
}
