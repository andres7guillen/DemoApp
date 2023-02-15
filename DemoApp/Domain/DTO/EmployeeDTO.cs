using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public DateTime DateAdmission { get; set; }
        public Guid CompanyId { get; set; }
        public string Company { get; set; }
    }
}
