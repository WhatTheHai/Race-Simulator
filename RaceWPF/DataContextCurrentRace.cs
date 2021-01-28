using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Controller;
using Model;

namespace RaceWPF
{
    public class DataContextCurrentRace : INotifyPropertyChanged
    {
        public IEnumerable<string> SectionTimes
        {
            get => Data.Competition.ParticipantsTimes.GetList()
                .Select(i => $"{i.Name} - {((ParticipantSectionTimes) i).Time}");
        }
        public IEnumerable<string> TimesBroken
        {
            get => Data.Competition.ParticipantTimesBroken.GetList().Select(i => $"{i.Name} - {((ParticipantTimesBroken)i).Time}");
        }

        public IEnumerable<string> ComponentStats
        {
            get => Data.Competition.ParticipantSpeedPerTrack.GetList().Select(i => $"{i.Name} - {((ParticipantSpeedPerTrack)i).Performance}P {((ParticipantSpeedPerTrack)i).Speed}S - {((ParticipantSpeedPerTrack)i).Track.Name}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DataContextCurrentRace()
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
