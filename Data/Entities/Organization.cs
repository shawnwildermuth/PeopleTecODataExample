using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persontec.Api.Data.Entities
{
  public class Organization
  {
    public int OrganizationId { get; set; }
    [Required]
    [MaxLength(255)]
    public string OrganizationName { get; set; }
    [Required]
    public int OrganizationCode { get; set; }

    public Employee VicePresident { get; set; }
    [ForeignKey("vice_president_id")]
    public int VicePresidentId { get; set; }


  }
}