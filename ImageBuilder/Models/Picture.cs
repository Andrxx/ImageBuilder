using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace ImageBuilder.Models
{
    public class Picture
    {
        //базовые настройки
        public string pictureName { get; set; }
        public string fileType { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Color bgColor { get; set; }

        //текст
        public string text { get; set; }
        public int textX { get; set; }
        public int textY { get; set; }
        public Color textColor { get; set; }
        public Color textBgColor { get; set; }
        public string fontType { get; set; }
        public int fontSize { get; set; }
        public int textWidth { get; set; }
        public int textLayer { get; set; }

        //картинки и логотипы
        public string path { get; set; }
        public int imgX { get; set; }
        public int imgY { get; set; }
        public double opacity { get; set; }
        public int imgWidth { get; set; }
        public int imgHeight { get; set; }
        public int imgLayer { get; set; }

        //линии
        public int lineX1 { get; set; }
        public int lineY1 { get; set; }
        public int lineX2 { get; set; }
        public int lineY2 { get; set; }
        public int thickness { get; set; }
        public Color lineColor { get; set; }
        public int lineLayer { get; set; }

        //спец
        public string specsType { get; set; }
        public string specsValue { get; set; }
        public int specsX { get; set; }
        public int specsY { get; set; }
        public Color specsColor { get; set; }
        public int specsLayer { get; set; }

    }
}