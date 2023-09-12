// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

// Extension Sınıflar
// Regex ile Çalışma
// Event Delegate
// Serialization
// File Stream

// Extension Sınıflar: C# daki bir tipi genişletip, yeni özellikler kazandırmamız sağlayan yapılar. Var olan tiplere bu sayede yeni özellilkler ekleyip kullanabiliriz.
// varolan tip bozulamaz, ufak müdahale ile yeni bir özellik kazandır. Özellikler metholar vasıtası ile yapılır


using AdvancedCSharp03.Extensions;
using AdvancedCSharp03.Models;
using Arthitect.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

public class Program
{
  public static void Main(string[] args)
  {

    #region extensions
    // DateTimeExtensionSample();
    // RegexSample();
    // RegexSample02();
    // DelegateSample();
    // EventDelegateSample();
    MediatorWeatherSample();
    #endregion
  }

  // MediaTR bu paket Event Driven Programlama işlemleri yapmamızı sağlar
  // yaygın kullanımı olan bir paket. OOP bazlı event programlama sağlar.

  public static void MediatorWeatherSample()
  {
    var serviceCollection = new ServiceCollection();
    // Registeration işlemi
    // uygulama içerisinde herhangi biryer mediator service ile ilgili instance işlemleri otomatik hale geliyor.
    //serviceCollection.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining<Program>());

    // şuan üzerinde çalıştığımız assembly load et
    serviceCollection.AddMediatR(opt => Assembly.GetExecutingAssembly());

    // mediator paketini Net core uygulamalarında uygulama tanıt.

    var provider = serviceCollection.BuildServiceProvider(); // sistemde çalıştır.

    var mediator = provider.GetRequiredService<IMediator>(); // uygulama içerisinde mediator instance üretildi.

    // send methodu ile bir kod blogunu tetikle, bir methodu handle MethodHandling et yapıyor
    // publish ile de bir eventi tetikle. EventHandling işlemi yapar.

    WeatherMediator m = new WeatherMediator();

    Console.WriteLine("Hava durumu giriniz");
    string status = Console.ReadLine();
    m.ChangeWeather(status, mediator);

    Console.WriteLine("Yeni bir hava durumu giriniz");
    status = Console.ReadLine();
    m.ChangeWeather(status, mediator);


    //mediator.Publish();
  }

  public static void EventDelegateSample()
  {
    Weather w = new Weather();
    w.OnWeaterChanged += W_OnWeaterChanged; // EventListener olarak eventi dinlemek için += operatörü ile evente bağlandık.
    // JS addEventListener ile aynı mantıkta bir tanımlama
    w.onWeatherStatusChanged += W_onWeatherStatusChanged;

    Console.WriteLine("Hava durum raporu giriniz");
    string newWeatherStatus = Console.ReadLine();

    w.ChangeWeather(newWeatherStatus);

    Console.WriteLine("Yeni bir hava durumu raporu giriniz");
    newWeatherStatus = Console.ReadLine();

    w.ChangeWeather(newWeatherStatus); // Olayı gerçekleştir
  }

  private static void W_onWeatherStatusChanged(WeatherEventArgs args)
  {
    Console.WriteLine($"Eski durum {args.OldStatus}, Yeni durum {args.CurrentStatus} saat : {args.OperationDateTime.Hour}:{args.OperationDateTime.Minute}");

  }

  // olay sonucunda yapılan eylem
  // weatherStatus EventArgs olarak kullanılır
  private static void W_OnWeaterChanged(string weatherStatus)
  {
    Console.WriteLine($"Hava durumu güncellendi. Güncel hava durumu: {weatherStatus}");
  }

  // Delegate => Methodların elçiliğini yapan yapılar. aynı imzaya sahip methodların delegate ile tetiklenmesi sağlanır. 

  delegate void MyDelegate(string message); // message args, void değer döndürmeli. Bu imzaya sahip uygulama içerisindeki tüm methodları MyDelagate üzerinden çalıştırabiliriz.
  // Uygulama bir durum oluştuğunda bu duruma ait methodunun tetiklenmesi ve methoda ilgili parametrelerin geçilmesini sağlar. Yani Event Driven Programlamlamayı destek. Olay tabanlı program geliştirme teknikleri ile yazılan uygulamalar sıklıkla kullanılır.
  // 1000 request üstüne çıkılınca, Yeni bir web sunucunu ayağa kaldıracak bir delegate tanımı yaparak. Operasyonun Handle edilmesi sağlanabilir.
  // ürün fiyatı güncellendiğinde, fiyatı güncellenen ürün ile ilgili ürün takip eden müşterilere mail atılabilir.
  public static void DelegateSample()
  {
    MyDelegate m1 = M1; // delegate M1 tanımlası yaptık
    MyDelegate m2 = M2;

    // 1. çağırma şekli
    m1("Mesaj1"); // delegate üzerinden methodu çalıştırıyoruz
    m2("Mesaj2");

    // 2. çağırma şekli
    m1.Invoke("Mesaj3");
  }

  public static void M1(string message)
  {
    Console.WriteLine(message);
  }
  public static void M2(string message)
  {
    Console.WriteLine(message);
  }



  public static void RegexSample02()
  {
    // kullanıcı console bir cümle yazsın. bizde kaç adet kelime olduğunu ekrana çıktı olarak verelim. C# yazısını ise ekrana çıktı olarak replace edip CSharp şeklinde yazsın.

    Console.WriteLine("içerisinde C# geçen bir cümle giriniz");
    string inputText = Console.ReadLine();

    string wordPattern = @"\b\w+\b"; // kelime bulma regex

    string replacedText = Regex.Replace(inputText, "C#", "CSharp");

    MatchCollection matchCollection = Regex.Matches(replacedText, wordPattern);

    foreach (Match match in matchCollection)
    {
      Console.WriteLine($"Kelime : {match.Value}");
    }

    Console.WriteLine($"Kelime Sayısı :{matchCollection.Count()}");

    Console.WriteLine(string.Format("Replaced Text, {0}", replacedText));

  }
  public static void RegexSample()
  {
    // Regular Expression Düzenli İfadeler olarak geçer. Metinsel bir ifade içinde arama, değer değiştirme gibi işlemlerde sıklıkla tercih edilir. metinsel bir patternde yazılır. isMatch, Replace, Matches gibi methodlar ile geliştiriye kolaylık sağlar.

    string pattern = @"^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$";

    Console.WriteLine("Güvenli bir paralo giriniz");

    string pass = Console.ReadLine();

    // Regex sayesinde metinsel bir ifadenin validasyonu yapabiliriz.
    bool isMatch = Regex.IsMatch(pass, pattern);

    Console.WriteLine(isMatch ? "Parola güvenli" : "Parola Güvenli değil");
  }
  public static void DateTimeExtensionSample()
  {
    Console.WriteLine("Doğum yılınızı Tarih ve saat formatında giriniz");
    DateTime dT = DateTime.Parse(Console.ReadLine());

    Console.WriteLine($"Tarih: {dT.ToShortDate()} Saat: {dT.ToShortTime()}" );


  }
  public static void PhoneExtensionsSample()
  {
    "Ali".Contains('a');

    #region ExtensionSample

    Console.WriteLine("GSM Numarası giriniz");
    string gsmNumber = Console.ReadLine();

    Console.WriteLine("Ülke Kodu Giriniz");
    string areaCode = Console.ReadLine();

    // 2. kullanım
    // StringExtensions.PhoneFormatted(gsmNumber, areaCode);

    // Date => (10.10.1990 13:15) => 10-10-1990 ShortDate ShortTime 13:15

    new DateTime().ToShortDateString(); // string formata sahip olucak.

    Console.WriteLine(gsmNumber.PhoneFormatted(areaCode));
    Console.WriteLine(" ali ".CapitalizedName());

   

    #endregion
  }
  public static void PersonClassExtensionSample()
  {
    // sınıfın içerisinde sınıfa müdahele etmeden dışından bir extend işlemi yaptım.
    var p = new Person();
    p.Name = "Ali";
    p.SurName = "Tan";
    string fullName = p.GetFullName();
  }
}










