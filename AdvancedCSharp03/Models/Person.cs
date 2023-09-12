using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Models
{
  public class Person
  {
    [JsonProperty("nAmE")]
    public string Name { get; set; }

    [JsonProperty("surname")]
    public string SurName { get; set; }

    [JsonProperty("bT")]
    public DateTime BirthDate { get; set; }

    [JsonIgnore]
    public int Age { get; set; } = 18; // Default int değeri 0


    public string GetFullName()
    {
      return $" {this.Name} {this.SurName}";
    }
  }
}
