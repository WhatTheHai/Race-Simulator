using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<IParticipant, int> _finishes = new Dictionary<IParticipant, int>();
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Timer _timer;
        private int _rounds = 1;
        private int _participantsCounter;
        private bool _isMoving = false;
        public event EventHandler DriversChanged;
        public event EventHandler NextRace;
        public Queue<IParticipant> finalScore = new Queue<IParticipant>();

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
            _participantsCounter = Participants.Count;

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;

            Start();
        }
        //If the participant gets move from the end to the start of the track, hide the participant
        //the hidden section does not count as an extra length, so it makes it up by adding the
        //section length to the distance
        public void hideParticipant(SectionData nextSection, SectionData hiddenSectionData, IParticipant participant1, IParticipant participant2)
        {
            //Left has to be the participant has had been moved, so either participant1 or participant2
            if (nextSection.Left != null && (nextSection.Left == participant1 || nextSection.Left == participant2))
            {
                hiddenSectionData.Left = nextSection.Left;
                hiddenSectionData.DistanceLeft = nextSection.DistanceLeft + Section.Length;
                //Reset
                nextSection.Left = null;
                nextSection.DistanceLeft = 0;
            }
            //Same principle as in the if condition before, but instead check right.
            if (nextSection.Right != null && (nextSection.Right == participant1 || nextSection.Right == participant2))
            {
                hiddenSectionData.Right = nextSection.Right;
                hiddenSectionData.DistanceRight = nextSection.DistanceRight + Section.Length;
                //Reset
                nextSection.Right = null;
                nextSection.DistanceRight = 0;
            }
        }

        public void CheckFinishes(SectionData finish, string side)
        {
            if (finish.Left != null && side == "left")
            {
                if (_finishes[finish.Left] >= _rounds)
                {
                    RemoveParticipant(finish, side);
                    _participantsCounter--;
                }
                else
                {
                    _finishes[finish.Left]++;
                }
            }

            if (finish.Right != null && side == "right")
            {
                if (_finishes[finish.Right] >= _rounds)
                {
                    RemoveParticipant(finish, side);
                    _participantsCounter--;
                }
                else
                {
                    _finishes[finish.Right]++;
                }
            }
        }

        public void RemoveParticipant(SectionData finish, string side)
        {
            if (side == "left")
            {
                AddToFinalScore(finish.Left);
                finish.Left = null;
                finish.DistanceLeft = 0;
            } 
            else if (side == "right")
            {
                AddToFinalScore(finish.Right);
                finish.Right = null;
                finish.DistanceRight = 0;
            }
        }

        public void AddToFinalScore(IParticipant participant)
        {
            finalScore.Enqueue(participant);
        }
        public void RepairOrBreakEquipment()
        {

            const int baseChance = 5;
            foreach (IParticipant participant in Participants)
            {
                //Repair
                if (participant.Equipment.IsBroken)
                {
                    //Everytime it's broken add the time.
                    Data.Competition.AddBrokenTime(participant.Name, DateTime.Now.Subtract(StartTime));
                    // 50% chance to repair every round, mathematically if something is broken
                    // it'll be broken for 2 rounds on average. (Since it's a geometric series that converges to 2)
                    int roll = _random.Next(0, 2);
                    if (roll != 1) continue;
                    participant.Equipment.IsBroken = false;
                    if (participant.Equipment.Speed >= 10)
                    {
                        participant.Equipment.Speed -= _random.Next(0, 2);
                    }
                    else
                    {
                        participant.Equipment.Speed++;
                    }
                    participant.Equipment.Performance -= _random.Next(0, 3);
                }
                //Break
                else
                {
                    //Lowers the chance of breaking depending on the quality
                    int chance = baseChance + participant.Equipment.Quality*2;
                    int roll = _random.Next(0, chance);
                    if (roll == chance - 1)
                    {
                        participant.Equipment.IsBroken = true;
                    }
                }
            }
        }
        public void MoveParticipants()
        {
            //Initialize
            SectionData nextSection = _positions.First().Value;
            SectionData hiddenSectionData = new SectionData();
            bool enteredOnce = false;
            IParticipant participant1 = _positions.Last().Value.Left;
            IParticipant participant2 = _positions.Last().Value.Right;

            //Iterate through all the positions, reverse the positions because otherwise it's checking the wrong way
            foreach (KeyValuePair<Section, SectionData> kvPair in _positions.Reverse())
            {
                SectionData currentSectionSD = kvPair.Value;
                if (currentSectionSD.Left != null)
                {
                    SaveSectionTimes(kvPair.Key, kvPair.Value, DateTime.Now, "left");
                    if (!currentSectionSD.Left.Equipment.IsBroken)
                    {
                        currentSectionSD.DistanceLeft +=
                            currentSectionSD.Left.Equipment.Speed * currentSectionSD.Left.Equipment.Performance;
                        if (currentSectionSD.DistanceLeft >= Section.Length)
                        {
                            if (kvPair.Key.SectionType == SectionTypes.Finish)
                            {
                                CheckFinishes(kvPair.Value, "left");
                            }

                            MoveParticipantsLeftSection(nextSection, currentSectionSD);
                        }
                    }
                }
                if (currentSectionSD.Right != null)
                {
                    SaveSectionTimes(kvPair.Key, kvPair.Value, DateTime.Now, "right");
                    if (!currentSectionSD.Right.Equipment.IsBroken)
                    {
                        currentSectionSD.DistanceRight +=
                            currentSectionSD.Right.Equipment.Speed * currentSectionSD.Right.Equipment.Performance;
                        if (currentSectionSD.DistanceRight >= Section.Length)
                        {
                            if (kvPair.Key.SectionType == SectionTypes.Finish)
                            {
                                CheckFinishes(kvPair.Value, "right");
                            }
                            MoveParticipantsRightSection(nextSection, currentSectionSD);
                        }
                    }
                }
                //If you moved it, don't move it again.
                if (!enteredOnce)
                {
                    //Check if any of them has moved from the end of the track to the start
                    if (participant1 == _positions.First().Value.Left ||
                        participant1 == _positions.First().Value.Right ||
                        participant2 == _positions.First().Value.Left ||
                        participant2 == _positions.First().Value.Right)
                    {
                        hideParticipant(_positions.First().Value, hiddenSectionData, participant1, participant2);
                    }
                    //So the if condition only executes once at the start of the foreach
                    enteredOnce = true;
                }

                //Compare next two sections, backwards
                nextSection = kvPair.Value;
            }

            //No point on revealing if there is nothing on the hiddensection
            if (hiddenSectionData.Left != null || hiddenSectionData.Right != null)
            {
                RevealParticipant(_positions.First().Value, hiddenSectionData);
            }
        }

        //No logic for catching up, because they already moved so the logic of catching up is
        //already there
        public void RevealParticipant(SectionData nextSection, SectionData hiddenSectionData)
        {
            if (hiddenSectionData.Left != null)
            {
                nextSection.Left = hiddenSectionData.Left;
                nextSection.DistanceLeft = hiddenSectionData.DistanceLeft;

                hiddenSectionData.Left = null;
                hiddenSectionData.DistanceLeft = 0;
            } 
            if (hiddenSectionData.Right != null)
            {
                nextSection.Right = hiddenSectionData.Right;
                nextSection.DistanceRight = hiddenSectionData.DistanceRight;

                hiddenSectionData.Right = null;
                hiddenSectionData.DistanceRight = 0;
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
                nextSection.DistanceRight = currentSection.DistanceLeft - Section.Length;
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
                nextSection.DistanceRight = currentSection.DistanceRight - Section.Length;
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
                RepairOrBreakEquipment();
                StartTime = DateTime.Now;
                DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = this.Track });
                CheckRaceFinished();
                _isMoving = false;
            }
        }


        public void Start()
        {
            _timer.Start();
            StartTime = DateTime.Now;
        }

        public void RandomizeEquipment()
        {
            foreach(IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(5, 10);
                participant.Equipment.Performance = _random.Next(60, 100);
            }
        }

        public void SectionsToDictionary()
        {
            foreach (Section section in Track.Sections)
            {
                _positions.Add(section, new SectionData());
            }
        }

        //Places all participants in the sections + every participant has their own counter of amount of finishes
        public void PlaceAllParticipants()
        {
            RandomizeEquipment();
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

                    _finishes.Add(Participants[participantsCounter], 0);
                    _finishes.Add(Participants[participantsCounter + 1], 0);
                    participantsCounter += 2;
                } else if (totalParticipants > participantsCounter)
                {
                    sectionData.Left = Participants[participantsCounter];
                    _finishes.Add(Participants[participantsCounter], 0);
                    participantsCounter++;
                }
            }
        }
        public void SaveSectionTimes(Section section, SectionData sectionData, DateTime timePast, string side)
        {
            String name = "";
            switch (side)
            {
                case "left":
                    name = sectionData.Left.Name;
                    break;
                case "right":
                    name = sectionData.Right.Name;
                    break;
                case "default":
                    break;
            }

            TimeSpan timeTaken = timePast.Subtract(StartTime);
            Data.Competition.AddSectionTimesToParticipants(name, section, timeTaken);
        }
        public void CheckRaceFinished()
        {
            if (_participantsCounter == 0)
            {
                NextRace?.Invoke(this, new EventArgs());
            }
        }

        public void CleanupEvents()
        {
            Delegate[] delegates = DriversChanged?.GetInvocationList();
            if (delegates != null)
            {
                foreach (var d in delegates)
                {
                    DriversChanged -= (EventHandler)d;
                }
            }
            delegates = NextRace?.GetInvocationList();
            if (delegates != null)
            {
                foreach (var d in delegates)
                {
                    NextRace -= (EventHandler)d;
                }
            }
        }
    }
}
