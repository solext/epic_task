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

        

     
    }
}
