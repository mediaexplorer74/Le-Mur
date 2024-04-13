using System;
using System.Collections.Generic;
using le_mur.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using le_mur.Consts;
using le_mur.Helpers;
using TL;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace le_mur.ViewModel.Folders.Timer
{
    public class TimerViewModel : BaseViewModel
    {
        private readonly Color defaultColor = Color.White;
        private readonly Color selectedColor = Color.LightGray;

        public INavigation Navigation { get; set; }
        private Model.Timer _timer;
        private ObservableCollection<Color> _buttonColors;

        public Command ChooseWeekdayCommand { get; }
        public Command ChooseRepeatCommand { get; }
        public Command SaveCommand { get; }
        public Command BackCommand { get; }


        public TimerViewModel()
        {
            _timer = new Model.Timer();
            _buttonColors = new ObservableCollection<Color>()
            {
                defaultColor, defaultColor, defaultColor, defaultColor, defaultColor, 
                defaultColor, defaultColor, defaultColor, defaultColor
            };

            this.ChooseWeekdayCommand = new Command(this.OnChooseWeekdayCommand);
            this.ChooseRepeatCommand = new Command(this.OnChooseRepeatCommand);
            this.SaveCommand = new Command(this.OnSaveCommand);
            this.BackCommand = new Command(this.OnBackCommand);
        }

        public TimerViewModel(int id) : this()
        {
            _timer = new Model.Timer(id);
            _timer.Weekdays.Where(x => x.HasValue).ForEach(x => _buttonColors[(int)x] = selectedColor);
        }

        public ObservableCollection<Color> ButtonColors
        {
            get => _buttonColors;
            set
            {
                if (_buttonColors != value)
                {
                    _buttonColors = value;
                    OnPropertyChanged("ButtonColors");
                }
            }
        }
        public Model.Timer Timer
        {
            get => _timer;
            set
            {
                if (value != _timer)
                {
                    _timer = value;
                    OnPropertyChanged("Timer");
                }
            }
        }

        private void OnSaveCommand()
        {
            //weekdays
            var weekdays = _buttonColors
                .Select((color, i) => (color, i))
                .Where(x => x.color == selectedColor && x.i < 7)
                .Select(x => (DayOfWeek)x.i)
                .ToArray();
            for (int i = 0; i < _timer.Weekdays.Length; i++)
                _timer.Weekdays[i] = i < weekdays.Length ? weekdays[i] : (DayOfWeek?)null;

            //dates
            var today = DateTime.Today;
            for (int i = 0; i < _timer.Dates.Length; i++)
                _timer.Dates[i] = i < weekdays.Length ? today.NextDayOfWeek(weekdays[i]) : (DateTime?)null;

            //repeat
            Model.Timer.RepeatStatus repeat;
            try
            {
                repeat = (Model.Timer.RepeatStatus)_buttonColors
                    .Select((color, i) => (color, i))
                    .First(x => x.color == selectedColor && x.i >= 7)
                    .i - 6;
            }
            catch (Exception ex)
            {
                repeat = Model.Timer.RepeatStatus.Not;
            }
            _timer.Repeat = repeat;

            //todo del
            //todo save into db
            _timer.Weekdays.ForEach(x => Debug.WriteLine(x.HasValue ? x.Value.ToString() : "null"));
            _timer.Dates.ForEach(x => Debug.WriteLine(x.HasValue ? x.Value.ToString() : "null"));
            Debug.WriteLine(_timer.Repeat.ToString());
            Debug.WriteLine($"from {_timer.StartTime} till {_timer.EndTime}");
        }

        private async void OnBackCommand()
        {
            await Navigation.PopAsync();
        }
        private void OnChooseWeekdayCommand(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int buttonIndex))
            {
                _buttonColors[buttonIndex] = _buttonColors[buttonIndex] == defaultColor ? selectedColor : defaultColor;
                ButtonColors = new ObservableCollection<Color>(_buttonColors);
            }
        }
        private void OnChooseRepeatCommand(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int buttonIndex))
            {
                switch (buttonIndex)
                {
                    case 7 when _buttonColors[8] == selectedColor:
                        _buttonColors[8] = defaultColor;
                        break;
                    case 8 when _buttonColors[7] == selectedColor:
                        _buttonColors[7] = defaultColor;
                        break;
                }

                _buttonColors[buttonIndex] = _buttonColors[buttonIndex] == defaultColor ? selectedColor : defaultColor;
                ButtonColors = new ObservableCollection<Color>(_buttonColors);
            }
        }
    }
}