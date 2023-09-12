using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arthitect.Common
{
 
  public class Person
  {
    [JsonIgnore] // bu property sınıf içinden dışarıya çıkmıyor
    public readonly string TCNo = "878545454";

    [JsonPropertyName("name")] // json olarak 

    [JsonPropertyOrder(2)] // json dosyasını oluşturuken burada verilen önceliğe göre json çıktısı oluşturuyor
    public string Name { get; set; }

    [JsonPropertyOrder(1)]
    public string SurName { get; set; }

    // Validasyon amaçlı DataAnnotations dan geliyor.
    [RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$", ErrorMessage = "İlgili pattern ile eşleşmedi")]
    public string Password { get; set; }


  }
}
