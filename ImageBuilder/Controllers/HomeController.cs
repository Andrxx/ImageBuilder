using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageBuilder.BLL;
using ImageBuilder.Models;

namespace ImageBuilder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateTestImg()
        {

            //Picture picture = new Picture();

            //picture.pictureName = "TestImg";
            //picture.fileType = "png";
            //picture.height = 150;
            //picture.width = 150;
            //picture.bgColor = Color.DarkOrange;

            //picture.text = "Some text";
            //picture.textX = 15;
            //picture.textY = 15;
            //picture.fontType = "Impact";
            //picture.fontSize = 20;
            //picture.textColor = Color.Black;
            ////picture.textBgColor = Color.DarkOrange;
            //picture.textLayer = 4;

            //picture.path = @"D:\iconva.png";
            //picture.imgX = 0;
            //picture.imgY = 50;
            //picture.opacity = 1;
            //picture.imgWidth = 150;
            //picture.imgHeight = 70;
            //picture.imgLayer = 1;

            //picture.lineX1 = 0;
            //picture.lineY1 = 45;
            //picture.lineX2 = picture.width - 1;
            //picture.lineY2 = 45;
            //picture.thickness = 3;
            //picture.lineColor = Color.GreenYellow;
            //picture.lineLayer = 1;

            //picture.specsType = "type";
            //picture.specsValue = "23";
            //picture.specsX = 0;
            //picture.specsY = 0;
            //picture.specsColor = Color.Black;
            //picture.specsLayer = 2;

            // CreateBitMap bitMap = new CreateBitMap(picture);

            // string path = bitMap.CreateBitmapAtRuntime();
            // ViewBag.path = path;
            string str = @"{
        'ImageModel':{
		'path':'image.png',
		'width':'500',
		'height':'500',
		'backColor':'red',
        },
		'ImageText': [
			{
				'Text':'Some text',
				'x':'10',
				'y':'10',
				'width':'400',
				'height':'30',
				'color':'black',
				'backColor':'black',
				'fontSize':'18',
				'fontFamily':'Arial'

            },
			{
				'Text':'Second line',
				'x':'10',
				'y':'40',
				'width':'800',
				'height':'30',
				'color':'blue',
				'backColor':'black',
				'fontSize':'20',
				'fontFamily':'Arial'
			},
			{
				'Text':'третья линия',
				'x':'10',
				'y':'70',
				'width':'400',
				'height':'30',
				'color':'green',
				'backColor':'black',
				'fontSize':'18',
				'fontFamily':'Arial'
			}
		],
		'ImagePicture': [
			{
				'path':'D:/iconva.png',
				'x':'0',
				'y':'100',
				'width':'100',
				'height':'75',
				'opacity':'0.9'
			},
			{
				'path':'D:/iconva.png',
				'x':'0',
				'y':'175',
				'width':'150',
				'height':'75',
				'opacity':'0.9'
			}
		],
		'ImageLine': [
			{
				'x1':'0',
				'y1':'200',
				'x2':'300',
				'y2':'200',
				'thickness':'3',
				'color':'black'
			},
			{
				'x1':'150',
				'y1':'300',
				'x2':'50',
				'y2':'0',
				'thickness':'20',
				'color':'black'
			},
			{
				'x1':'10',
				'y1':'200',
				'x2':'150',
				'y2':'300',
				'thickness':'3',
				'color':'black'
			},
            {
				'x1':'0',
				'y1':'0',
				'x2':'150',
				'y2':'300',
				'thickness':'3',
				'color':'white'
			}
		]	
    }";

            ImageGenerator.CreateImageFromJSON(str);
            return View();
        }
        
    }
}