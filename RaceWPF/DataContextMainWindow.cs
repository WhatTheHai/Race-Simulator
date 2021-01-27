using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Controller;

namespace RaceWPF
{
    public class DataContextMainWindow: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DataContextMainWindow()
        {
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
            }
        }

        public string CurrentTrackName
        {
            get => $"Current track:\n{Data.CurrentRace.Track.Name}";
        }

        public void OnDriversChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
