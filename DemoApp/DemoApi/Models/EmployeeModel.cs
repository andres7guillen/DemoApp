using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class EmployeeModel
    {
        public string? Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }
        public DateTime DateAdmission { get; set; }
        public string? CompanyId { get; set; }
        public string? Company { get; set; }
    }
}

