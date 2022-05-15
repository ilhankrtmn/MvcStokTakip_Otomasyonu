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
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(string p, int sayfa=1)
        {
            //var degerler = db.TblKategoriler.ToList();
            var degerler = from d in db.TblKategoriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.KategoriAd.Contains(p));
            }
            return View(degerler.ToList().ToPagedList(sayfa, 4));



            //var degerler = db.TblKategoriler.ToList().ToPagedList(sayfa,4);
            //return View(degerler);
            /* ToPagedList tarafında ilk değer kaçıncı sıradan başlasın
             * yani kaçıncı sayfadan gözükmeye başlasın oluyor.
             2.değer sayfalarda kaç tane değer olsun*/

        }

        /*
         * HttpPost sayfaya her hangi bir işlem yapıldığı zaman
         * mesela butona basıldığı zaman buradaki işlemi gerçekleştir
         * aksi durumda HttpGet kullanarak yazarsak; butona tıklamadığımızda
         * veya sayfada herhangi bir işlem yapmazsak sadece sayfayı geriye döndür.
         */
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TblKategoriler p1)
        {
            if (!ModelState.IsValid) //Kategori adı boşsa YeniKategori sayfasını döndür
            {
                return View();
            }
            db.TblKategoriler.Add(p1);
            db.SaveChanges();
            return View();
            /*Bu şekilde kullandığın zaman sayfayı her yenilediğinde 
            boş bir veri ekliyor. Bunu engellemek için HttpGet veya
            HttpPost kullanılır.*/
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.TblKategoriler.Find(id);
            db.TblKategoriler.Remove(kategori);//gelen değere göre kategori silme
            db.SaveChanges();//Değişiklikleri kaydetme
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TblKategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult Guncelle(TblKategoriler p1)
        {
            var ktgr = db.TblKategoriler.Find(p1.KategoriID);
            ktgr.KategoriAd = p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}