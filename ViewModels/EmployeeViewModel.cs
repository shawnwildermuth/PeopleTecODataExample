using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.ViewModels
{
  public class EmployeeViewModel
  {
    public int EmployeeId { get; set; } = -1;
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public int EmployeeNumber { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public string Status { get; set; }
    public string OrganizationName { get; set; }
    public int OrganizationCode { get; set; }
  }
}
