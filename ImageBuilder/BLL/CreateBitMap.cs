using ImageBuilder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace ImageBuilder.BLL
{

    public class CreateBitMap
    {
        Bitmap _bitmap;
        Picture _picture = new Picture();
        Graphics _graphics;
        List<int> _order = new List<int>();
        Dictionary<int, bool> dict = new Dictionary<int, bool>();
        bool _drawText = false;
        bool _drawPicture = false;
        bool _drawLine = false;
        bool _drawSpecs = false;
        string savingPath = @"D:\\Test2.jpg";
        //в конструкторе проводим анализ переданных данных, и определяем объем отрисовки
        public CreateBitMap(Picture picture)
        {
            _picture = picture;

            if (!string.IsNullOrEmpty(picture.text))
            {
                _drawText = true;
                _order.Add(_picture.textLayer);
                dict.Add(_picture.textLayer, _drawText);
            }


            if (!string.IsNullOrEmpty(picture.path))
            {
                _drawPicture = true;
                _order.Add(_picture.imgLayer);
                //dict.Add(_picture.imgLayer, _drawPicture);
            }


            if (picture.lineColor != null)
            {
                _drawLine = true;
                _order.Add(_picture.lineLayer);
                //dict.Add(_picture.lineLayer, _drawLine);
            }


            if (string.IsNullOrEmpty(picture.specsType))
            {
                _drawSpecs = true;
                _order.Add(_picture.specsLayer);
                //dict.Add(_picture.specsLayer, _drawSpecs);
            }

        }

        public string CreateBitmapAtRuntime()
        {
            //создаем изображение и закрашиваем фон
            _bitmap = new Bitmap(_picture.width, _picture.height);
            _graphics = Graphics.FromImage(_bitmap);
            SolidBrush _brush = new SolidBrush(_picture.bgColor);
            _graphics.FillRectangle(_brush, 0, 0, _picture.width - 1, _picture.width - 1);


            //отрисовка текста, если есть
            if (_drawText)
            {
                DrawText();
            }

            //отрисовка линии, если есть
            if (_drawLine)
            {
                DrawLine();
            }

            //добавление картинки
            if (_drawPicture)
            {
                DrawPicture();
            }

            _bitmap.Save(savingPath);

            _brush.Dispose();
            _bitmap.Dispose();
            _graphics.Dispose();
            return savingPath;
        }

        void DrawText()
        {
            if (string.IsNullOrEmpty(_picture.fontType))
                _picture.fontType = FontFamily.GetFamilies(_graphics).FirstOrDefault().ToString();
            Font font = new Font(_picture.fontType, _picture.fontSize);
            SolidBrush _textBrush = new SolidBrush(_picture.textColor);
            _graphics.DrawString("test test", font, _textBrush, _picture.textX, _picture.textY);

            _textBrush.Dispose();
            font.Dispose();
        }

        void DrawLine()
        {
            Pen pen = new Pen(_picture.lineColor, _picture.thickness);
            _graphics.DrawLine(pen, _picture.lineX1, _picture.lineY1, _picture.lineX2, _picture.lineY2);
            pen.Dispose();
        }

        void DrawPicture()
        {
            try
            {
                Rectangle _rectangle = new Rectangle(_picture.imgX, _picture.imgY, _picture.imgWidth, _picture.imgHeight);
                Image _img = Image.FromFile(_picture.path);
                _graphics.DrawImage(_img, _rectangle);
                _img.Dispose();
            }
            catch
            {

            }
        }

    }

    class DrawableObject
    {
        Graphics _graphics;
        int order;
        DrawableObject(Graphics graphics)
        {
            _graphics = graphics;
        }
    }



}