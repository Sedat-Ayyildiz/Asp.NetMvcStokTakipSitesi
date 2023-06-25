using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();//VT'de ki Tablolara Ulaşmamız için
        public ActionResult Index(int sayfa = 1)
        {
            //1.YÖNTEM
            //var kategoriler = db.TblKategoriler.ToList();//Burada Verilerimizi Entity ile Listeliyoruz.

            //2.YÖNTEM
            //PagedList() içinde ki 1.parametre hangi sayfadan başlıcağını, 2.parametre ise 1 sayfada kaç adet olacağını ayarlar.
            var kategoriler = db.TblKategoriler.ToList().ToPagedList(sayfa, 10);
            return View(kategoriler);
        }

        [HttpGet]//(Sayfa İlk Yüklendiğinde)Sayfada Butona tıklamazsam vb. işleri yapmazsam, aynı işlem yapılmamış kayıt eklenmemiş sayfayı geri bana döndürür !
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]//Sadece Butona Basıldığın da Alttaki İşlemi Gerçekleştirmemize Yarar(Örneğin KAYIT EKLE vb. Butona Basana kadar Boş Kayıt Eklemeyi Engellemek için) !
        public ActionResult YeniKategori(TblKategoriler k1)
        {
            if (!ModelState.IsValid)//Model Doğrulanmadıysa Çalıştır(Bu if Döngüsü çalışmaz ise ValidationMessage çalışmaz !)
            {
                return View("YeniKategori");
            }
            db.TblKategoriler.Add(k1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SİL(int id)//KATEGORİ SİLME İŞLEMİNİ YAPTIK
        {
            var kategori = db.TblKategoriler.Find(id);
            db.TblKategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");//Sildikten sonra tekrar Kategori INDEX sayfasına yönlendirdik.
        }

        public ActionResult KategoriGetir(int id)//KATEGORİGÜNCELLE SAYFAMIZA VERİ(ID) TAŞIYIP VİEW YAPARAK ID'leri GÖRÜNTÜLEDİK
        {
            var ktgr = db.TblKategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }

        //KATEGORİ GÜNCELLEME İŞLEMİ
        public ActionResult Guncelle(TblKategoriler k1)//KategoriGetir'de ki BeginForm da tanımladığımız isme göre methodu oluşturuyoruz DİKKAT !!!
        {
            var ktg = db.TblKategoriler.Find(k1.KATEGORIID);
            ktg.KATEGORIAD = k1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");//İşlem bittikten sonra otomatik döneceğimiz sayfa.
        }
    }
}