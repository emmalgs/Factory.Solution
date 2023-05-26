using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Location
  {
    public int LocationId { get; set; }
    [Required(ErrorMessage="Location must have a city name")]
    public string City { get; set; }
    [Required(ErrorMessage="Location must have a country name")]
    public string Country { get; set; }
    public List<Engineer> Engineers { get; set; }
    public List<Machine> Machines { get; set; }
  }
}