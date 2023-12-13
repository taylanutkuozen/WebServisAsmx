using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using webServisOrnekUygulama.Context;
using webServisOrnekUygulama.Entities;
using webServisOrnekUygulama.GuvenlikIslemleri;
namespace webServisOrnekUygulama
{
    /// <summary>
    /// Summary description for urunServis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class urunServis : System.Web.Services.WebService
    {
        //[WebMethod]
        //public outputType urunKayitYeni(Urun u,Guvenlik g)
        //{
        //    string sifrelenmisData = "F7038C9D-7D39-445C-AB4A-EA5B42C873B2" + g.privateValue.ToString() + g.tokenTime;
        //    if (g.clientKey==SHA.sha256Olustur(sifrelenmisData))//Sql server'dan select newid() komutunu çalıştırdık veya visual studio'da tools-create Guid diyoruz copy diyerek alıyoruz --//if(g.kullaniciAdi=="Demo"&&g.Password=="Demo")
        //    {
        //        using (databaseLogicLayer dal = new databaseLogicLayer())
        //        {
        //            if (dal.urunKimlikKontrol(u.urunKimlik) == outputType.yeniUrun)
        //            {
        //                return dal.urunKayitYeni(u);
        //            }
        //            else
        //            {
        //                return outputType.kayitliUrun;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return outputType.guvenlikHatasi;
        //    }
        //}
        public Guvenlik guvenlikSoapHeader;
        [WebMethod]
        [SoapHeader("guvenlikSoapHeader",Required =true)]
        public outputType urunKayitDuzenle(Urun u)
        {
            if (guvenlikSoapHeader.kontrol())
            {
                using (databaseLogicLayer dal = new databaseLogicLayer())
                {
                    if (dal.urunKimlikKontrol(u.urunKimlik) == outputType.kayitliUrun)
                        return dal.urunDuzenle(u);
                    else
                        return outputType.tanimsizUrun;
                }
            }
            else
            { 
                return outputType.guvenlikHatasi; 
            }
        }
        [WebMethod]
        public outputType urunSil(string urunKimlik)
        {
            using(databaseLogicLayer dal=new databaseLogicLayer())
            {
                if(dal.urunKimlikKontrol(urunKimlik)==outputType.kayitliUrun)
                {
                    return dal.urunKayitSil(urunKimlik);
                }
                else
                {
                    return outputType.tanimsizUrun;
                }
            }
        }
        [WebMethod]
        public List<Urun> urunTumListe()
        {
            using(databaseLogicLayer dal=new databaseLogicLayer())
            {
                return dal.urunKayitListe();
            }
        }
        [WebMethod]
        public Urun urunGetir(string urunKimlik)
        {
            if(guvenlikSoapHeader.kontrol())
            {
                using (databaseLogicLayer dal = new databaseLogicLayer())
                {
                    if (dal.urunKimlikKontrol(urunKimlik) == outputType.kayitliUrun)
                        return dal.urunKayitListe(urunKimlik);
                    else
                        return new Urun();
                }
            }
            else
            {
                return new Urun();
            }
        }
    }
}