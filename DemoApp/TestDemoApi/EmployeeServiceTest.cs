using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestDemoApi
{
    public class EmployeeServiceTest
    {
        private readonly EmployeeService _sut;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

        public EmployeeServiceTest()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _sut = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async void CreateAEmployeeShouldWorks()
        {
            //Arrange
            var employeeEntity = new Employee();
            employeeEntity.CompanyId = Guid.NewGuid();
            employeeEntity.DateAdmission = DateTime.Now;
            employeeEntity.Id = Guid.NewGuid();
            employeeEntity.Name = "Employee Name";

            //arrange
            _employeeRepositoryMock
                .Setup(r => r.Create(It.IsAny<Employee>()))
                .ReturnsAsync(employeeEntity);

            //Act
            var employeeCreated = await _sut.Create(employeeEntity);

            //Assert
            Assert.NotNull(employeeCreated);
        }

        [Fact]
        public async void GetAllEmployeesShouldWorks()
        {
            var employees = new List<Employee>()
            {
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 1", Company = new Company(){ Id = Guid.NewGuid(),Name = "Company 1" } },
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 2", Company = new Company(){ Id = Guid.NewGuid(),Name = "Company 2" } }
            }.AsQueryable();

            _employeeRepositoryMock.Setup(r => r.GetAll())
                .Returns(employees);

            //Act
            var employeeService = new EmployeeService(_employeeRepositoryMock.Object);
            var employeesResult = await employeeService.GetAll();

            //Assert
            Assert.NotNull(employeesResult);
        }

        [Fact]
        public async void GetAEmployeeByIdShouldWorks()
        {
            //arrange
            var employees = new List<Employee>()
            {
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 1", Company = new Company() { Name = "Company 1", Id = Guid.NewGuid() } },
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 2", Company = new Company() { Name = "Company 2", Id = Guid.NewGuid() } }
            }.AsQueryable();
            var firstEmployee = employees.Where(c => c.Id == employees.FirstOrDefault().Id).AsQueryable();

            _employeeRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(firstEmployee);

            var employeeService = new EmployeeService(_employeeRepositoryMock.Object);
            var getByIdEmployee = await employeeService.GetById(employees.FirstOrDefault().Id);

            //act            
            Assert.Equal(firstEmployee.FirstOrDefault().Id, getByIdEmployee.Id);
        }

        [Fact]
        public async void UpdateEmployeeShouldWorks()
        {
            //arrange
            var employees = new List<Employee>()
            {
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 1", Company = new Company(){ Id = Guid.NewGuid(),Name = "Company 1" } },
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 2", Company = new Company(){ Id = Guid.NewGuid(),Name = "Company 2" } }
            };
            var firstEmployee = employees.First();
            var firstEmployeeCopy = employees.First();
            var firstEmployeeNameCopy = firstEmployeeCopy.Name;
            firstEmployee.Name = "New Employee Name";

            _employeeRepositoryMock.Setup(r => r.Edit(It.IsAny<Employee>()))
                .ReturnsAsync(firstEmployee);

            //act
            var companyUpdated = await _sut.Edit(firstEmployee);

            //assert
            Assert.Equal(firstEmployeeCopy.Id, companyUpdated.Id);
            Assert.NotEqual(firstEmployeeNameCopy, companyUpdated.Name);
        }

        [Fact]
        public async void DeleteAEmployeeShouldWorks()
        {
            //arrange
            var employees = new List<Employee>()
            {
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 1", Company = new Company() { Name = "Company 1", Id = Guid.NewGuid() } },
                new Employee(){ Id = Guid.NewGuid(), Name = "Employee 2", Company = new Company() { Name = "Company 2", Id = Guid.NewGuid() } }
            };
            var firstEmployee = employees.First();

            _employeeRepositoryMock.Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //act
            var isDeleted = await _sut.Delete(firstEmployee.Id);

            //assert
            Assert.True(isDeleted);
        }

    }
}
