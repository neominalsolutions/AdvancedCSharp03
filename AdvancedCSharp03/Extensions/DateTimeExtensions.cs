using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Extensions
{
  public static class DateTimeExtensions
  {
    public static string ToShortDate(this DateTime dateTime)
    {
      return $"{dateTime.Day}-{dateTime.Month}-{dateTime.Year}";
    }

    public static string ToShortTime(this DateTime dateTime)
    {
      return $"{dateTime.Hour}:{dateTime.Minute}";
    }
  }
}
