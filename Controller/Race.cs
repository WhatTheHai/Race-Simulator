using Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
//ambiguous

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Timer _timer;
        public event EventHandler DriversChanged;


        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            PlaceAllParticipants();

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;

        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {

        }

        public void Start()
        {
            _timer.Start();
        }

        public void RandomizeEquipment()
        {
            foreach(IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(1, 10);
                participant.Equipment.Performance = _random.Next(1, 100);
            }
        }
        public void PlaceAllParticipants()
        {
            //Stack instead of queue because you want the first people that you want to add to be at the front of the
            //startsections
            Stack<Section> StartSections = new Stack<Section>();
            int participantsCounter = 0;
            int totalParticipants = Participants.Count;

            //Push only sections with the type "StartGrid" into the stack
            foreach (Section section in Track.Sections)
            {
                if( section.SectionType == SectionTypes.StartGrid)
                {
                    StartSections.Push(section);
                }
            }
            while(StartSections.Count > 0)
            {
                Section section = StartSections.Pop();
                SectionData sectionData = GetSectionData(section);
               
                // Keeps adding 2 till there is 1 left and after that it reaches the end of the while loop.
                if(totalParticipants > participantsCounter + 1)
                {
                    sectionData.Left = Participants[participantsCounter];
                    sectionData.Right = Participants[participantsCounter + 1];
                    participantsCounter += 2;
                } else if (totalParticipants > participantsCounter)
                {
                    sectionData.Left = Participants[participantsCounter];
                    participantsCounter++;
                }
            }
        }
    }
}
