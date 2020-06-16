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
                _drawText = true;

            if (!string.IsNullOrEmpty(picture.path))
                _drawPicture = true;

            if (picture.lineColor != null)
                _drawLine = true;

            if (string.IsNullOrEmpty(picture.specsType))
                _drawSpecs = true;
        }
        public string CreateBitmapAtRuntime()
        {
            //создаем изображение и закрашиваем фон
            _bitmap = new Bitmap(_picture.width, _picture.height);
            Graphics graphics = Graphics.FromImage(_bitmap);
            SolidBrush _brush = new SolidBrush(_picture.bgColor);
            graphics.FillRectangle(_brush, 0, 0, _picture.width - 1, _picture.width - 1);

            //отрисовка текста, если есть
            if (_drawText)
            {
                if (string.IsNullOrEmpty(_picture.fontType))
                    _picture.fontType = FontFamily.GetFamilies(graphics).FirstOrDefault().ToString();
                Font font = new Font(_picture.fontType, _picture.fontSize);
                SolidBrush _textBrush = new SolidBrush(_picture.textColor);
                graphics.DrawString("test test", font, _textBrush, _picture.textX, _picture.textY);

                _textBrush.Dispose();
                font.Dispose();
            }

            //отрисовка линии, если есть
            if (_drawLine)
            {
                Pen pen = new Pen(_picture.lineColor, _picture.thickness);
                graphics.DrawLine(pen, _picture.lineX1, _picture.lineY1, _picture.lineX2, _picture.lineY2);
                pen.Dispose();
            }

            //добавление картинки
            if (_drawPicture)
            {
                try
                {
                    Rectangle _rectangle = new Rectangle(_picture.imgX, _picture.imgY, _picture.imgWidth, _picture.imgHeight);
                    Image _img = Image.FromFile(_picture.path);
                    graphics.DrawImage(_img, _rectangle);
                    _img.Dispose();
                }
                catch
                {

                }
            }

            _bitmap.Save(savingPath);

            _brush.Dispose();
            _bitmap.Dispose();
            graphics.Dispose();
            return savingPath;
        }
    }
}