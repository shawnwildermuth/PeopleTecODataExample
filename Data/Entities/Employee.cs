using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Data.Entities
{
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }
    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }
    [Range(100, 999999)]
    public int EmployeeNumber { get; set; }

    public ICollection<EmploymentPeriod> EmploymentPeriods { get; set; }
    public Employee Supervisor { get; set; }
    public Organization Organization { get; set; }
  }
}
