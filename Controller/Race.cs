using Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool _isMoving = false;
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
            SectionsToDictionary();
            PlaceAllParticipants();

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;

            Start();
        }

        public void MoveParticipants()
        {
            //Initialize
            SectionData nextSection = _positions.First().Value;

            //Iterate through all the positions, reverse the positions because otherwise it's checking the wrong way
            foreach (KeyValuePair<Section, SectionData> kvPair in _positions.Reverse())
            {
                SectionData currentSectionSD = kvPair.Value;
                if (currentSectionSD.Left != null)
                {
                    currentSectionSD.DistanceLeft +=
                        currentSectionSD.Left.Equipment.Speed * currentSectionSD.Left.Equipment.Performance;
                    if (currentSectionSD.DistanceLeft >= Section.Length)
                    {
                        MoveParticipantsLeftSection(nextSection, currentSectionSD);
                    }
                }
                if (currentSectionSD.Right != null)
                {
                    currentSectionSD.DistanceRight +=
                        currentSectionSD.Right.Equipment.Speed * currentSectionSD.Right.Equipment.Performance;
                    if (currentSectionSD.DistanceRight >= Section.Length)
                    {
                        MoveParticipantsRightSection(nextSection, currentSectionSD);
                    }
                }

                
            }
        }

        public void MoveParticipantsLeftSection(SectionData nextSection, SectionData currentSection)
        {
            if (nextSection.Left == null)
            {
                nextSection.Left = currentSection.Left;
                nextSection.DistanceLeft = currentSection.DistanceLeft - Section.Length;
                //Reset
                currentSection.Left = null;
                currentSection.DistanceLeft = 0;


            }
            //if left is full, try right
            else if (nextSection.Right == null)
            {
                nextSection.Right = currentSection.Left;
                nextSection.DistanceRight = 0;
                //Reset
                currentSection.Left = null;
                currentSection.DistanceLeft = 0;
            }
        }
        public void MoveParticipantsRightSection(SectionData nextSection, SectionData currentSection)
        {
            if (nextSection.Right == null)
            {
                nextSection.Right = currentSection.Right;
                nextSection.DistanceRight = currentSection.DistanceRight - Section.Length;
                //Reset
                currentSection.Right = null;
                currentSection.DistanceRight = 0;

            }
            //if right is full, try left
            else if (nextSection.Left == null)
            {
                nextSection.Left = currentSection.Right;
                nextSection.DistanceRight = 0;
                //Reset
                currentSection.Right = null;
                currentSection.DistanceRight = 0;
            }
        }
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //Allows pauses/debugging without crashing the console!
            if (!_isMoving)
            {
                _isMoving = true;
                MoveParticipants();
                DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = this.Track });
                _isMoving = false;
            }

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

        public void SectionsToDictionary()
        {
            foreach (Section section in Track.Sections)
            {
                _positions.Add(section, new SectionData());
            }
        }
        public void PlaceAllParticipants()
        {
            //Stack instead of queue because you want the first people that you want to add to be at the front of the
            //startSections
            Stack<Section> startSections = new Stack<Section>();
            int participantsCounter = 0;
            int totalParticipants = Participants.Count;

            //Push only sections with the type "StartGrid" into the stack
            foreach (Section section in Track.Sections)
            {
                if( section.SectionType == SectionTypes.StartGrid)
                {
                    startSections.Push(section);
                }
            }
            while(startSections.Count > 0)
            {
                Section section = startSections.Pop();
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
