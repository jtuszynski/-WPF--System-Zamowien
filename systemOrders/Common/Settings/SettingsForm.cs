using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Common.Settings
{
    public class SettingsForm
    {
        public SettingsForm() { }

        // Ustawia tło formatki na obrazek zamieszczony w zasobach projektu SystemOrders o nazwie background_image
        public ImageBrush SettingsBackroungForm()
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Common.Properties.Resources.background_image.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            return new ImageBrush(bitmapSource);
        }
        public BitmapImage SettingsIconsForm()
        {
            return new BitmapImage(new Uri(@"/Resources/icons_large.png"));
        }

    }
}
