using System.Collections.ObjectModel;
using System.Linq;
using le_mur.View.Folders;
using Xamarin.Forms;
using Folder = le_mur.Model.Folder;

namespace le_mur.ViewModel.Folders
{
    public class FoldersViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Folder> _allFolders;

        private bool _editIsVisible;

        private ObservableCollection<Folder> _folders;

        private string _searchRequest;

        public FoldersViewModel()
        {
            BackCommand = new Command(OnBackCommand);
            AddCommand = new Command(OnAddCommand);
            StatusChangeCommand = new Command(OnStatusChangeCommand);
            EditCommand = new Command(OnEditCommand);

            _allFolders = new ObservableCollection<Folder>
            {
                new Folder(1, "new folder1"),
                new Folder(2, "new folder2")
            };
            Folders = _allFolders;
        }

        public INavigation Navigation { get; set; }

        public Command BackCommand { get; }
        public Command AddCommand { get; }
        public Command StatusChangeCommand { get; }
        public Command EditCommand { get; }

        public ObservableCollection<Folder> Folders
        {
            get => _folders;
            set
            {
                if (_folders != value)
                {
                    _folders = value;
                    EditIsVisible = _folders.Any() ? true : false;
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

        public bool EditIsVisible
        {
            get => _editIsVisible;
            set
            {
                if (_editIsVisible != value)
                {
                    _editIsVisible = value;
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

        private async void OnAddCommand()
        {
            await Navigation.PushAsync(new AddFolderPage());
        }

        private void OnBackCommand()
        {
        }

        private async void OnEditCommand()
        {
            await Navigation.PushAsync(new EditFoldersPage());
        }

        private void OnStatusChangeCommand(object parameter)
        {
            var id = (int)parameter;
            Folders.First(x => x.Id == id).IsShow ^= true;
        }
    }
}