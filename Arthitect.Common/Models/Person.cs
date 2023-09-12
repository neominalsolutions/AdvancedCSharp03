using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arthitect.Common
{
  public class Person
  {
  
    public string Name { get; set; }
    public string SurName { get; set; }

    // Validasyon amaçlı DataAnnotations dan geliyor.
    [RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$", ErrorMessage = "İlgili pattern ile eşleşmedi")]
    public string Password { get; set; }


  }
}
