using System.Collections.ObjectModel;
using System.Linq;
using le_mur.View.Folders.Timer;
using Xamarin.Forms;

namespace le_mur.ViewModel.Folders.Timer
{
    public class EditTimersListViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command BackCommand { get; }
        public Command ChangeCommand { get; }
        public Command DeleteCommand { get; }
        public Command DoneCommand { get; }

        public EditTimersListViewModel()
        {
            this.BackCommand = new Command(this.OnBackCommand);
            this.ChangeCommand = new Command(this.OnChangeCommand);
            this.DeleteCommand = new Command(this.OnDeleteCommand);
            this.DoneCommand = new Command(this.OnDoneCommand);

            //todo запрос к бд для получения всех таймеров
            TimersList = new ObservableCollection<Model.Timer>() { new Model.Timer(0), new Model.Timer(1) };
        }

        public ObservableCollection<Model.Timer> TimersList { get; }
        public bool HasAnyTimers => TimersList.Any();

        private async void OnBackCommand()
        {
            await Navigation.PopAsync();
        }
        private async void OnDoneCommand()
        {
            await Navigation.PushAsync(new TimersListPage());
        }
        private void OnDeleteCommand(object id)
        {
            //todo удаление таймера из бд
            throw new System.NotImplementedException();
        }
        private async void OnChangeCommand(object id)
        {
            await Navigation.PushAsync(new TimerPage((int)id));
        }
    }
}