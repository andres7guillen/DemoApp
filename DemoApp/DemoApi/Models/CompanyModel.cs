using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class CompanyModel
    {
        public string? Id { get; set; }
        public IEnumerable<EmployeeModel>? Employees { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }
    }
}
