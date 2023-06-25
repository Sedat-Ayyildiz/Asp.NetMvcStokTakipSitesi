using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string m)
        {
            //var musteriler = db.TblMusteriler.ToList();//Burada Verilerimizi Entity ile Listeliyoruz.
            //return View(musteriler);
            var musteriler = from d in db.TblMusteriler select d;
            if (!string.IsNullOrEmpty(m))//Textbox boş değilse bu kod satırını uygula !
            {
                musteriler = musteriler.Where(k => k.MUSTERIAD.Contains(m));
            }
            return View(musteriler.ToList());
        }

        [HttpGet]//(Sayfa İlk Yüklendiğinde)Sayfada Butona tıklamazsam vb. işleri yapmazsam, aynı işlem yapılmamış kayıt eklenmemiş sayfayı geri bana döndürür !
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]//Sadece Butona Basıldığın da Alttaki İşlemi Gerçekleştirmemize Yarar(Örneğin KAYIT EKLE vb.) !
        public ActionResult YeniMusteri(TblMusteriler m1)
        {
            if (!ModelState.IsValid)//Model Doğrulanmadıysa Çalıştır
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(m1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SİL(int id)//MÜŞTERİ SİLME İŞLEMİ YAPTIK
        {
            var musteri = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)//MÜSTERİGÜNCELLE SAYFAMIZA VERİ(ID) TAŞIYIP VİEW YAPARAK ID'leri GÖRÜNTÜLEDİK.
        {
            var mus = db.TblMusteriler.Find(id);
            return View("MusteriGetir", mus);
        }

        //MÜŞTERİ GÜNCELLEME İŞLEMİ
        public ActionResult Guncelle(TblMusteriler m1)//MusteriGetir'de ki BeginForm da tanımladığımız isme göre methodu oluşturuyoruz DİKKAT !!!
        {
            var mstr = db.TblMusteriler.Find(m1.MUSTERIID);
            mstr.MUSTERIAD = m1.MUSTERIAD;
            mstr.MUSTERISOYAD = m1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");//İşlem bittikten sonra otomatik döneceğimiz sayfa.
        }
    }
}