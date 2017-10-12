using MVCOtel_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOtel_2.Controllers
{
    [Authorize(Roles = "Admin")] //Giriş yapanlardan Admin
    public class YonetimController : Controller
    {
        // GET: Yonetim

        [AllowAnonymous] //Herkese izin ver.
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SliderEkle() //Sayfayı göstermek için
        {
            return View();
        }

        [HttpPost]
        public ActionResult SliderEkle(SliderModel s, HttpPostedFileBase resim) //Eklemek için. Kayıt için. HttpPostedFileBase -> s id ve içindekileri tutar(Dosya hariç). resim yolunun ne old. bilmiyor. s'yi biliyor. 
        {
            if (resim !=null && resim.ContentLength > 0) //Dosyayı kaydetmek için
            {
                s.ResimYolu = resim.FileName;
                string yol = Server.MapPath("/Content/slider/");
                yol += resim.FileName;
                if (System.IO.File.Exists(yol))
                    yol.Replace(resim.FileName, Guid.NewGuid().ToString() + ".jpg");
                resim.SaveAs(yol);
            }

            if (ModelState.IsValid)
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.SliderModeller.Add(s);
                ctx.SaveChanges();
            }

            return View();
        }

        public ActionResult SliderListe()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.SliderModeller.ToList());
        }

        public ActionResult SliderSil(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var silinecek = ctx.SliderModeller.Find(id);
            ctx.SliderModeller.Remove(silinecek);
            ctx.SaveChanges();
            return RedirectToAction("SliderListe");
        }

        [HttpGet]
        public ActionResult OdaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OdaEkle(OdaModel o, HttpPostedFileBase odaResim)
        {
            if (odaResim !=null && odaResim.ContentLength > 0) //Resim kayıt işlemi
            {
                o.ResimURL = odaResim.FileName;
                string yol = Server.MapPath("/Content/oda/");
                odaResim.SaveAs(yol + odaResim.FileName);
            }

            if (ModelState.IsValid) //Oda kayıt işlemi
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.OdaModeller.Add(o);
                ctx.SaveChanges();
            }

            return View();
        }

        public ActionResult OdaListele()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.OdaModeller.ToList());
        }

        [HttpGet]
        public ActionResult OdaDuzenle(int? id) //RouteConfig den id kısmından geliyor.
        {

            if (id == null)
            {
                ViewBag.mesaj = "Bir oda seçmediniz, id bekleniyor.";
                return View();
            }
            else
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                var oda = ctx.OdaModeller.Find(id);
                return View(oda);
            }

        }

        [HttpPost]
        public ActionResult OdaDuzenle(OdaModel oda, HttpPostedFileBase resim)
        {
            var eskiOda = "";
            using (ApplicationDbContext ctx2 = new ApplicationDbContext())
            {
                eskiOda = ctx2.OdaModeller.Find(oda.OdaModelID).ResimURL;
            }
                
            ApplicationDbContext ctx = new ApplicationDbContext();
            
            var klasor = Server.MapPath("/Content/oda/");

            //Resim yüklenmişse
            if (resim !=null && resim.ContentLength > 0)
            {
                //Eski resim silinmeli
                if (string.IsNullOrEmpty(eskiOda))
                    System.IO.File.Delete(klasor + eskiOda);

                //Resim kayıt edilmeli
                resim.SaveAs(klasor + resim.FileName);
                //Modeldeki URL değiştirilmeli
                oda.ResimURL = resim.FileName;
            }

            //Resim yüklenMEmişse. (Yeni resim)
            else
            {
                oda.ResimURL = eskiOda;
            }

            if (ModelState.IsValid)
            {
                ctx.Entry(oda).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("OdaListele");
            }
            return View(oda); //Kullanıcı düzenleme yaptıktan sonra tekrar odayı görsün. oda değişkenini döndürdük.
        }

        public ActionResult OdaSil(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var silinecek = ctx.OdaModeller.Find(id);
            ctx.OdaModeller.Remove(silinecek);
            ctx.SaveChanges();
            return RedirectToAction("OdaListele");
        }

        [HttpGet]
        public ActionResult GaleriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GaleriEkle(GaleriModel g, HttpPostedFileBase galeriResim)
        {
            if(galeriResim !=null && galeriResim.ContentLength > 0)
            {
                g.ResimYoluGaleri = galeriResim.FileName;
                string yol = Server.MapPath("/Content/galeri/");
                galeriResim.SaveAs(yol + galeriResim.FileName);
            }

            if(ModelState.IsValid)
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.GaleriModeller.Add(g);
                ctx.SaveChanges();
            }
            return View(); //Hata varsa demiyoruz. Hata varsa Viewla aktarıyor.
        }

        public ActionResult GaleriListele()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.GaleriModeller.OrderBy(x => x.GaleriModelID).ToList());
        }

        public ActionResult GaleriSil(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var silinecek = ctx.GaleriModeller.Find(id);
            ctx.GaleriModeller.Remove(silinecek);
            ctx.SaveChanges();
            return RedirectToAction("GaleriListele");
        }
    }
}