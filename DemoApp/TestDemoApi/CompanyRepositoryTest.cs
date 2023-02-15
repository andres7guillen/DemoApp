using Data.Context;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestDemoApi
{
    public class CompanyRepositoryTest
    {
        private readonly Mock<DemoContext> _contextMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        public CompanyRepositoryTest()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async void CreateACompanyShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;

            var CompanyEntity = new Company();
            CompanyEntity.Name = "Test Company";
            CompanyEntity.Id = Guid.NewGuid();
            CompanyEntity.Employees = new HashSet<Employee>();

            using (var context = new DemoContext(options)) 
            {
                //Act
                var companyRepository = new CompanyRepository(context);
                var companyCreated = await companyRepository.Create(CompanyEntity);

                //Assert
                Assert.NotNull(companyCreated);
            }
                
        }

        [Fact]
        public async void GetAllCompaniesShouldWorks() 
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;

            var CompanyEntity = new Company();
            CompanyEntity.Name = "Test Company";
            CompanyEntity.Id = Guid.NewGuid();
            CompanyEntity.Employees = new HashSet<Employee>();

            using (var context = new DemoContext(options)) 
            {
                context.Add(CompanyEntity);
                await context.SaveChangesAsync();
            }


            using (var context = new DemoContext(options))
            {


                //Act
                var companyRepository = new CompanyRepository(context);
                var companies = await companyRepository.GetAll().ToListAsync();

                //Assert
                Assert.NotNull(companies);
            }
        }

        [Fact]
        public async void GetCompanyByIdShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;
            Guid companyId;
            var CompanyEntity = new Company();
            CompanyEntity.Name = "Test Company";
            CompanyEntity.Id = Guid.NewGuid();
            CompanyEntity.Employees = new HashSet<Employee>();
            companyId = CompanyEntity.Id;
            using (var context = new DemoContext(options)) 
            {
                context.Add(CompanyEntity);
                await context.SaveChangesAsync();
            }

            using (var context = new DemoContext(options))
            {
                //Act
                var companyRepository = new CompanyRepository(context);
                var company = await companyRepository.GetById(companyId).FirstOrDefaultAsync();

                //Assert
                Assert.NotNull(company);
                Assert.Equal(company.Id, companyId);
            }


        }

        [Fact]
        public async void UpdateCompanyShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;
            Guid companyId;
            var CompanyEntity = new Company();
            CompanyEntity.Name = "Test Company";
            CompanyEntity.Id = Guid.NewGuid();
            CompanyEntity.Employees = new HashSet<Employee>();
            companyId = CompanyEntity.Id;
            string companyUpdatedName;
            using (var context = new DemoContext(options))
            {
                context.Add(CompanyEntity);
                await context.SaveChangesAsync();
            }

            using (var context = new DemoContext(options))
            {
                //Act
                var companyRepository = new CompanyRepository(context);
                var company = await companyRepository.GetById(companyId).FirstOrDefaultAsync();

                company.Name = "new name";
                companyUpdatedName = company.Name;
                context.Companies.Update(company);
                await context.SaveChangesAsync();

                var companyUpdated = await companyRepository.GetById(companyId).FirstOrDefaultAsync();

                //Assert
                Assert.NotNull(companyUpdated);
                Assert.Equal(companyUpdated.Name, companyUpdatedName);
            }


        }

        [Fact]
        public async void DeleteCompanyShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;
            Guid companyId;
            var CompanyEntity = new Company();
            CompanyEntity.Name = "Test Company";
            CompanyEntity.Id = Guid.NewGuid();
            CompanyEntity.Employees = new HashSet<Employee>();
            companyId = CompanyEntity.Id;
            using (var context = new DemoContext(options)) 
            {
                context.Add(CompanyEntity);
                await context.SaveChangesAsync();
            }

            using (var context = new DemoContext(options))
            {
                //Act
                var companyRepository = new CompanyRepository(context);
                var isDeleted = await companyRepository.Delete(companyId);
                var companyDeleted = context.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

                //Assert
                Assert.Null(companyDeleted.Result);
            }


        }

    }
}
