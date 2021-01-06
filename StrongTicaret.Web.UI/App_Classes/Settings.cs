using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace StrongTicaret.Web.UI.App_Classes
{
    public class Settings



    //Class statik olmasa bile içerisinde ki proplar statik olabilir.Fakat class statikse içeriside bulunan nesneler statik olmak zorundadır
    //Statik calass dan instance alınamaz
    {
        public static Size UrunOrtaBoyut {
            get
            {
                //boyutlar web.config içerisnde tanımlandı
                Size sz = new Size();
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["UrunOrtaHeight"]);
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["UrunOrtaWidth"]);
                return sz;
            }
        }


        public static Size UrunBuyukBoyut
        {
            get
            {
                Size sz = new Size();
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["UrunBuyukWidth"]);
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["UrunBuyukHeight"]);
                return sz;
            }
        }

        public static Size SliderBoyut
        {
            get
            {
                Size sz = new Size();
                sz.Height = Convert.ToInt32(ConfigurationManager.AppSettings["SliderHeight"]);
                sz.Width = Convert.ToInt32(ConfigurationManager.AppSettings["SliderWidth"]);
                return sz;
                    
            }
        }
            

    
    }
}