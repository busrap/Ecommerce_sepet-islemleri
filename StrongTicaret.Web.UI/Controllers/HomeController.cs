using StrongTicaret.Web.UI.App_Classes;
using StrongTicaret.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static StrongTicaret.Web.UI.App_Classes.Sepet;


namespace StrongTicaret.Web.UI.Controllers
{
    [System.Runtime.InteropServices.Guid("F8C09183-019F-4871-9E78-2A654EB30943")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Sepet()
        {
            return PartialView();
        }

        public void SepeteEkle(int id)
        {
            //MiniSepetWidget view sayfasında ekleme isi
            SepetItem si = new SepetItem();
            Urun u = Context.Baglanti.Uruns.FirstOrDefault(x => x.Id == id);

            si.Urn = u;
            si.Adet = 1;
            si.Indirim = 0;

            Sepet spt = new Sepet();
            spt.SepeteEkle(si);
        }

        public PartialViewResult MiniSepetWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            else
                return PartialView();
        }
        
        public void SepetUrunAdetDusur(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];

                if (s.Urunler.FirstOrDefault(x => x.Urn.Id == id).Adet > 1)

                {
                    s.Urunler.FirstOrDefault(x => x.Urn.Id == id).Adet--;
                }
                    
                else
                {
                    SepetItem si = s.Urunler.FirstOrDefault(x => x.Urn.Id == id);
                    s.Urunler.Remove(si);
                    
                }

            }
            
              
        }

        public PartialViewResult Slider() 
        {
            var dataImage = Context.Baglanti.Resims.Where(x => x.BuyukYol.Contains("Slider")).ToList();
            return PartialView(dataImage);
        }
        public PartialViewResult YeniUrunler()
        {
           var dataUrun= Context.Baglanti.Uruns.ToList();
            return PartialView(dataUrun);
        }

        public PartialViewResult Servisler()
        {
          
            
            return PartialView();
        }
       
        public PartialViewResult Markalar()
        {
            var data = Context.Baglanti.Markas.ToList();
            return PartialView(data);

        }
        [HttpPost]
        public ActionResult SliderResimEkle (HttpPostedFileBase fileUpload)
        {
            return View();
        }
        public ActionResult UrunDetay(string id)
        {
            var u = Context.Baglanti.Uruns.FirstOrDefault(x => x.Adi == id);
            
            return View(u);
        }
        public ActionResult Kategoriler()
        {
            var data = Context.Baglanti.Kategoris.ToList();
            return View(data);
        }




    }
}