using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace webServisOrnekUygulama.GuvenlikIslemleri
{
    public static class SHA
    {
        public static string sha256Olustur(string sifrelenecekDeger)
        {
            SHA256 sha256= SHA256Managed.Create();
            byte[] bytedizi=Encoding.UTF8.GetBytes(sifrelenecekDeger);
            byte[] sifredizi=sha256.ComputeHash(bytedizi);
            string hashString = FormatDuzenle(sifredizi);
            return hashString;
        }
        private static string FormatDuzenle(byte[] hash)
        {
            StringBuilder sonuc=new StringBuilder();
            for(int i=0;i<hash.Length; i++)
            {
                sonuc.Append(hash[i].ToString("X2"));
            }
            return sonuc.ToString();
        }
    }
}