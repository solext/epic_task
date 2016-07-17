using Blue.Windows;
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

namespace TestWindow.View
{
    /// <summary>
    /// Interaction logic for StickerWindow.xaml
    /// </summary>
    public partial class StickerWindow : Window
    {
        private StickyWindow StickyWindow;
        //private Window _parent;
        public StickerWindow(Window parent)
        {
            InitializeComponent();
            Owner = parent;
            Owner.LocationChanged += loc_LocationChanged;
            Owner.SizeChanged += Window_SizeChanged;
            this.Loaded += StickerWindowLoaded;
            
            //_parent = parent;
        }

        private void StickerWindowLoaded(object sender, RoutedEventArgs e)
        {
            StickyWindow = new StickyWindow(this);
            StickyWindow.StickToScreen = true;
            StickyWindow.StickToOther = true;
            StickyWindow.StickOnResize = true;
            StickyWindow.StickOnMove = true;
        }

        private void loc_LocationChanged(object sender, EventArgs e)
        {
            Left = Owner.Left + Owner.ActualWidth;
            Top = Owner.Top;
            Height = Owner.Height;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = Owner.Height;
            Width = Width;
            loc_LocationChanged(sender, null);
        }
    }
}
