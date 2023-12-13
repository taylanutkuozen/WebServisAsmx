using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webServisOrnekUygulama.Entities;
namespace webServisOrnekUygulama.Context
{
    public class databaseLogicLayer:IDisposable
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataReader reader;
        int returnValueInt;
        outputType returnValueOutputType;
        public databaseLogicLayer()
        {
            sqlConnection = new SqlConnection(@"Data Source=;Initial Catalog=webServisKullanimi;user id=;password=");
        }
        public void baglantiIslemleri()
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            else
                sqlConnection.Close();
        }
        public outputType urunKayitYeni(Urun u)
        {
            sqlCommand = new SqlCommand("insert into urun values(@urunKimlik,@tanim,@aciklama,@stokAdet)", sqlConnection);
            sqlCommand.Parameters.Add("@urunKimlik", SqlDbType.NVarChar).Value = u.urunKimlik;
            sqlCommand.Parameters.Add("@tanim", SqlDbType.NVarChar).Value = u.tanim;
            sqlCommand.Parameters.Add("@aciklama", SqlDbType.NVarChar).Value = u.aciklama;
            sqlCommand.Parameters.Add("@stokAdet", SqlDbType.Int).Value = u.stokAdet;
            baglantiIslemleri();
            returnValueOutputType = sqlCommand.ExecuteNonQuery() > 0 ? outputType.islemBasarili : outputType.islemBasarisiz;
            baglantiIslemleri();
            return returnValueOutputType;
        }
        public outputType urunKimlikKontrol(string urunKimlik)
        {
            sqlCommand = new SqlCommand("select count(1) from urun where urunKimlik=@urunKimlik", sqlConnection);
            sqlCommand.Parameters.Add("@urunKimlik", SqlDbType.NVarChar).Value = urunKimlik;
            baglantiIslemleri();
            returnValueOutputType = ((int)sqlCommand.ExecuteScalar() > 0) ? outputType.kayitliUrun : outputType.yeniUrun; //ExecuteScalar obje döner o yüzden int kullanarak boxing-unboxing işlemi yaptık.
            baglantiIslemleri();
            return returnValueOutputType;
        }
        public outputType urunDuzenle(Urun u)
        {
            sqlCommand = new SqlCommand("update urun set tanim=@tanim,aciklama=@aciklama,stokAdet=@stokAdet where urunKimlik=@urunKimlik)", sqlConnection);
            sqlCommand.Parameters.Add("@urunKimlik", SqlDbType.NVarChar).Value = u.urunKimlik;
            sqlCommand.Parameters.Add("@tanim", SqlDbType.NVarChar).Value = u.tanim;
            sqlCommand.Parameters.Add("@aciklama", SqlDbType.NVarChar).Value = u.aciklama;
            sqlCommand.Parameters.Add("@stokAdet", SqlDbType.Int).Value = u.stokAdet;
            baglantiIslemleri();
            returnValueOutputType = sqlCommand.ExecuteNonQuery() > 0 ? outputType.islemBasarili : outputType.islemBasarisiz;
            baglantiIslemleri();
            return returnValueOutputType;
        }
        public outputType urunKayitSil(string urunKimlik)
        {
            sqlCommand = new SqlCommand("delete from urun where urunKimlik=@urunKimlik", sqlConnection);
            sqlCommand.Parameters.Add("@urunKimlik", SqlDbType.NVarChar).Value = urunKimlik;
            baglantiIslemleri();
            returnValueOutputType = sqlCommand.ExecuteNonQuery() > 0 ? outputType.islemBasarili : outputType.islemBasarisiz;
            baglantiIslemleri();
            return returnValueOutputType;
        }
        public List<Urun> urunKayitListe()
            {
            List<Urun> urunListe = new List<Urun>();
            sqlCommand = new SqlCommand("select * from urun", sqlConnection);
            reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    urunListe.Add(new Urun()
                    {
                        Id = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                        urunKimlik =reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        tanim=reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        aciklama=reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        stokAdet=reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                    });
                }
            reader.Close();
            baglantiIslemleri();
            return urunListe;
            }
        public Urun urunKayitListe(string urunKimlik)
        {
            Urun bulunanUrun=null;
            sqlCommand = new SqlCommand("select * from urun where urunKimlik=@urunKimlik", sqlConnection);
            sqlCommand.Parameters.Add("urunKimlik",SqlDbType.NVarChar).Value=urunKimlik;
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                bulunanUrun=new Urun()
                {
                    Id = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                    urunKimlik = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    tanim = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    aciklama = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    stokAdet = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                };
            }
            reader.Close();
            baglantiIslemleri();
            return bulunanUrun;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}