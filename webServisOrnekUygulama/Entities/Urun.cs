using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace webServisOrnekUygulama.Entities
{
    public class Urun
    {
        public int Id { get; set; }
        public string urunKimlik { get; set; }
        public string tanim { get; set;}
        public string aciklama { get; set; }
        public int stokAdet { get; set; }
    }
}