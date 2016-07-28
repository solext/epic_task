using System;
using System.Windows;

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
            //this.DataContext = new StickerWindowVM();
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
            catch (ArgumentException)
            {

            }

            loc_LocationChanged(sender, null);
        }

        private void loc_StateChanged(object sender, EventArgs e)
        {

        }
    }
}
