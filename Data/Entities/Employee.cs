using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    
    public virtual Organization Organization { get; set; }
    public int OrganizationId { get; set; }

    [ForeignKey("SupervisorId")]
    public ICollection<Employee> DirectReports { get; set; }

    public ICollection<Transfer> Transfers { get; set; }
  }
}
