using StrongTicaret.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrongTicaret.Web.UI.App_Classes
{


    public class Context
    {
        //StrongContext webconfig de connection name dir.
        private static StrongContext baglanti;

        public static StrongContext Baglanti
        {
            get
            {

                if (baglanti == null)
                    baglanti = new StrongContext();

                return baglanti;

            }
            set { baglanti = value; }
        }

    }
}