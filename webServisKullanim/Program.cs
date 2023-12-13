using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace webServisKullanim
{
    public class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            urunWebServis.Guvenlik webServisGuvenlik=new urunWebServis.Guvenlik();
            webServisGuvenlik.privateValue = rnd.Next(0, int.MaxValue); ;
            webServisGuvenlik.tokenTime = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
            webServisGuvenlik.clientKey = "F7038C9D-7D39-445C-AB4A-EA5B42C873B2" + webServisGuvenlik.privateValue + webServisGuvenlik.tokenTime;
            webServisGuvenlik.clientKey = SHA.sha256Olustur(webServisGuvenlik.clientKey);
            //1.Adım Yeni bir ürün ekleme işlemi
            urunWebServis.urunServis _webServis= new urunWebServis.urunServis();//İlk önce servis referansımızdan bir instance almamız gerekiyor.
            urunWebServis.outputType returnType = _webServis.urunKayitYeni(new urunWebServis.Urun()
            {
                tanim = "Laptop",
                aciklama = "Bilgisayar",
                stokAdet = 50,
                urunKimlik = "ASUS"
            }, webServisGuvenlik); /*kullaniciAdi="Demo15618", Password="Demo"*/
            Console.ReadLine();
        }
    }
}