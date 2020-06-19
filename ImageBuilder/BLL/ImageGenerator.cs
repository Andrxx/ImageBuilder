using ImageBuilder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageBuilder.BLL
{
    public class ImageModel
    {
        public string Path { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string BackColor { get; set; }
        public List<ImageText> Texts { get; set; }
        public List<ImagePicture> Pictures { get; set; }
        public List<ImageLine> Lines { get; set; }
        public List<ImageObject> Objects { get; set; }
    }

    public class ImageText
    {
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        public string BackColor { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
    }

    public class ImagePicture
    {
        public string Path { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Opacity { get; set; }
    }

    public class ImageLine
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public float Thickness { get; set; }
        public string Color { get; set; }
    }

    public class ImageObject
    {
        public string Type { get; set; }  // barcode, qrcode
        public string Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
    }


    public class ImageGenerator
    {
         
        public ImageGenerator()
        {
        }
        public static ResultModel CreateImageFromJSON(string json)
        {
            var res = new ResultModel();
            var m = new ImageModel { };
            // заполняем m из json 
            try
            {
                JObject jObject = JObject.Parse(json);
                JObject imageModel = (JObject)jObject.SelectToken("$.ImageModel");
                m = JsonConvert.DeserializeObject<ImageModel>(imageModel.ToString());
                //парсим массив текста
                JArray imageTexts = (JArray)jObject.SelectToken("$.ImageText");
                m.Texts = new List<ImageText>();
                if (imageTexts != null)
                {
                    foreach (JObject jo in imageTexts)
                    {
                        ImageText imageText = JsonConvert.DeserializeObject<ImageText>(jo.ToString());
                        m.Texts.Add(imageText);
                    }
                }
                //парсим массив картинок
                JArray imagePictures = (JArray)jObject.SelectToken("$.ImagePicture");
                m.Pictures = new List<ImagePicture>();
                foreach (JObject jo in imagePictures)
                {
                    ImagePicture imagePicture = JsonConvert.DeserializeObject<ImagePicture>(jo.ToString());
                    m.Pictures.Add(imagePicture);
                }
                //парсим массив линий
                JArray imageLines = (JArray)jObject.SelectToken("$.ImageLine");
                m.Lines = new List<ImageLine>();
                foreach (JObject jo in imageLines)
                {
                    ImageLine imageLine = JsonConvert.DeserializeObject<ImageLine>(jo.ToString());
                    m.Lines.Add(imageLine);
                }

                //парсим массив объектов
                JArray imageObjects = (JArray)jObject.SelectToken("$.ImageObject");
                m.Objects = new List<ImageObject>();
                if (imageObjects != null)
                {
                    foreach (JObject jo in imageObjects)
                    {
                        ImageObject imageObject = JsonConvert.DeserializeObject<ImageObject>(jo.ToString());
                        m.Objects.Add(imageObject);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.InnerException != null) msg += " / Inner: " + ex.InnerException.Message;
            }

            res = CreateImage(m);
            return res;
        }

        // 1 парс json в модель
        // дорисовка rect backColor 
        // линии и картинки обработать. 



        //в конструкторе проводим анализ переданных данных, и определяем объем отрисовки
        public static ResultModel CreateImage(ImageModel m)
        {
            Bitmap img = null;
            Graphics drawing = null;
            var res = new ResultModel();
            try
            {
                img = new Bitmap(m.Width, m.Height);
                drawing = Graphics.FromImage(img);

                //paint the background
                if (!String.IsNullOrEmpty(m.BackColor))
                {
                    drawing.Clear(System.Drawing.ColorTranslator.FromHtml(m.BackColor));
                }

                if (m.Texts != null)
                {
                    foreach (var r in m.Texts)
                    {
                        var color = System.Drawing.ColorTranslator.FromHtml(r.Color);
                        Brush textBrush = new SolidBrush(color);
                        RectangleF rectangle = new RectangleF(r.X, r.Y, r.Width, r.Height);
                        if (!string.IsNullOrEmpty(r.BackColor))
                        {
                            SolidBrush _brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml(r.BackColor));
                            drawing.FillRectangle(_brush, rectangle);
                        }
                        var fontFamily = !String.IsNullOrEmpty(r.FontFamily) ? r.FontFamily : "Arial";
                        var font = new Font(fontFamily, (float)r.FontSize);
                        drawing.DrawString(r.Text, font, textBrush, rectangle);
                    }
                }

                if (m.Lines != null)
                {
                    foreach (var l in m.Lines)
                    {
                        var color = System.Drawing.ColorTranslator.FromHtml(l.Color);
                        Pen pen = new Pen(color, l.Thickness);
                        drawing.DrawLine(pen, l.X1, l.Y1, l.X2, l.Y2);
                        pen.Dispose();
                    }
                }

                if (m.Pictures != null)
                {
                    foreach (var p in m.Pictures)
                    {
                        drawing.DrawImage(Image.FromFile(p.Path), new RectangleF(p.X, p.Y, p.Width, p.Height));
                    }
                }

                var path = HttpContext.Current.Server.MapPath(m.Path);
                img.Save(@"D:\\Test3.jpg");
                res = new ResultModel { Msg = "", Result = true };
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.InnerException != null) msg += " / Inner: " + ex.InnerException.Message;
                res = new ResultModel { Result = false, Msg = msg };
            }
            finally
            {
                if(img != null) img.Dispose();
                if (drawing != null) drawing.Dispose();
            }
            return res;
        }

    }
}