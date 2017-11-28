using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace QuickNet
{
    public static class Renderer
    {
        public static void EllipseChangeSize(Ellipse ellipse, int amountOfEllipses)
        {
            int height = 555;
            int NewDiameter = (height - 55) / amountOfEllipses; // 55 is an arbitrary choice
            //ellipse.Width = NewDiameter;
            //ellipse.Height = NewDiameter;

            //MainPage.AnimateEllipse(ellipse, NewDiameter);
        }

        public static double GetEllipseSize(int amountOfEllipses)
        {
            int height = 555;
            int NewDiameter = (height - 55) / amountOfEllipses; // 55 is an arbitrary choice
            return NewDiameter;
        }
    }
}
