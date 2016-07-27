using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TestWindow.View;
using TestWindow.ViewModel;

namespace TestWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var stickerWindow = new StickerWindow();
            stickerWindow.DataContext = new StickerWindowVM(stickerWindow);
            stickerWindow.Show();
        }
        

    }
}
