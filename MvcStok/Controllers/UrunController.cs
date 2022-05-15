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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = from d in db.TblUrunler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.UrunAd.Contains(p));
            }
            return View(degerler.ToList().ToPagedList(sayfa, 7));

            /*
            var degerler = db.TblUrunler.ToList().ToPagedList(sayfa,5);
            return View(degerler);*/
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            /*Alttaki kısımda value değeri tutarken text onun adını listeliyor
            Yani biz adını seçicez ancak arka planda id değerini gönderecek.*/
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            //viewBag view tarafına taşımamızı sağlıyor.
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TblUrunler p1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriID == p1.TblKategoriler.KategoriID).FirstOrDefault();
            p1.TblKategoriler = ktg;
            db.TblUrunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");//İşlem tamamlandığında Index sayfasına göndericek.
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);//Find ile dosyayı buluyoruz.
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TblUrunler.Find(id);

            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            //viewBag view tarafına taşımamızı sağlıyor.
            ViewBag.dgr = degerler;
            return View("UrunGetir",urun);
        } 

        public ActionResult Guncelle(TblUrunler p1)
        {
            var urun = db.TblUrunler.Find(p1.UrunID);
            urun.UrunAd = p1.UrunAd;
            urun.Marka = p1.Marka;
            urun.Stok = p1.Stok;
            urun.Fiyat = p1.Fiyat;
            //urun.UrunKategori = p1.UrunKategori;
            var ktg = db.TblKategoriler.Where(m => m.KategoriID == p1.TblKategoriler.KategoriID).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}