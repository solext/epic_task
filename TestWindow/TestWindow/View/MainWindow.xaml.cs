using System.Windows;

namespace TestWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Show();
            
            //var child = new StickerWindow(this);
            //child.Show();
        }

        

        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Console.WriteLine();
        //}
    }
}
