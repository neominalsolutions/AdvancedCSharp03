using AdvancedCSharp03.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Models
{
  public class WeatherMediator
  {
    private string currentWeather;

    // IMediator inteface ile eventi publish ettim.
    public void ChangeWeather(string weatherStatus, IMediator mediator)
    {
      string oldWeather = this.currentWeather;

      this.currentWeather = weatherStatus;

      var @event = new WeatherChanged(weatherStatus,oldWeather);
      mediator.Publish(@event);


    }
  }
}
