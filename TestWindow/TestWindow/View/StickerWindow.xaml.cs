using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestWindow.ViewModel;

namespace TestWindow.View
{
    /// <summary>
    /// Interaction logic for StickerWindow.xaml
    /// </summary>
    public partial class StickerWindow : Window
    {
        public bool Rside = false;
        //private Window _parent;
        public StickerWindow()
        {
            InitializeComponent();
            this.DataContext = new StickerWindowVM();
            //Owner = parent;
            //Owner.LocationChanged += loc_LocationChanged;
            //Owner.SizeChanged += Window_SizeChanged; 

            //this.LocationChanged += loc_LocationChanged_StW;
            //_parent = parent;
        }

        private void loc_LocationChanged_StW(object sender, EventArgs e)
        {
            if (this.Left > Owner.Left + Owner.ActualWidth/2)
            {
                Rside = true;
            }
            else
            {
                Rside = false;
            }
        }

        private void loc_LocationChanged(object sender, EventArgs e)
        {
            if (Rside)
            {
                Left = Owner.Left + Owner.ActualWidth - 15;
            }
            else
            {
                Left = Owner.Left - this.ActualWidth + 15;
            }
            //}
            Top = Owner.Top;
            Height = Owner.Height;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = Owner.Height;
            try
            {
                if (Rside)
                {
                    Width = Width - (e.PreviousSize.Width - e.NewSize.Width);
                }
                else
                {
                    Width = Width - (e.PreviousSize.Width - e.NewSize.Width);
                }
            }
            catch (System.ArgumentException)
            {

            }

            loc_LocationChanged(sender, null);
        }
    }
}
