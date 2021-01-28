using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Controller;
using Model;

namespace RaceWPF
{
    public class DataContextCompetition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<string> AllPoints
        {
            get => Data.Competition.ParticipantPoints.GetList()
                .Select(i => $"{i.Name} - {((ParticipantPoints)i).Points}");
        }

        public string BestParticipant
        {
            get => Data.Competition.ParticipantSpeedPerTrack.bestParticipant();
        }

        public DataContextCompetition()
        {
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
            }
        }

        public void OnDriversChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
