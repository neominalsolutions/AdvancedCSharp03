using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp03.Models
{
  // Değişen Hava durumu raporunu takip ettiğimiz sınıf.
  // genelde delegate yapılar EventHandler suffix ile yazılır. Bir olay meydana geldiğinde bir eylemi işlemek amaçlı kullanıldığında genelde bu şekilde tanımlanır

  // base EventArgs type sayesinde bir eventin içerisine kendi custom nesneleri event argüman olarak gönderebiliriz.
  // Args nesnelerinde contructordan veri gönderimi dışında method gibi üyeler olmaz.
  public class WeatherEventArgs:EventArgs
  {
    public string CurrentStatus { get;  init; }
    public string OldStatus { get; init; }

    public DateTime OperationDateTime { get; init; }

    public WeatherEventArgs(string currentStatus, string oldStatus)
    {
      this.CurrentStatus = currentStatus;
      this.OldStatus = oldStatus;
      OperationDateTime = DateTime.Now;
    }
  }
  
  
  public class Weather
  {
    private string currentWeather = "Güneşli";

    public delegate void WeatherChangedEventHandler(string weatherStatus);
    public event WeatherChangedEventHandler OnWeaterChanged; // gerçekleşek olayı bu event vasıtası ile işleyeceğiz.
    // event tanımlarıda bir olayın gerçekleşmesini temsil ettiğinde dolayı dili geçmiş zaman kipinde kullanılırlar

    public delegate void WeatherStatusChangedEventHandler(WeatherEventArgs args);

    public event WeatherStatusChangedEventHandler onWeatherStatusChanged;

    public void ChangeWeather(string weatherStatus)
    {
      string oldWeather = this.currentWeather;

      this.currentWeather = weatherStatus;
      OnWeaterChanged.Invoke(weatherStatus); // yeni bir hava durum parameteresi gönderdik. // olay sonucu delegate imzasına ship bir methodun tetiklenmesini sağladık.
      var args = new WeatherEventArgs(weatherStatus, oldWeather);
      onWeatherStatusChanged.Invoke(args);
    }

  }
}
