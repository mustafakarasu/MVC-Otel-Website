using Microsoft.AspNet.Identity;
using MVCOtel_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOtel_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainSlider()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.SliderModeller.OrderBy(x=>x.Sira).ToList());
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult HeroAction()
        {
            return View();
        }

        public ActionResult OurTeam()
        {
            return View();
        }

        public ActionResult Testimonial()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.GaleriModeller.OrderBy(x => x.GaleriModelID).ToList());
        }

        public ActionResult Pricing()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.OdaModeller.OrderBy(x => x.Fiyat).ToList());
        }

        public ActionResult BusinessStats()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RezervasyonForm(int id)
        {
            RezervasyonModel r = new RezervasyonModel();
            r.OdaID = id;
            r.UserID = User.Identity.GetUserId();
            ViewBag.OdaFiyat = ctx.OdaModeller.Find(id).Fiyat;
            return View(r);
        }

        [HttpPost]
        public ActionResult RezervasyonForm(RezervasyonModel r)
        {
            var odafiyat = ctx.OdaModeller.Find(r.OdaID).Fiyat;
            r.Fiyat = odafiyat * r.KacKisi;

            if(ModelState.IsValid)
            {
                ctx.Rezervasyonlar.Add(r);
                ctx.SaveChanges();
            }
            return View();
        }

        ApplicationDbContext ctx = new ApplicationDbContext();
        [Authorize]
        public ActionResult Rezervasyon(int? id)
        {
            if (id == null)
                return RedirectToAction("OdaSec");

            //id null değil. Else gerek yok.
            var secilen = ctx.OdaModeller.Find(id);
            return View(secilen);
        }

        public string OdaSec()
        {
            return "Sayfaya id gelmediği için hangi odayı seçtiğinizi bilmiyorum. Geri dönüp tekrar deneyin.";
        }
    }
}