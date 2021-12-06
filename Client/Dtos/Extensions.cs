using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Client.Dtos
{
    public static class Extensions
    {
        public static Image ToImage(this byte[] byteImage)
        {
            Image img = new Image();
            using (MemoryStream stream = new MemoryStream(byteImage))
            {
                ((Image)img).Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            return img;
        }
    }
}
