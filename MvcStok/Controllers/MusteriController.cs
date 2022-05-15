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
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p, int sayfa=1)
        {
            var degerler = from d in db.TblMusteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m=>m.Ad.Contains(p));
            }
            return View(degerler.ToList().ToPagedList(sayfa,4));
            //var degerler = db.TblMusteriler.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler p1)
        {
            if (!ModelState.IsValid) //Müşteri adı boşsa YeniMusteri sayfasını döndür
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult Sil(int id)
        {
            var mus = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(mus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TblMusteriler.Find(id);
            return View("MusteriGetir",mus);
            //musteriye göndericek oraya gönderirken bilgileride getiriyor.
        }

        public ActionResult Guncelle(TblMusteriler p1)
        {
            var mus = db.TblMusteriler.Find(p1.MusteriID);
            mus.Ad = p1.Ad;
            mus.Soyad = p1.Soyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}