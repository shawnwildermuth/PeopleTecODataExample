using System;
using System.ComponentModel.DataAnnotations;

namespace Persontec.Api.Data.Entities
{
  public class EmploymentPeriod
  {
    public int EmploymentPeriodId { get; set; }
    [Required]
    public DateTime StartingDate { get; set; }
    public DateTime? EndingDate { get; set; }
    [Required]
    [MaxLength(50)]
    public string Status { get; set; }
  }
}