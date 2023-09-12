using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Events
{
  // Event özelliği gösteren yapıları INotification interface ile işaretleriz.
  // EventDelegate tekniğindeki EventArgs class denk gelen yapı
  public record WeatherChanged:INotification
  {
    public WeatherChanged(string newStatus, string oldStatus)
    {
      NewStatus = newStatus;
      OldStatus = oldStatus;
    }

    public string NewStatus { get; init; }
    public string OldStatus { get; init; }
  }
}
