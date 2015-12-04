using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MOD3_sokoban.Model
{
    public abstract class Tile
    {
        public BitmapImage BackgroundImage { get; private set; }

         public void SetBackgroundImage(string imageName)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
           
            path = path + "\\Images\\" + imageName + ".png";
            Uri imageUrl = new Uri(path);

            try { BackgroundImage = new BitmapImage(imageUrl); } catch { /* Image does not exsists. */ Console.WriteLine("Warning: Image does not exsist!"); }
        }

        public void returnValue()
        {
            Console.WriteLine("Test");
        }
    }
}
