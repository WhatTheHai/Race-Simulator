using System;
using System.Collections.Generic;
using System.Drawing;
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
        private CompetitionPartStats _competitionPartStats;
        private CurrentRaceStats _currentRaceStats;
        public MainWindow()
        {
            Data.Initialize();
            
            ImageCache.Initialize();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.NextRace += OnNextRace;
            InitializeComponent();
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

        public void OnNextRace(object sender, EventArgs e)
        {
            ImageCache.ClearCache();
            Data.CurrentRace.CleanupEvents();
            Data.NextRace();
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
                Data.CurrentRace.NextRace += OnNextRace;
                Data.CurrentRace.Start();
            }
            else
            {
                Visualization.DrawTrack(null);
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_ShowCurrentRaceStats_Click(object sender, RoutedEventArgs e)
        {
            _currentRaceStats = new CurrentRaceStats();
            _currentRaceStats.Show();
        }

        private void MenuItem_ShowCompPartStats_Click(object sender, RoutedEventArgs e)
        {
            _competitionPartStats = new CompetitionPartStats();
            _competitionPartStats.Show();
        }
    }
}
