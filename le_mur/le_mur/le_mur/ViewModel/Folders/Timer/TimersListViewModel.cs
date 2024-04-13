using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using le_mur.Model;
using le_mur.View.Folders.Timer;
using TL;
using Xamarin.Forms;

namespace le_mur.ViewModel.Folders.Timer
{
    public class TimersListViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        private ObservableCollection<Model.Timer> _timers;
        public Command AddCommand { get; }
        public Command EditCommand { get; }
        public Command BackCommand { get; }
        public TimersListViewModel()
        {
            this.AddCommand = new Command(this.OnAddCommand);
            this.EditCommand = new Command(this.OnEditCommand);
            this.BackCommand = new Command(this.OnBackCommand);

            //todo запрос к бд для получения всех таймеров
            _timers  = new ObservableCollection<Model.Timer>() { new Model.Timer(0), new Model.Timer(1) };
            // _timers = new ObservableCollection<Model.Timer>();
        }
        public bool HasAnyTimers => _timers.Any();
        public ObservableCollection<Model.Timer> TimersList
        {
            get => _timers;
            set
            {
                if (_timers != value)
                {
                    _timers = value;
                    OnPropertyChanged("TimersList");
                }
            }
        }

        private async void OnBackCommand()
        {
            await Navigation.PopAsync();
        }

        private void OnEditCommand()
        {
            throw new System.NotImplementedException();
        }

        private async void OnAddCommand()
        {
            await Navigation.PushAsync(new TimerPage());
        }

    }
}