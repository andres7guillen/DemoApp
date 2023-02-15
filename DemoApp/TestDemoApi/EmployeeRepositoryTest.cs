using Data.Context;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestDemoApi
{
    public class EmployeeRepositoryTest
    {
        private readonly Mock<DemoContext> _contextMock;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        public EmployeeRepositoryTest()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async void CreateAEmployeeShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;

            var EmployeeEntity = new Employee();
            EmployeeEntity.Name = "Test Employee";
            EmployeeEntity.Id = Guid.NewGuid();
            EmployeeEntity.DateAdmission = DateTime.Now;
            EmployeeEntity.CompanyId = Guid.NewGuid();

            using (var context = new DemoContext(options))
            {
                //Act
                var employeeRepository = new EmployeeRepository(context);
                var employeeCreated = await employeeRepository.Create(EmployeeEntity);

                //Assert
                Assert.NotNull(employeeCreated);
            }

        }

        [Fact]
        public async void GetAllEmployeesShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;

            var EmployeeEntity = new Employee();
            EmployeeEntity.Name = "Test Employee";
            EmployeeEntity.Id = Guid.NewGuid();
            EmployeeEntity.DateAdmission = DateTime.Now;
            EmployeeEntity.CompanyId = Guid.NewGuid();

            using (var context = new DemoContext(options))
            {
                context.Add(EmployeeEntity);
                await context.SaveChangesAsync();
            }


            using (var context = new DemoContext(options))
            {


                //Act
                var employeeRepository = new EmployeeRepository(context);
                var employees = await employeeRepository.GetAll().ToListAsync();

                //Assert
                Assert.NotNull(employees);
            }
        }

        [Fact]
        public async void GetEmployeeByIdShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;
            Guid employeeId;
            var EmployeeEntity = new Employee();
            EmployeeEntity.Name = "Test Employee";
            EmployeeEntity.Id = Guid.NewGuid();
            EmployeeEntity.DateAdmission = DateTime.Now;
            EmployeeEntity.CompanyId = Guid.NewGuid();
            EmployeeEntity.Company = new Company() { Id = EmployeeEntity.CompanyId, Name = "COMPANY TEST 1" };
            employeeId = EmployeeEntity.Id;

            using (var context = new DemoContext(options))
            {
                context.Employees.Add(EmployeeEntity);
                var result = await context.SaveChangesAsync();
            }

            using (var context = new DemoContext(options))
            {
                //Act
                var employeeRepository = new EmployeeRepository(context);
                var employee = employeeRepository.GetById(employeeId).FirstOrDefaultAsync().Result;

                //Assert
                Assert.NotNull(employee);
                Assert.Equal(EmployeeEntity.Id, employeeId);
            }


        }

        [Fact]
        public async void UpdateEmployeeShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;
            Guid employeeId;
            string employeeUpdatedName;
            var EmployeeEntity = new Employee();
            EmployeeEntity.Name = "Test Employee";
            EmployeeEntity.Id = Guid.NewGuid();
            EmployeeEntity.DateAdmission = DateTime.Now;
            EmployeeEntity.CompanyId = Guid.NewGuid();
            EmployeeEntity.Company = new Company() { Id = EmployeeEntity.CompanyId, Name = "COMPANY TEST 1" };
            employeeId = EmployeeEntity.Id;
            using (var context = new DemoContext(options))
            {
                context.Add(EmployeeEntity);
                await context.SaveChangesAsync();
            }

            using (var context = new DemoContext(options))
            {
                //Act
                var employeeRepository = new EmployeeRepository(context);
                var employee = await employeeRepository.GetById(employeeId).FirstOrDefaultAsync();

                employee.Name = "new name";
                employeeUpdatedName = employee.Name;
                context.Employees.Update(employee);
                await context.SaveChangesAsync();

                var employeeUpdated = await employeeRepository.GetById(employeeId).FirstOrDefaultAsync();

                //Assert
                Assert.NotNull(employeeUpdated);
                Assert.Equal(employeeUpdated.Name, employeeUpdatedName);
            }


        }

        [Fact]
        public async void DeleteEmployeeShouldWorks()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DemoContext>()
                .UseInMemoryDatabase(databaseName: "DemoApp")
                .Options;
            Guid employeeId;
            var EmployeeEntity = new Employee();
            EmployeeEntity.Name = "Test Employee";
            EmployeeEntity.Id = Guid.NewGuid();
            EmployeeEntity.DateAdmission = DateTime.Now;
            EmployeeEntity.CompanyId = Guid.NewGuid();
            employeeId = EmployeeEntity.Id;
            employeeId = EmployeeEntity.Id;
            using (var context = new DemoContext(options))
            {
                context.Add(EmployeeEntity);
                await context.SaveChangesAsync();
            }

            using (var context = new DemoContext(options))
            {
                //Act
                var employeeRepository = new EmployeeRepository(context);
                var isDeleted = await employeeRepository.Delete(employeeId);
                var employeeDeleted = context.Employees.FirstOrDefaultAsync(c => c.Id == employeeId);

                //Assert
                Assert.Null(employeeDeleted.Result);
            }


        }
    }
}
