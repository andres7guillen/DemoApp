using DemoApi.Models;
using Domain.Entities;

namespace DemoApi.Converts
{
    public static class CompanyConvert
    {
        public static CompanyModel toModel(Company input) 
        { 
            CompanyModel output = new CompanyModel();
            output.Id = input.Id.ToString();
            output.Name = input.Name;
            output.Id = input.Id.ToString();
            output.Employees =  input.Employees.Count >= 1 ?  EmployeeConvert.toListModel(input.Employees.ToList()) : new List<EmployeeModel>();
            return output;
        }

        public static List<CompanyModel> toListModel(List<Company> input) 
        {
            return input.Select(e => toModel(e)).ToList();
        }

        public static Company toEntitie(CompanyModel input) 
        {
            Company output = new Company();
            output.Id = Guid.Parse(input.Id);
            output.Name = input.Name;
            return output;
        }

    }
}
