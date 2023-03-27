using System;
using System.Collections.Generic;

// Hesap sınıfı, hesap bilgilerini içeren özelliklere sahiptir.
public class Hesap
{
    public int HesapNo { get; set; }
    public string Ad { get; set; }
    public decimal Bakiye { get; set; }
}

// HesapEnvanteri sınıfı, hesapları depolayan bir sözlük içerir ve para yatırma, para çekme ve hesap sorgulama işlevleri sağlar.
public class HesapEnvanteri
{
    private Dictionary<int, Hesap> _hesaplar = new Dictionary<int, Hesap>();
    public int HesapSayisi { get; internal set; }
    
    // Yeni bir hesap eklemek için kullanılır.
    public void HesapAc(Hesap hesap)
    {
        _hesaplar.Add(123456, hesap);
    }

    // Belirli bir hesaba para yatırmak için kullanılır.
    public void ParaYatir(int hesapNo, decimal miktar)
    {
        _hesaplar[hesapNo].Bakiye += miktar;
    }

    // Belirli bir hesaptan para çekmek için kullanılır.
    public void ParaCek(int hesapNo, decimal miktar)
    {
        _hesaplar[hesapNo].Bakiye -= miktar;
    }

    // Belirli bir hesabın bilgilerini getirmek için kullanılır.
    public Hesap HesapGetir(int hesapNo)
    {
        return _hesaplar[hesapNo];
    }
}

// HesapAcmaKomutu sınıfı, yeni bir hesap açmak için kullanılan özelliklere sahiptir.
public class HesapAcmaKomutu
{
    public string Ad { get; set; }
    public decimal Bakiye { get; set; }
    public int HesapNo { get; internal set; }
    public int BaslangicBakiyesi { get; internal set; }
}

// ParaYatirmaKomutu sınıfı, belirli bir hesaba para yatırmak için kullanılan özelliklere sahiptir.
public class ParaYatirmaKomutu
{
    public int HesapNo { get; set; }
    public decimal Miktar { get; set; }
}

// ParaCekmeKomutu sınıfı, belirli bir hesaptan para çekmek için kullanılan özelliklere sahiptir.
public class ParaCekmeKomutu
{
    public int HesapNo { get; set; }
    public decimal Miktar { get; set; }
}

// HesapSorgu sınıfı, belirli bir hesap hakkında sorgulama yapmak için kullanılan özelliklere sahiptir.
public class HesapSorgu
{
    public int HesapNo { get; set; }
}

// HesapSorguSonucu sınıfı, belirli bir hesap hakkında sorgulama sonucunda döndürülen bilgil
public class HesapSorguSonucu
{
    public string Ad { get; set; }  // Hesap sahibinin adı
    public decimal Bakiye { get; set; } // Hesap bakiyesi
 }   
    public class KomutIsleyicisi
  {
 private HesapEnvanteri _hesapEnvanteri;

 // KomutIsleyicisi sınıfının yapıcı metodunda HesapEnvanteri nesnesi alınır
 // ve _hesapEnvanteri değişkenine atanır.
 public KomutIsleyicisi(HesapEnvanteri hesapEnvanteri)
 {
  _hesapEnvanteri = hesapEnvanteri;
 }

 // HesapAcmaKomutu işlendiğinde çalışacak metot.
 public void Handle(HesapAcmaKomutu komut)
 {
  // HesapAcmaKomutu'ndan gelen bilgiler kullanılarak yeni bir Hesap nesnesi oluşturulur.
  Hesap hesap = new Hesap
  {
   HesapNo = _hesapEnvanteri.HesapSayisi + 1,
   Ad = komut.Ad,
   Bakiye = komut.BaslangicBakiyesi
  };

  // HesapEnvanteri nesnesi üzerinden hesap açılır.
  _hesapEnvanteri.HesapAc(hesap);
 }

 // ParaYatirmaKomutu işlendiğinde çalışacak metot.
 public void Handle(ParaYatirmaKomutu komut)
 {
  // HesapEnvanteri nesnesi üzerinden para yatırma işlemi yapılır.
  _hesapEnvanteri.ParaYatir(komut.HesapNo, komut.Miktar);
 }

 // ParaCekmeKomutu işlendiğinde çalışacak metot.
 public void Handle(ParaCekmeKomutu komut)
 {
  // HesapEnvanteri nesnesi üzerinden para çekme işlemi yapılır.
  _hesapEnvanteri.ParaCek(komut.HesapNo, komut.Miktar);
 }
}

public class HesapSorguIsleyici
{
 private HesapEnvanteri _hesapEnvanteri;

 // HesapSorguIsleyici sınıfının yapıcı metodunda HesapEnvanteri nesnesi alınır
 // ve _hesapEnvanteri değişkenine atanır.
 public HesapSorguIsleyici(HesapEnvanteri hesapEnvanteri)
 {
  _hesapEnvanteri = hesapEnvanteri;
 }

 // HesapSorgu işlendiğinde çalışacak metot.
 public HesapSorguSonucu Handle(HesapSorgu sorgu)
 {
  // HesapEnvanteri nesnesi üzerinden istenilen hesap bilgisi getirilir.
  Hesap hesap = _hesapEnvanteri.HesapGetir(sorgu.HesapNo);

  // HesapSorguSonucu nesnesi oluşturularak hesap bilgileri doldurulur ve geri döndürülür.
  return new HesapSorguSonucu
  {
   Ad = hesap.Ad,
   Bakiye = hesap.Bakiye
  };
 }
}
class Program
 {
static void Main(string[] args)
{
    // Hesap envanteri oluşturuluyor.
    HesapEnvanteri hesapEnvanteri = new HesapEnvanteri();

    // Komut işleyicisi oluşturuluyor ve hesap envanteri ile birlikte kullanılıyor.
    KomutIsleyicisi komutIsleyicisi = new KomutIsleyicisi(hesapEnvanteri);

    // Hesap sorgu işleyicisi oluşturuluyor ve hesap envanteri ile birlikte kullanılıyor.
    HesapSorguIsleyici hesapSorguIsleyici = new HesapSorguIsleyici(hesapEnvanteri);

    // Yeni bir hesap açma komutu oluşturuluyor ve komut işleyicisi kullanılarak işleniyor.
    var hesapAcmaKomutu = new HesapAcmaKomutu
    {
        HesapNo = 123456,
        Ad = "Ali",
        BaslangicBakiyesi = 800
    };
    komutIsleyicisi.Handle(hesapAcmaKomutu);

    // Para yatırma komutu oluşturuluyor ve komut işleyicisi kullanılarak işleniyor.
    var paraYatirmaKomutu = new ParaYatirmaKomutu
    {
        HesapNo = 123456,
        Miktar = 250
    };
    komutIsleyicisi.Handle(paraYatirmaKomutu);

    // Para çekme komutu oluşturuluyor ve komut işleyicisi kullanılarak işleniyor.
    var paraCekmeKomutu = new ParaCekmeKomutu
    {
        HesapNo = 123456,
        Miktar = 350
    };
    komutIsleyicisi.Handle(paraCekmeKomutu);

    // Hesap sorgusu oluşturuluyor ve hesap sorgu işleyicisi kullanılarak işleniyor.
    var hesapSorgu = new HesapSorgu
    {
        HesapNo = 123456
    };
    var hesapSorguSonucu = hesapSorguIsleyici.Handle(hesapSorgu);

    // Hesap sorgu sonucu ekrana yazdırılıyor.
    Console.WriteLine("Hesap adı: " + hesapSorguSonucu.Ad);
    Console.WriteLine("Hesap Bakiyesi: " + hesapSorguSonucu.Bakiye);
}
