using System;
using System.Collections.Generic;
using le_mur.Model;
using le_mur.NetworkCalling;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using le_mur.View.Folders;
using TL;
using Xamarin.Forms;
using Folder = le_mur.Model.Folder;

namespace le_mur.ViewModel.Folders
{
    public class AddFolderViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        private Folder _folder;
        private ObservableCollection<ChatInfo> _channels;
        private ObservableCollection<ChatInfo> _allChannels;
        private string _searchRequest;
        public Command ChronicCommand { get; }
        public Command SaveCommand { get; }
        public Command StatusChangeCommand { get; }
        public Command BackCommand { get; }

        public AddFolderViewModel()
        {
            this.ChronicCommand = new Command(this.OnChronicCommand);
            this.SaveCommand = new Command(this.OnSaveCommand);
            this.StatusChangeCommand = new Command(OnStatusChangeCommand);
            this.BackCommand = new Command(OnBackCommand);
            _folder = new Folder();
            _channels = new ObservableCollection<ChatInfo>();
            _allChannels = new ObservableCollection<ChatInfo>();
            GetAllChannels();
        }

        public AddFolderViewModel(int id) : this()
        {
            _folder = new Folder(id);
            SetVisibleChannels();
        }

        public string FolderName 
        { 
            get => _folder.Name;
            set
            {
                if (_folder.Name != value)
                {
                    _folder.Name = value;
                    OnPropertyChanged("FolderName");
                }
            }
        }

        public ObservableCollection<ChatInfo> Channels
        {
            get => _channels;
            set
            {
                if (_channels != value)
                {
                    _channels = value;
                    OnPropertyChanged("Channels");
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
                    OnPropertyChanged("SearchRequest");
                }
            }
        }

        private async void GetAllChannels()
        {
            var chats = await TelegramApi.GetChatsInfo();
            foreach (var item in chats)
            {
                item.IsShow = false;
                _allChannels.Add(item);
                Channels.Add(item);
            }
        }

        private async void SetVisibleChannels()
        {
            _allChannels = new ObservableCollection<ChatInfo>(_allChannels.Where(x => x.IsShow = _folder.Chats.Any(item => item.Id == x.Id)).ToList());
            Channels = new ObservableCollection<ChatInfo>(_allChannels);
        }

        private void OnChronicCommand()
        {
            
        }

        private async void OnSaveCommand()
        {
            if (_folder.Name == "")
                _folder.Name = "New Folder";

            // todo сохранение папки в бд
            // поиск в бд. если такой папки нет, то добавление. иначе редактирование и сохранение

            await Navigation.PushAsync(new FoldersPage());
        }

        private void OnStatusChangeCommand(object parameter)
        {
            var id = (InputPeer)parameter;
            if (Channels.First(x => x.Id == id).IsShow ^= true)
                _folder.Chats.Add(new ChatInfo(id));
            else
                _folder.Chats.Remove(_folder.Chats.First(x => x.Id == id));
        }

        private async void OnBackCommand()
        {
            await Navigation.PopAsync();
        }

        private void Search()
        {
            Channels = new ObservableCollection<ChatInfo>(_allChannels.Where(x => x.Title.ToLower().Contains(SearchRequest.ToLower())).ToList());
        }
    }
}