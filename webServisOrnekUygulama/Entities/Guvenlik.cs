using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
namespace webServisOrnekUygulama.Entities
{
    public class Guvenlik:System.Web.Services.Protocols.SoapHeader
    {
        public string kullaniciAdi { get; set; }
        public string Password { get; set; }
        //public string clientKey { get; set; }
        //public string tokenTime { get; set; }
        //public int privateValue { get; set; }
        public bool kontrol()
        {
            if(kullaniciAdi == "Demo" && Password=="Demo")
                return true;
            else
                return false;
        }
    }
}