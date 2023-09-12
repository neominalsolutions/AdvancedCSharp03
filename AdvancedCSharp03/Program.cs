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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using System.Text.Json;
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
    // MediatorWeatherSample();
    // StreamReaderWriterSample();
    // FileStreamReaderWriterSample();
    // SystemTextJsonSample();
    NewtonSoftJSONSample();
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
    var p = new Arthitect.Common.Person();
    p.Name = "Ali";
    p.SurName = "Tan";
    string fullName = p.GetFullName();
  }


  // Stream => Dosya okuma yazma işlemleri için geliştirilmiş. Boyutu fazla olan işlemlerde veri akışı sağlayan, ve performanslı okuma yazma işlemleri yapmamıza olanak sağlayan bir tip. Stream base type gelirler, FileStrem FileStreamReader, FileStremWriter, Metin dosyası işlemleri için StreamWriter StreamReader. UnManagement Resource olması sebebi ile using kod blogu kullanılması gerekir.
  // 

  public static void StreamReaderWriterSample()
  {
    try
    {
      // append dosyadakilerin üzerine yeni satıralar ekleyerek ilerlemeyi sağlar.
      // dosya dizinde dosya olmasa dahil dosyayı oluşturup dosya üzerinden yazma işlemi yapabiliyor.
      // IDisposable interfaceden implemente olduğundan dolayı yönetilemeyen bir kaynaktır bu sebeple using blogu içerisinde yazılmalıdır.
      using (StreamWriter writer  = new StreamWriter("metinDosyasi.txt",append:false))
      {
        writer.WriteLine(" bu bir örnektir");
        writer.WriteLine(" ikinci satır");

        writer.Close(); // dosyayı kapptık.
      }

    }
    catch (Exception)
    {

      throw;
    }

    using (StreamReader reader = new StreamReader("metinDosyasi.txt"))
    {

      string data = reader.ReadToEnd();

      //while (reader.Read() > 0) // reader read edebilecek bir satırı varsa döngü devam eder.
      //{
      //  Console.WriteLine(" "  + reader.ReadLine());
      //}

      reader.Close(); // reader ile dosyanın kapatılmasını sağladık. Close yapmazsak ramde memory leak hataları meydana gelebilir. 
    }
  }

  // bütün dosya formatları ile çalır. Metinsel ifade dışında binary Data olarak kullanır. PDF dosyası, Excel Dosyası, Bin uzantılı bir dosya
  public static void FileStreamReaderWriterSample()
  {

    try
    {
      using (FileStream fileStream = new FileStream("binaryDosya.bin", FileMode.Create, FileAccess.Write))
      {
        byte[] data = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f }; // hello

        fileStream.Write(data, 0, data.Length);

        fileStream.Close();
      }
    }
    catch (Exception)
    {

      throw;
    }


    using (FileStream fileStream = new FileStream("binaryDosya.bin",FileMode.Open,FileAccess.Read))
    {
      byte[] buffer = new byte[1024]; // 1024 bytelık dosyayı okumak için tampon bir alan belirledim.

      while (fileStream.Read(buffer,0, buffer.Length) > 0)
      {
        string data = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        Console.WriteLine("Okunan bin dosyası :" + data);

        fileStream.Close();
      }
    }
  }

  // Asp.Net Web API defaultda C# nesnelerini JSON veri formatına veya XML veri formatına serialize eder.
  // Net Core 3.1 versiyonu ve sonrasında System.Text.JSON geldi. Net 5.0 sonrasıdaki tüm projelerde kullanımı kesinle öneriliyor.
  // NewtonSoft.JSON bir paket kullanıyorduk.

  // C# Object => JSONString => Serialize
  // JSONString => C# Object => Deserialize işlemi yapıyoruz.

  public static void SystemTextJsonSample()
  {
    var person = new Arthitect.Common.Person();

    Console.WriteLine("Adınızı giriniz");
    person.Name = Console.ReadLine();

    Console.WriteLine("Soyadınızı giriniz");
    person.SurName = Console.ReadLine();

    var options = new JsonSerializerOptions
    {
      WriteIndented = true, // uzun json dosyalarının okunaklı olması için bu ayar true yapılır.
      DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, // defaultda null değerlerin ignore edilmesi
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      IgnoreReadOnlyFields = true // Class içerisinde read only alanları ignore eder serialize ve deserialize etmez.
    };
    // name, surname

    string jsonString = System.Text.Json.JsonSerializer.Serialize(person, options);

    Console.WriteLine("Serialize JSON" + jsonString);
    var p2 = System.Text.Json.JsonSerializer.Deserialize<Arthitect.Common. Person>(jsonString, options);

    Console.WriteLine("Deserialize" + p2.GetFullName());
  }

  public static void NewtonSoftJSONSample()
  {
    var p = new AdvancedCSharp03.Models.Person();

    var opts = new JsonSerializerSettings
    {
      ContractResolver = new CamelCasePropertyNamesContractResolver(), // camelCase
      DefaultValueHandling = DefaultValueHandling.Ignore, // default değerleri ignore et
      NullValueHandling = NullValueHandling.Ignore, // Null değerler dışarı çıkmasın,
      Formatting = Formatting.Indented, // Uzun json dosyalarında okunaklılığı artırmak için kullandılan bir özellik.
      DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

    };

    Console.WriteLine("Adınızı giriniz");
    p.Name = Console.ReadLine();
    p.Age = 18; // değer verdik.
    Console.WriteLine("Soyadınızı giriniz");
    p.SurName = Console.ReadLine();

    p.BirthDate = DateTime.Now.AddYears(-30);


    string json = JsonConvert.SerializeObject(p, opts);

    Console.WriteLine("NewtonsoftJSON " + json);

    AdvancedCSharp03.Models.Person p2 = JsonConvert.DeserializeObject<AdvancedCSharp03.Models.Person>(json, opts);

    Console.WriteLine("Deserialize " + p2.GetFullName());

  }



}










