﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWindow.View;

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
