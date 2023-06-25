using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var urunler = db.TblUrunler.ToList();//Burada Verilerimizi Entity ile Listeliyoruz.
            return View(urunler);
        }

        [HttpGet]//(Sayfa İlk Yüklendiğinde)Sayfada Butona tıklamazsam vb. işleri yapmazsam, aynı işlem yapılmamış kayıt eklenmemiş sayfayı geri bana döndürür !
        public ActionResult YeniUrun()
        {
            List<SelectListItem> yeniurun = (from i in db.TblKategoriler.ToList()//SelectListItem bir nevi seç demek Datagridviewde ki selectedcell vb.
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,//Text ve Value Combobox'ta ki ValueMember ve DisplayMember ile aynı işlevde.
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = yeniurun;//Listeyi bu kod ile diğer sayfaya taşıdık.
            return View();
        }

        [HttpPost]//Sadece Butona Basıldığın da Alttaki İşlemi Gerçekleştirmemize Yarar(Örneğin KAYIT EKLE vb.) !
        public ActionResult YeniUrun(TblUrunler u1)
        {
            var ktgr = db.TblKategoriler.Where(m => m.KATEGORIID == u1.TblKategoriler.KATEGORIID).FirstOrDefault();
            u1.TblKategoriler = ktgr;
            db.TblUrunler.Add(u1);
            db.SaveChanges();
            return RedirectToAction("Index");//Ürün Kaydetme İşlemi Bittikten Sonra Bizi Index Sayfasına Geri Yönlendirdik.
        }

        public ActionResult SİL(int id)//ÜRÜN SİLME İŞLEMİ YAPTIK
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");//
        }

        public ActionResult UrunGetir(int id)//Kategorileri DropDownList ile Getirdik !
        {
            var urun = db.TblUrunler.Find(id);
            List<SelectListItem> yeniurun = (from i in db.TblKategoriler.ToList()//SelectListItem bir nevi seç demek Datagridviewde ki selectedcell vb.
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,//Text ve Value Combobox'ta ki ValueMember ve DisplayMember ile aynı işlevde.
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = yeniurun;//Listeyi bu işlem ile diğer sayfaya taşıdık.Verileri ÜrünGeti sayfasına taşıdık güncelleme yapabilmek için.
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TblUrunler u1)
        {
            var urn = db.TblUrunler.Find(u1.URUNID);
            urn.URUNAD = u1.URUNAD;
            urn.MARKA = u1.MARKA;
            urn.STOK = u1.STOK;
            //urn.URUNKATEGORI = u1.URUNKATEGORI; --> Allta ki kod Satırıyla UrunKategoriyi de güncelledik !
            var ktgr = db.TblKategoriler.Where(m => m.KATEGORIID == u1.TblKategoriler.KATEGORIID).FirstOrDefault();
            u1.URUNKATEGORI = ktgr.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}