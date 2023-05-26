using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public DateTime LastMaintenance { get; set; }
    public string Status { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Do you have a factory yet? This engineer must be assigned to a factory location")]
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
    public static Dictionary<int, string> StatusOptions = new Dictionary<int, string>() {
      {1, "Operational"},
      {2, "Malfunctioning"},
      {3, "Repairing"}
    };
  }
}