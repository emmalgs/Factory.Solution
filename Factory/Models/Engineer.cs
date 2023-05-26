using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    [Required(ErrorMessage="The engineer name field cannot be empty")]
    public string Name { get; set; }
    public bool Idle { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Do you have a factory yet? This engineer must be assigned to a factory location")]
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}