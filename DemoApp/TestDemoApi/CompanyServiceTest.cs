
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Services;
using Moq;
using Xunit;

namespace TestDemoApi
{
    public class CompanyServiceTest
    {
        private readonly CompanyService _sut;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        public CompanyServiceTest()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _sut = new CompanyService(_companyRepositoryMock.Object);
        }

        [Fact]
        public async void CreateACompanyShouldWorks()
        {
            //Arrange
            var CompanyEntity = new Company();
            CompanyEntity.Name = "Test Company";
            CompanyEntity.Id = Guid.NewGuid();
            CompanyEntity.Employees = new HashSet<Employee>();

            //arrange
            _companyRepositoryMock
                .Setup(r => r.Create(It.IsAny<Company>()))
                .ReturnsAsync(CompanyEntity);

            //Act
            var companyCreated = await _sut.Create(CompanyEntity);

            //Assert
            Assert.NotNull(companyCreated);
            _companyRepositoryMock.Verify(r => r.Create(companyCreated));
        }

        [Fact]
        public async void GetAllCompaniesShouldWorks()
        {
            //Arrange
            var companies = new List<Company>()
            {
                new Company(){ Id = Guid.NewGuid(), Name = "Company 1", Employees = new HashSet<Employee>() },
                new Company(){ Id = Guid.NewGuid(), Name = "Company 2", Employees = new HashSet<Employee>() }
            }.AsQueryable();


            _companyRepositoryMock.Setup(r => r.GetAll())
                .Returns(companies);

            //Act
            var companyService = new CompanyService(_companyRepositoryMock.Object);
            var companiesResult = await companyService.GetAll();

            //Assert
            Assert.NotNull(companiesResult);
        }

        [Fact]
        public async void GetACompanyByIdShouldWorks()
        {
            //arrange
            var companies = new List<Company>()
            {
                new Company(){ Id = Guid.NewGuid(), Name = "Company 1", Employees = new HashSet<Employee>() },
                new Company(){ Id = Guid.NewGuid(), Name = "Company 2", Employees = new HashSet<Employee>() }
            }.AsQueryable();
            var firstCompany = companies.Where(c => c.Id == companies.FirstOrDefault().Id).AsQueryable();

            _companyRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(firstCompany);

            var companyService = new CompanyService(_companyRepositoryMock.Object);
            var getByIdCompany = await companyService.GetById(companies.FirstOrDefault().Id);

            //act            
            Assert.Equal(firstCompany.FirstOrDefault().Id, getByIdCompany.Id);
        }

        [Fact]
        public async void UpdateCompanyShouldWorks()
        {
            //arrange
            List<Company> companies = new List<Company>()
            {
                new Company(){ Id = Guid.NewGuid(), Name = "Company 1", Employees = new HashSet<Employee>() },
                new Company(){ Id = Guid.NewGuid(), Name = "Company 2", Employees = new HashSet<Employee>() }
            };
            var firstCompany = companies.First();
            var firstCompanyCopy = companies.First();
            var firstCompanyNameCopy = firstCompanyCopy.Name;
            firstCompany.Name = "New Company Name";

            _companyRepositoryMock.Setup(r => r.Edit(It.IsAny<Company>()))
                .ReturnsAsync(firstCompany);

            //act
            var companyUpdated = await _sut.Edit(firstCompany);

            //assert
            Assert.Equal(firstCompanyCopy.Id, companyUpdated.Id);
            Assert.NotEqual(firstCompanyNameCopy, companyUpdated.Name);
        }

        [Fact]
        public async void DeleteACompanyShouldWorks()
        {
            //arrange
            List<Company> companies = new List<Company>()
            {
                new Company(){ Id = Guid.NewGuid(), Name = "Company 1", Employees = new HashSet<Employee>() },
                new Company(){ Id = Guid.NewGuid(), Name = "Company 2", Employees = new HashSet<Employee>() }
            };
            var firstCompany = companies.First();

            _companyRepositoryMock.Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //act
            var isDeleted = await _sut.Delete(firstCompany.Id);
            var companyDeleted = await _sut.GetById(firstCompany.Id);

            //assert
            Assert.True(isDeleted);
            Assert.Null(companyDeleted);
        }
    }
}
