using System;

namespace Persontec.Api.Data.Entities
{
  public class Transfer
  {
    public int TransferId { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? EndingDate { get; set; }
    public Employee Employee { get; set; }

    public Organization CurrentOrganization { get; set; }

  }
}