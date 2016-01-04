using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using WforViolation.Models;

namespace WforViolation.Helpers
{
    public static class ImageHelper
    {
        public static PictureInterface SavePic(HttpPostedFileBase Resim, HttpContextBase ctx,PictureInterface pictureInterface)
        {
            int kucukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
            int kucukHeigth = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
            int ortaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
            int ortaHeigth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
            int buyukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
            int buyukHeigth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
            string newName = Path.GetFileNameWithoutExtension(Resim.FileName) + "-" + Guid.NewGuid() + Path.GetExtension(Resim.FileName);
            Image orjRes = Image.FromStream(Resim.InputStream);
            Bitmap kucukRes = new Bitmap(orjRes, kucukWidth, kucukHeigth);
            Bitmap ortaRes = new Bitmap(orjRes, ortaWidth, ortaHeigth);
            Bitmap buyukRes = new Bitmap(orjRes);

            //kucukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Kucuk/" + newName));
            //ortaRes.Save(ctx.Server.MapPath("~/Content/Resimler/Orta/" + newName));
            //buyukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Buyuk/" + newName));

            using (var m = new MemoryStream()) 
            {
                kucukRes.Save(m, ImageFormat.Jpeg);
                ortaRes.Save(m, ImageFormat.Jpeg);
                buyukRes.Save(m, ImageFormat.Jpeg); 
                var img = Image.FromStream(m);
                kucukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Kucuk/" + newName));
                ortaRes.Save(ctx.Server.MapPath("~/Content/Resimler/Orta/" + newName));
                buyukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Buyuk/" + newName)); 

            }

            pictureInterface.Name = Resim.FileName;
            pictureInterface.LargePath = "/Content/Resimler/Buyuk/" + newName;
            pictureInterface.MediumPath = "/Content/Resimler/Orta/" + newName;
            pictureInterface.SmallPath = "/Content/Resimler/Kucuk/" + newName;
            pictureInterface.AddedDateTime = DateTime.Now;

            return pictureInterface;

        }
    }
}