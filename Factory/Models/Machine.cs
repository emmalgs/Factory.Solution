namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public DateTime LastMaintenance { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}