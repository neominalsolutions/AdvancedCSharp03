using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Events
{
  // Weather change olduğunda çalışacak olan Notification Handler
  public class WheaterChangedHandler : INotificationHandler<WeatherChanged>
  {
    public Task Handle(WeatherChanged notification, CancellationToken cancellationToken)
    {
      Console.WriteLine($"Eski Değer {notification.OldStatus} Yeni Değer: {notification.NewStatus}");

      return Task.CompletedTask;
    }
  }
}
