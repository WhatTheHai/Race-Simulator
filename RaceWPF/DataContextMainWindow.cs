﻿using System;
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
            get => Data.CurrentRace != null
                ? $"Current track:\n{Data.CurrentRace.Track.Name}"
                : "All races\nhave finished!";
        }

        public void OnDriversChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
