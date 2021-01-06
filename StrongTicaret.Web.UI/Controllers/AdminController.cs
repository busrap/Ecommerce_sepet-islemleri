using StrongTicaret.Web.UI.App_Classes;
using StrongTicaret.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StrongTicaret.Web.UI.Controllers
{
    [System.Runtime.InteropServices.Guid("C336E0AB-A244-4776-A50B-FA1DC2CA0606")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler()
        {
            return View(Context.Baglanti.Uruns.ToList());
        }

        public ActionResult UrunEkle()
        {
            //Data'yı model'den çekeriz(view da) yada viewBag(dynamic alan ile)ile data gönderilir.
            ViewBag.kategoriler = Context.Baglanti.Kategoris.ToList();
            ViewBag.markalar = Context.Baglanti.Markas.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun urn)
        {
            Context.Baglanti.Uruns.Add(urn);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Urunler");
        }

        public ActionResult UrunResimEkle(int id)
        {

            return View(id);
        }
        [HttpPost]
        public ActionResult UrunResimEkle(int uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap ortaResimBitMap = new Bitmap(img, Settings.UrunOrtaBoyut);
                Bitmap buyukResimBitMap = new Bitmap(img, Settings.UrunBuyukBoyut);

                string ortaName = "/Templates/UrunImage/OrtaYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string buyukName = "/Templates/UrunImage/BuyukYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                //İmages'ı belirtilen adreste bulunan klasöre kayıt etme işlemi
                ortaResimBitMap.Save(Server.MapPath(ortaName));
                buyukResimBitMap.Save(Server.MapPath(buyukName));


                Resim rsm = new Resim();
                rsm.OrtaYol = ortaName;
                rsm.BuyukYol = buyukName;
                rsm.UrunID = uId;


                if (Context.Baglanti.Resims.FirstOrDefault(x => x.UrunID == uId && x.Varsayilan == false) != null)
                {
                    rsm.Varsayilan = true;
                }
                else
                {
                    rsm.Varsayilan = false;
                }

                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();
                TempData["msg"] = "<script>alert('Resim Ekleme işlemi başarılı.');</script>";


                return View(uId);
            }
            return View(uId);
        }
        public ActionResult Markalar()
        {
            return View(Context.Baglanti.Markas.ToList());
        }
        public ActionResult MarkaEkle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult MarkaEkle(Marka mrk, HttpPostedFileBase fileUpload)
        {
            int resimId = -1;


            if (fileUpload != null)
            {

                Image img = Image.FromStream(fileUpload.InputStream);

                int width = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaWidth"].ToString());

                int height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaHeight"].ToString());

                Bitmap bm = new Bitmap(img, width, height);

                string name = "/Templates/MarkaImage/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                bm.Save(Server.MapPath(name));

                Resim rsm = new Resim();
                rsm.OrtaYol = name;
                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();

                if (rsm.Id != null)

                    resimId = rsm.Id;




            }
            if (resimId != 0)
                mrk.ResimID = resimId;


            Context.Baglanti.Markas.Add(mrk);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");



        }

        public ActionResult MarkaSil(int markId)
        {
            Resim rsmRmove = Context.Baglanti.Resims.FirstOrDefault(x => x.Id == markId);
            Context.Baglanti.Resims.Remove(rsmRmove);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");
        }

        public ActionResult Kategoriler()
        {
            return View(Context.Baglanti.Kategoris.ToList());
        }
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori ktg)
        {
            Context.Baglanti.Kategoris.Add(ktg);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("Kategoriler");

        }

        public ActionResult OzellikTipleri()
        {
            return View(Context.Baglanti.OzellikTips.ToList());
        }

        public ActionResult OzellikTipEkle()
        {
            return View(Context.Baglanti.Kategoris.ToList());
        }

        [HttpPost]
        public ActionResult OzellikTipEkle(OzellikTip ot)
        {
            Context.Baglanti.OzellikTips.Add(ot);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("OzellikTipleri");

        }

        public ActionResult OzellikDegerleri()
        {
            return View(Context.Baglanti.OzellikDegers.ToList());
        }

        public ActionResult OzellikDegerEkle()
        {
            //OzellikDegerEkle'me işlemi view'da ozellikTip listeleme işlemi(select input için)
            return View(Context.Baglanti.OzellikTips.ToList());

        }

        [HttpPost]
        public ActionResult OzellikDegerEkle(OzellikDeger od)
        {
            Context.Baglanti.OzellikDegers.Add(od);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikDegerleri");
        }

        public ActionResult UrunOzellikleri()
        {
            return View(Context.Baglanti.UrunOzelliks.ToList());
        }
        [HttpPost]
        public ActionResult UrunOzellikEkle(UrunOzellik uo)
        {
            Context.Baglanti.UrunOzelliks.Add(uo);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }
        public ActionResult UrunOzellikEkle()
        {


            return View(Context.Baglanti.Uruns.ToList());
        }

        public ActionResult UrunOzellikSil(int urunId, int tipId, int degerId)
        {
            UrunOzellik oz;
            oz = Context.Baglanti.UrunOzelliks.FirstOrDefault(x => x.UrunID == urunId && x.OzellikTipID == tipId && x.OzellikDegerID == degerId);
            Context.Baglanti.UrunOzelliks.Remove(oz);
            Context.Baglanti.SaveChanges();

            return RedirectToAction("UrunOzellikleri");

        }

        public PartialViewResult UrunOzellikTipWidget(int? katId)
        {
            if (katId != null)
            {
                var dataList = Context.Baglanti.OzellikTips.Where(x => x.KategoriID == katId).ToList();
                return PartialView(dataList);

            }
            else
            {
                var dataList = Context.Baglanti.OzellikTips.ToList();
                return PartialView(dataList);
            }
        }
        public PartialViewResult UrunOzellikDegerWidget(int? tipId)
        {
            if (tipId != null)
            {
                var dataList = Context.Baglanti.OzellikDegers.Where(x => x.OzellikTipID == tipId).ToList();
                return PartialView(dataList);
            }
            else
            {
                var dataList = Context.Baglanti.OzellikDegers.ToList();
                return PartialView(dataList);


            }
        }

        #region StringBuilder kullanımı
        //public string UrunOzellikListele(object list)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    List<OzellikTip> ot = (List<OzellikTip>)list;
        //    sb.Append("  <div class='form - group'>< label class='col-lg-2 control-label'>Özellik: </label> <div class='col-lg-10'><select class='form-control input-sm m-bot15' name='Id' id='urunSelect'>");

        //    foreach (OzellikTip item in ot)
        //    {
        //        sb.Append("<option value="+item.Id+">"+item.Adi+"</option>");
        //    }
        //    sb.Append("</select></ div ></ div >");

        //    return sb.ToString();


        //}
        #endregion
        public ActionResult SliderResimleri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SliderResimEkle(HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap bmp = new Bitmap(img, Settings.SliderBoyut);

                string yol = ("/Templates/SliderImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName));
                bmp.Save(Server.MapPath(yol));

                Resim rsm = new Resim();
                rsm.BuyukYol = yol;
                Context.Baglanti.Resims.Add(rsm);
                Context.Baglanti.SaveChanges();
                TempData["msg"] = "<script>alert('Resim Ekleme işlemi başarılı.');</script>";
            }
            return RedirectToAction("SliderResimleri");
        }
    }
}