using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using le_mur.ViewModel.Folders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View.Folders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFoldersPage : ContentPage
    {
        public EditFoldersPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new EditFoldersViewModel() { Navigation = this.Navigation };
        }
    }
}