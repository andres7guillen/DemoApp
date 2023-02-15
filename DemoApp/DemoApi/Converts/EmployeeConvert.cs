using DemoApi.Models;
using Domain.Entities;

namespace DemoApi.Converts
{
    public static class EmployeeConvert
    {
        public static EmployeeModel toModel(Employee input) 
        { 
            EmployeeModel output = new EmployeeModel();
            output.DateAdmission = input.DateAdmission;
            output.Name = input.Name;
            output.CompanyId = input.CompanyId.ToString();
            output.Company = input.Company != null ? input.Company.Name : "No company";
            output.Id = input.Id.ToString();
            return output;
        }

        public static List<EmployeeModel> toListModel(List<Employee> input) 
        {
            return input.Select(e => toModel(e)).ToList();
        }

        public static Employee toEntitie(EmployeeModel input)
        { 
            Employee output = new Employee();
            output.DateAdmission = input.DateAdmission;
            output.Name = input.Name;
            output.Id = input.Id != null ? Guid.Parse(input.Id) : Guid.Empty;
            output.CompanyId = Guid.Parse(input.CompanyId);
            return output;
        }

    }
}
