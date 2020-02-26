using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace webTemplate.Controllers
{
    public class ImageMergeController : BaseController
    {
        // GET: ImageMerge
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageMerge(HttpPostedFileBase UploadImage, string Text)
        {
            //-----------------------
            string firstText = Text;
            string imageFilePath = "C:/xinyu/" + UploadImage.FileName;
            string LogoPath = "P:/光點專案/01.公司介紹/logo/logo_135x140.png";
            Bitmap logomap = (Bitmap)Image.FromFile(LogoPath);//load the image file

            using (Stream inpustStream = UploadImage.InputStream)
            {
                Bitmap bitmap = (Bitmap)Image.FromStream(inpustStream, true, true);//load the image file
                PointF firstLocation = new PointF(bitmap.Width - 50, bitmap.Height - 20);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("微軟正黑體", 14))
                    {
                        ///文字水平垂直
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        graphics.DrawString(firstText, arialFont, Brushes.Black, firstLocation, stringFormat);
                    }
                    graphics.DrawImage(logomap, bitmap.Width - 140, bitmap.Height - 55, 50, 50);
                }
                bitmap.Save(imageFilePath);//save the image file   
                inpustStream.Dispose();
            }
            //-----------------------
            return View("Index");
        }
    }
}