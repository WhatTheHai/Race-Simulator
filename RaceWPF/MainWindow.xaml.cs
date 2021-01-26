﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Controller;
using Model;

namespace RaceWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Data.Initialize();
            ImageCache.Initialize();
            InitializeComponent();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        public void OnDriversChanged(object sender, EventArgs e)
        {
            DriversChangedEventArgs driverE = (DriversChangedEventArgs) e;
            this.TrackImg.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackImg.Source = null;
                    this.TrackImg.Source = Visualization.DrawTrack(driverE.Track);
                }));
        }
    }
}
