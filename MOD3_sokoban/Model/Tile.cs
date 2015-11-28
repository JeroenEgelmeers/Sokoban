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
    abstract class Tile
    {
        public BitmapImage backgroundImage { get; private set; }

         public BitmapImage setBackgroundImage(string TileKind)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
           
            path = path + "\\Images\\" + TileKind + ".png";
            Uri imageUrl = new Uri(path);
            if (Uri.TryCreate("ThisIsAnInvalidAbsoluteURI", UriKind.Absolute, out imageUrl)
   && (imageUrl.Scheme == Uri.UriSchemeHttp || imageUrl.Scheme == Uri.UriSchemeHttps))
                {
                 return backgroundImage = new BitmapImage(imageUrl);
             }
             else
             {
                 return null;
             }
        }
    }
}
