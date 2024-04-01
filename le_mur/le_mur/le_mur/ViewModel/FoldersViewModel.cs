using le_mur.Model;
using le_mur.NetworkCalling;
using le_mur.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TL;
using Xamarin.Forms;
using Folder = le_mur.Model.Folder;

namespace le_mur.ViewModel
{
    public class FoldersViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command BackCommand { get; }
        public Command AddCommand { get; }
        public Command StatusChangeCommand { get; }
        public Command EditCommand { get; }

        private ObservableCollection<Folder> folders;
        public ObservableCollection<Folder> Folders
        {
            get { return folders; }
            set
            {
                if (folders != value)
                {
                    folders = value;
                    EditIsVisible = folders.Count() > 0 ? true : false;
                    OnPropertyChanged("Folders");
                }
            }
        }

        private ObservableCollection<Folder> allFolders;

        private string searchRequest;
        public string SearchRequest
        {
            get { return searchRequest; }
            set
            {
                if (searchRequest != value)
                {
                    searchRequest = value;
                    Search();
                    OnPropertyChanged("SearchRequest");
                }
            }
        }

        private bool editIsVisible;
        public bool EditIsVisible
        {
            get { return editIsVisible; }
            set
            {
                if (editIsVisible != value)
                {
                    editIsVisible = value;
                    OnPropertyChanged("EditIsVisible");
                }
            }
        }

        public FoldersViewModel()
        {
            BackCommand = new Command(OnBackCommand);
            AddCommand = new Command(OnAddCommand);
            StatusChangeCommand = new Command(OnStatusChangeCommand);
            EditCommand = new Command(OnEditCommand);

            allFolders = new ObservableCollection<Folder>();
            allFolders.Add(new Folder(1, "new folder1"));
            allFolders.Add(new Folder(2, "new folder2"));
            Folders = allFolders;
        }

        private void Search()
        {
            var list = allFolders.Where(c => c.Name.ToLower().Contains(SearchRequest.ToLower()) == true).ToList();
            ObservableCollection<Folder> collection = new ObservableCollection<Folder>();
            foreach (var item in list)
                collection.Add(item);
            Folders = collection;
        }

        private void OnAddCommand()
        {

        }

        private void OnBackCommand()
        {
            
        }

        private void OnEditCommand()
        {

        }

        private void OnStatusChangeCommand(object parameter)
        {
            var id = (int)parameter;
            Folders.Where(x => x.Id == id).First().IsShow = !Folders.Where(x => x.Id == id).First().IsShow;
        }
    }
}
