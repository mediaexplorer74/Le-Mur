using System.Collections.ObjectModel;
using System.Linq;
using le_mur.View.Folders.Timer;
using Xamarin.Forms;

namespace le_mur.ViewModel.Folders.Timer
{
    public class TimersListViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command AddCommand { get; }
        public Command EditCommand { get; }
        public Command BackCommand { get; }
        public TimersListViewModel()
        {
            this.AddCommand = new Command(this.OnAddCommand);
            this.EditCommand = new Command(this.OnEditCommand);
            this.BackCommand = new Command(this.OnBackCommand);

            //todo запрос к бд для получения всех таймеров
            TimersList  = new ObservableCollection<Model.Timer>() { new Model.Timer(0), new Model.Timer(1) };
            // TimersList = new ObservableCollection<Model.Timer>();
        }
        public bool HasAnyTimers => TimersList.Any();
        public ObservableCollection<Model.Timer> TimersList { get; }

        private async void OnBackCommand()
        {
            await Navigation.PopAsync();
        }

        private async void OnEditCommand()
        {
            await Navigation.PushAsync(new EditTimersListPage());
        }

        private async void OnAddCommand()
        {
            await Navigation.PushAsync(new TimerPage());
        }

    }
}