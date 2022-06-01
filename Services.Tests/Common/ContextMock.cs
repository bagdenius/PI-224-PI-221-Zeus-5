﻿using DAL.Entities;
using Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Tests.Common
{
    public static class ContextMock
    {
        public static string ApplicationIdForUpdate = Guid.NewGuid().ToString();

        public static string VacancyIdForUpdate = Guid.NewGuid().ToString();
        public static string VacancyIdForApplicationUpdate = Guid.NewGuid().ToString();
        public static string VacancyIdForDelete = Guid.NewGuid().ToString();

        public static string UserIdWithApplication = Guid.NewGuid().ToString();
        public static string UserIdWithoutApplication = Guid.NewGuid().ToString();
        public static string UserIdForApplicationUpdate = Guid.NewGuid().ToString();

        public static DatabaseContext Create()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);
            context.Database.EnsureCreated();
            context.Users.AddRange(
                new DAL.Entities.User
                {
                    Id = UserIdWithApplication,
                    Role = Roles.Applicant,
                    FullName = "FullName1",
                    IsVerified = true
                },
                new DAL.Entities.User
                {
                    Id = UserIdWithoutApplication,
                    Role = Roles.Applicant,
                    FullName = "FullName2",
                    IsVerified = true
                },
                new DAL.Entities.User
                {
                    Id = UserIdForApplicationUpdate,
                    Role = Roles.Applicant,
                    FullName = "FullName3",
                    IsVerified = true
                });

            context.Vacancies.AddRange(
                new DAL.Entities.Vacancy
                {
                    Id = VacancyIdForUpdate,
                    Title = "Title",
                    Description = "Description1",
                    Location = "Location1",
                    Sector = "Sector1",
                    EmployerId = UserIdWithApplication
                },
                new DAL.Entities.Vacancy
                {
                    Id = VacancyIdForApplicationUpdate,
                    Title = "Title",
                    Description = "Description2",
                    Location = "Location2",
                    Sector = "Sector2",
                    EmployerId = UserIdWithApplication
                },
                new DAL.Entities.Vacancy
                {
                    Id = VacancyIdForDelete,
                    Title = "Title3",
                    Description = "Description3",
                    Location = "Location3",
                    Sector = "Sector3",
                    EmployerId = UserIdWithApplication
                });

            context.Applications.AddRange(
                new DAL.Entities.Application
                {
                    Id = ApplicationIdForUpdate,
                    ApplicantId = UserIdWithApplication,
                    Name = "Name1",
                    IsApproved = false,
                    VacancyId = VacancyIdForUpdate
                });
            context.SaveChanges();
            return context;
        }

        public static void Destroy(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
