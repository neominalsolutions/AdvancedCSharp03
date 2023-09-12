using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Extensions
{
  // extend edilecek sınıflar static olarak işaretlenir
  // ali can -> Ali CAN, ali => Ali
  public static class StringExtensions
  {
    public static string CapitalizedName(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return string.Empty;
      }
      else
      {
       value = value.Trim();
       string[] splitedValues = value.Split(" ");

        if(splitedValues.Length > 1)
        {
          // Ali Can
          return splitedValues[0][0].ToString().ToUpper() + splitedValues[0].Substring(1, splitedValues[0].Length -1) + " " + splitedValues[1].ToUpper();
        }
        else
        {
          return splitedValues[0][0].ToString().ToUpper() + splitedValues[0].Substring(1, splitedValues[0].Length -1);
        }
      }
    }

    public static string PhoneFormatted(this string value, string areaCode)
    {
      if(value.Length == 10)
      {
        // 3-3-2-2
        return $"+{areaCode} ({value.Substring(0, 3)}) {value.Substring(3, 3)}-{value.Substring(6, 2)}-{value.Substring(8, 2)}";
      }

      return value;

      
    }
  }
}
