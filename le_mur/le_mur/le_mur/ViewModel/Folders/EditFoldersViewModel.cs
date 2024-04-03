using System.Collections.ObjectModel;
using System.Linq;
using le_mur.View.Folders;
using Xamarin.Forms;
using Folder = le_mur.Model.Folder;

namespace le_mur.ViewModel.Folders
{
    public class EditFoldersViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private readonly ObservableCollection<Folder> _allFolders;

        private ObservableCollection<Folder> _folders;

        private string _searchRequest;

        public Command BackCommand { get; }
        public Command ChangeCommand { get; }
        public Command DeleteCommand { get; }
        public Command DoneCommand { get; }

        public EditFoldersViewModel()
        {
            BackCommand = new Command(OnBackCommand);
            ChangeCommand = new Command(OnChangeCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            DoneCommand = new Command(OnDoneCommand);

            _allFolders = new ObservableCollection<Folder>
            {
                new Folder(1, "new folder1"),
                new Folder(2, "new folder2")
            };
            Folders = _allFolders;
        }

        public ObservableCollection<Folder> Folders
        {
            get => _folders;
            set
            {
                if (_folders != value)
                {
                    _folders = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchRequest
        {
            get => _searchRequest;
            set
            {
                if (_searchRequest != value)
                {
                    _searchRequest = value;
                    Search();
                    OnPropertyChanged();
                }
            }
        }

        private void Search()
        {
            var list = _allFolders.Where(c => c.Name.ToLower().Contains(SearchRequest.ToLower())).ToList();
            var collection = new ObservableCollection<Folder>();
            foreach (var item in list)
                collection.Add(item);
            Folders = collection;
        }
        
        private async void OnBackCommand()
        {
            await Navigation.PopAsync();
        }

        private async void OnDoneCommand()
        {
            await Navigation.PushAsync(new FoldersPage());
        }
        private async void OnChangeCommand(object id)
        {
            await Navigation.PushAsync(new AddFolderPage((int)id));
        }
        private void OnDeleteCommand(object id)
        {
        }
    }
}