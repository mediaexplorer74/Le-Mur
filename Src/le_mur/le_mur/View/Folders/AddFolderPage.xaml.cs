using le_mur.ViewModel.Folders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using le_mur.Model;
using TL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View.Folders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFolderPage : ContentPage
    {
        public AddFolderPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new AddFolderViewModel() { Navigation = this.Navigation };
        }
        public AddFolderPage(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new AddFolderViewModel(id) { Navigation = this.Navigation };
        }
    }
}