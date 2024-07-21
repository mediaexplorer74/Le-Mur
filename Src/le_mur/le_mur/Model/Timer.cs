using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using le_mur.Consts;
using TL;

namespace le_mur.Model
{
    public class Timer : INotifyPropertyChanged
    {
        public enum RepeatStatus
        {
            Not,
            Weekly,
            Fortnightly,
        }

        private int _id;
        private DayOfWeek?[] _weekdays;
        private DateTime?[] _dates;
        private RepeatStatus _repeat;
        private TimeSpan _startTime;
        private TimeSpan _endTime;

        public Timer()
        {
            _weekdays = new DayOfWeek?[7] { null, null, null, null, null, null, null };
            _dates = new DateTime?[7] { null, null, null, null, null, null, null };
        }

        public Timer(int id)
        {
            // todo поиск в базе
            _id = id;
            _weekdays = new DayOfWeek?[7] { DayOfWeek.Sunday, DayOfWeek.Thursday, null, null, null, null, null };
            _dates = new DateTime?[7] { new DateTime(2024, 4, 14), null, null, null, null, null, null };
            _startTime = new TimeSpan(9, 0, 0);
            _endTime = new TimeSpan(12, 0, 0);
            _repeat = RepeatStatus.Weekly;
        }

        public int Id
        {
            get => _id; 
            set => _id = value;
        }
        public DayOfWeek?[] Weekdays
        {
            get => _weekdays;
            set => _weekdays = value;
        }
        public DateTime?[] Dates
        {
            get => _dates;
            set => _dates = value;
        }
        public RepeatStatus Repeat
        {
            get => _repeat; 
            set => _repeat = value;
        }
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        public string NextSessionStr
        {
            get
            {
                var day = 
                    _weekdays
                        .Where(x => x.HasValue && x > DateTime.Today.DayOfWeek)
                        ?.Min(x => x - DateTime.Today.DayOfWeek) 
                    ?? 
                    _weekdays
                        .Where(x => x.HasValue && x <= DateTime.Today.DayOfWeek)
                        ?.Min(x => DateTime.Today.DayOfWeek - x);
                return day.HasValue ? ((DayOfWeek)day.Value).ToString() : "Not";
            }
        }

        public string RepeatStr => _repeat.ToString();


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}